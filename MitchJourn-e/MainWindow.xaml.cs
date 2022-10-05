using MitchJourn_e.Properties;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using Application = System.Windows.Application;
using TextBox = System.Windows.Controls.TextBox;
using Control = System.Windows.Controls.Control;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using MessageBox = System.Windows.MessageBox;
using System.Windows.Media;
using Button = System.Windows.Controls.Button;
using System.Windows.Forms;
using System.Text;
using System.Collections.Specialized;
using System.Drawing.Imaging;

namespace MitchJourn_e
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Timers.Timer timer;
        string lastImg = "";
        string lastPrompt = "";
        string currentProcessName = "";
        public int currentCMDProcessID = 0;
        public Process rendererProcess;
        public StreamWriter textWriter;
        bool isFirstRun = true;

        public MainWindow()
        {
            InitializeComponent();
            InitializePromptHelper2();
            InitializeSettings();
            StartRendering();
        }

        /// <summary>
        /// Go button
        /// </summary>
        private void btn_Go_Click(object sender, RoutedEventArgs e)
        {
            if ((string)lbl_Status.Content == "Stopped continuously prompting.")
            {
                chk_ContinuouslyPrompt.IsChecked = true;
                StartRendering();
            }
            else
            {
                StartRendering();
            }
        }

        /// <summary>
        /// Start the Python script and create the specified image
        /// </summary>
        /// <param name="promptText">Leave blank to use the prompt text box</param>
        /// <param name="incrementSeed">Use the next seed number for this image, chk_IncrementSeed must also be checked</param>
        private async void StartRendering(string promptText = "", bool incrementSeed = true)
        {
            Process process = new Process();
            bool useOldCMD = rendererProcess != null && !rendererProcess.HasExited;
            if (useOldCMD)
            {
                process = rendererProcess;
            }

            Application.Current.Dispatcher.Invoke((Action)async delegate
            {
                string prompt = CleanPrompt(txt_Prompt.Text);
                string promptHelper = CleanPrompt(txt_PromptHelper.Text);
                string promptSettings = "";
                string imagePrompt = "";

                string seed = (string)Settings.Default["Seed"];
                string uprez = "";
                string useFullPrecision = "";

                lbl_Status.Content = "Loading...";

                if (promptText != "")
                {
                    prompt = promptText;
                }

                // Seed
                if (seed == "random")
                {
                    seed = "" + DateTime.Now.Hour +
                        DateTime.Now.Minute +
                        DateTime.Now.Second +
                        DateTime.Now.Millisecond;
                }
                else if ((bool)chk_IncrementSeed.IsChecked && incrementSeed)
                {
                    int.TryParse(seed, out int seedAsInt);

                    seed = (seedAsInt + 1).ToString();
                    txt_Seed.Text = seed;
                }

                // Upscale
                if ((bool)chk_HighRes.IsChecked)
                {
                    uprez = "-G 1";
                }

                // Image Prompt
                if (txt_ImagePrompt.Text != "")
                {
                    if (float.TryParse(txt_ImagePromptWeight.Text, out float imageWeight))
                    {
                        imagePrompt += $"-I {txt_ImagePrompt.Text.Replace(@"\", @"\\")}";
                        imagePrompt += $" --strength {1 - imageWeight}";
                    }
                }
                
                promptSettings = "" +
                        $"-W {Settings.Default["Width"]} " +
                        $"-H {Settings.Default["Height"]} " +
                        $"-C {Settings.Default["Scale"]} " +
                        $"-S {seed} " +
                        $"-s {Settings.Default["Steps"]} " +
                        $"-n {Settings.Default["Iter"]} " +
                        $"{uprez}" +
                        $"{imagePrompt}";

                // Full Precision
                if (Settings.Default["UseFullPrecision"].ToString() == "1")
                {
                    useFullPrecision = "-F";
                }

                // create the process properties
                ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe");
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.UseShellExecute = false;

                // check if there is already a cmd window running from this program
                if (!useOldCMD)
                {
                    // start a new cmd prompt
                    process = Process.Start(processStartInfo);

                    currentProcessName = process.MainWindowTitle;
                    currentCMDProcessID = process.Id;
                    rendererProcess = process;
                    textWriter = process.StandardInput;
                    string sampler = Settings.Default["SamplerType"].ToString();

                    // move the cmd directory to the main stable diffusion path and open the python environment called ldm (environment used at python install)
                    string prerequisites = $"cd {Settings.Default["MainPath"]} & call %userprofile%\\anaconda3\\Scripts\\activate.bat ldm &";
                    // send the command to the CMD window to start the python script, enable the upsampler
                    process.StandardInput.WriteLine($"{prerequisites} python dream.py --gfpgan_bg_tile {Settings.Default["gfpganBgTileSize"]} --gfpgan_upscale {Settings.Default["gfpganUprezScale"]} --gfpgan_bg_upsampler realesrgan {useFullPrecision}" +
                        $" --gfpgan --gfpgan_dir GFPGAN --gfpgan_model_path {Settings.Default["MainPath"]}\\GFPGAN\\experiments\\pretrained_models\\GFPGANv1.3.pth --sampler {sampler}");
                    // send the command to the CMD window to set the image output directory (TODO: I don't think this is actually working, investigate escaped characters)
                    process.StandardInput.WriteLine($"cd {Settings.Default["MainPath"].ToString().Replace(@"\", @"\\")}\\outputs\\img-samples\\");

                    if (isFirstRun)
                    {
                        if (Settings.Default["EnableWelcomePrompt"].ToString() == "1")
                        {
                            process.StandardInput.WriteLine($"Welcome -S 13{seed}37 -s 25");
                        }
                        else
                        {
                            lbl_Status.Content = "Enter a prompt and press go!";
                        }
                        isFirstRun = false;
                    }
                    else
                    {
                        process.StandardInput.WriteLine($"{prompt} {promptHelper} {promptSettings}");
                    }
                }
                else // if the CMD window is already opened, send the prompt
                {
                    process.StandardInput.WriteLine($"{prompt} {promptHelper} {promptSettings}");
                }

                // start or restart the timer to check for a new image in the output directory
                if (timer != null)
                    timer.Dispose();
                timer = new(interval: 1000);
                timer.Elapsed += (sender, e) => DisplayImage();
                timer.Start();

                lastPrompt = txt_Prompt.Text;
            });

        }

        /// <summary>
        /// Checks the output directory and displays the last rendered image
        /// </summary>
        void DisplayImage(string promptRAW = "", bool hasImagePrompt = false)
        {
            string imageDirectory = $"{Settings.Default["MainPath"]}\\outputs\\img-samples\\";

            if (Directory.Exists(imageDirectory))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(imageDirectory);

                FileInfo[] Files = directoryInfo.GetFiles("*.png"); //Getting Text files

                if (Files.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate {
                        try
                        {
                            // Check if there is a new image in the folder
                            string filePath = Files.Last().FullName;
                            if (filePath != lastImg)
                            {
                                if (lastImg == "")
                                {
                                    lastImg = filePath;
                                    return;
                                }

                                // create a bitmap from the image file
                                BitmapImage myBitmapImage = new BitmapImage();
                                myBitmapImage.BeginInit();
                                myBitmapImage.UriSource = new Uri(filePath);
                                myBitmapImage.EndInit();
                               
                                // Create the image using the bitmap
                                Image output = new Image
                                {
                                    Margin = new Thickness(8),
                                    Stretch = System.Windows.Media.Stretch.Uniform,
                                    Source = myBitmapImage
                                };
                                scroll_Images.MaxHeight = myBitmapImage.Height + 50;

                                RenderedImage renderedImage = new RenderedImage().CreateRenderedImage(output, filePath, txt_Prompt.Text, txt_Scale.Text, txt_Seed.Text,
                                    txt_Steps.Text, myBitmapImage.Width.ToString(), myBitmapImage.Height.ToString(), txt_ImagePrompt.Text, txt_ImagePromptWeight.Text);

                                output.Tag = renderedImage;
                                
                                // Add the right click menu to the image
                                ContextMenu rightClickMenu = new ContextMenu();
                                
                                output.ContextMenu = rightClickMenu;

                                // Create Variation
                                MenuItem menuItemCreateVariation = new MenuItem();
                                menuItemCreateVariation.Header = "Create Variations";
                                menuItemCreateVariation.Tag = renderedImage;
                                menuItemCreateVariation.Click += MenuItemCreateVariation_Click;
                                rightClickMenu.Items.Add(menuItemCreateVariation);

                                // Save As
                                MenuItem menuItemSaveAs = new MenuItem();
                                menuItemSaveAs.Header = "Save As";
                                menuItemSaveAs.Tag = renderedImage.filePath;
                                menuItemSaveAs.Click += MenuItemSaveAs_Click;
                                rightClickMenu.Items.Add(menuItemSaveAs);

                                // Open Containing Folder
                                MenuItem menuItemOpenContainingFolder = new MenuItem();
                                menuItemOpenContainingFolder.Header = "Open Containing Folder";
                                menuItemOpenContainingFolder.Tag = renderedImage.filePath;
                                menuItemOpenContainingFolder.Click += MenuItemOpenContainingFolder_Click;
                                rightClickMenu.Items.Add(menuItemOpenContainingFolder);

                                // Recreate prompt
                                MenuItem menuItemRecreatePrompt = new MenuItem();
                                menuItemRecreatePrompt.Header = "Recreate Prompt";
                                menuItemRecreatePrompt.Tag = renderedImage;
                                menuItemRecreatePrompt.Click += MenuItemRecreatePrompt_Click;
                                rightClickMenu.Items.Add(menuItemRecreatePrompt);

                                // ----
                                rightClickMenu.Items.Add(new Separator());

                                // Delete
                                MenuItem menuItemDelete = new MenuItem();
                                menuItemDelete.Header = "Delete";
                                menuItemDelete.Tag = renderedImage;
                                menuItemDelete.Click += MenuItemDelete_Click;
                                rightClickMenu.Items.Add(menuItemDelete);

                                // Add the image to the top of the image stack
                                stack_Images.Items.Insert(0, output);
                                lastImg = filePath;

                                // If trying to create variations of upscalled images (too large to recreate image size)
                                if (lbl_Status.Content.ToString() != "Can't set upscaled image as image source, creating downrezed version instead...")
                                {
                                    lbl_Status.Content = $"Created image from seed {txt_Seed.Text}";
                                }
                                else
                                {
                                    lbl_Status.Content = "Created downrezed version.";
                                }

                                // continuous prompting
                                if ((bool)chk_ContinuouslyPrompt.IsChecked)
                                {
                                    StartRendering();
                                }
                            }
                        }
                        catch { return; }
                    });
                }
            }
        }

        private void InitializePromptHelper2()
        {
            //Properties.Settings.Default.Reset();
            /*
                Artists/Greg=greg rutkowski
                Artists/Thomas=thomas kinkade
             */
            string[] presets = ((StringCollection)Settings.Default["PromptHelperPresets"]).Cast<string>().ToArray<string>();

            foreach (string preset in presets)
            {
                // add the presets to the prompt helper button
                string[] directoriesAndValue = preset.Split('=');
                string value = directoriesAndValue[1];
                string allDirectories = directoriesAndValue[0];
                string[] directories = allDirectories.Split('/');
                string topDirectory = directories[0];
                string menuHeader = directories[1];

                MenuItem helperMenuItem = new MenuItem();

                // check if it's a unique directory
                bool isUniqueDirectory = true;
                foreach (MenuItem menuItem in menuItem_PromptHelper.Items)
                {
                    if (menuItem.Header.ToString() == topDirectory)
                    {
                        isUniqueDirectory = false;
                        helperMenuItem = menuItem;
                        break;
                    }
                }

                // add directory
                if (isUniqueDirectory)
                {
                    helperMenuItem.Header = topDirectory;
                    helperMenuItem.StaysOpenOnClick = true;
                    menuItem_PromptHelper.Items.Add(helperMenuItem);
                }

                MenuItem MenuItemPrompt = new MenuItem();
                MenuItemPrompt.Header = menuHeader;
                MenuItemPrompt.StaysOpenOnClick = true;
                MenuItemPrompt.Tag = value;
                MenuItemPrompt.Click += PromptHelperMenuItem_Click2;
                helperMenuItem.Items.Add(MenuItemPrompt);

                // add the prompt helper editor text boxes and buttons
                StackPanel stackPanel = new StackPanel
                {
                    Orientation = System.Windows.Controls.Orientation.Horizontal,
                    Tag = helperMenuItem
                };
                TextBox textBox = new TextBox
                {
                    Text = preset,
                    FontSize = 16,
                    Padding = new Thickness(2),
                    Margin = new Thickness(4),
                    Width = 420,
                    Tag = menuHeader
                };
                textBox.TextChanged += PromptPresetTextChanged;
                Button btn_Delete = new Button
                {
                    Content = "Delete",
                    Padding = new Thickness(2),
                    Margin = new Thickness(4),
                    Tag = stackPanel
                };
                btn_Delete.Click += Btn_Delete_Click;
                stackPanel.Children.Add(textBox);
                stackPanel.Children.Add(btn_Delete);
                stack_PromptHelperPresets.Children.Add(stackPanel);
            }
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            StackPanel stack_PromptToDelete = (StackPanel)((Button)sender).Tag;
            MenuItem menuItem_TopDirectory = (MenuItem)stack_PromptToDelete.Tag;
            string presetValue = "";

            try
            {
                foreach (StackPanel stack in stack_PromptHelperPresets.Children)
                {
                    if (stack == stack_PromptToDelete && ((TextBox)stack.Children[0]).Tag != null)
                    {
                        stack.Visibility = Visibility.Collapsed;
                        presetValue = ((TextBox)stack.Children[0]).Tag.ToString();
                    }
                }
                foreach (MenuItem menuItemTopDirectories in menuItem_PromptHelper.Items)
                {
                    if (menuItemTopDirectories == menuItem_TopDirectory)
                    {
                        foreach (MenuItem menuItem in menuItemTopDirectories.Items)
                        {
                            if (presetValue == menuItem.Header.ToString())
                            {
                                menuItem.Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                }
            }
            catch { return; }
        }

        /// <summary>
        /// Creates the menu button with words to add to the prompt box
        /// </summary>
        private void InitializePromptHelper()
        {
            string[] mainMenuItems = { "Artists", "Styles", "Sources", "Effects", "Mediums" };
            string[] artists = { "greg rutkowski", "wlop", "sung choi", "ilya kuvshinov", "andreas rocha", "lois van baarle", "rossdraws", "Rembrandt", "marc simonetti", "Luis Royo", "beksiński", "hieronymus bosch", "thomas kinkade",
                "vincent van gogh", "leonid afremov", "claude monet", "edward hopper", "norman rockwell", "william-adolphe bouguereau", "albert bierstadt", "john singer sargent", "pierre-auguste renoir"};
            string[] styles = { "atmospheric", "hyperdetailed", "hd", "magical world", "cinematic lighting", "concept art", "logo", "editorial photography", "wildlife photography", "technicolour", "anaglyph" };
            string[] sources = { "artstation", "deviantart", "behance", "cgsociety", "dribble", "flickr", "instagram", "pexels", "pinterest", "pixabay", "pixiv", "polycount", "reddit", "shutterstock", "tumblr", "unsplash", "zbrush central" };
            string[] effects = { "post processing", "cgi" , "chromatic aberration", "anaglyph", "cropped", "glowing edges", "glow effect", "bokeh", "dramatic", "glamor shot", "colourful", "complimentary-colours", "golden hour", "dark mood",
                "multiverse", "soft lighting", "hard lighting", "volumetric", "lumen global illumination", "beautiful lighting", "octane render" };
            string[] mediums = { "photo realistic", "graphic novel", "fountain pen", "pastel art", "fine art", "acrylic paint", "oil paint", "watercolour", "digital art", "magazine", "comic book", "pokemon card", "puzzle" };

            foreach (string mainMenuItemName in mainMenuItems)
            {
                MenuItem mainMenuItem = new MenuItem();
                mainMenuItem.Header = mainMenuItemName;
                mainMenuItem.StaysOpenOnClick = true;
                menuItem_PromptHelper.Items.Add(mainMenuItem);

                if (mainMenuItemName == "Artists")
                {
                    foreach (string artistsMenuItemName in artists)
                    {
                        MenuItem ArtistsMenuItem = new MenuItem();
                        ArtistsMenuItem.Header = artistsMenuItemName;
                        ArtistsMenuItem.Click += PromptHelperMenuItem_Click;
                        ArtistsMenuItem.StaysOpenOnClick = true;
                        mainMenuItem.Items.Add(ArtistsMenuItem);
                    }
                }
                else if (mainMenuItemName == "Styles")
                {
                    foreach (string stylesMenuItemName in styles)
                    {
                        MenuItem stylesMenuItem = new MenuItem();
                        stylesMenuItem.Header = stylesMenuItemName;
                        stylesMenuItem.Click += PromptHelperMenuItem_Click;
                        stylesMenuItem.StaysOpenOnClick = true;
                        mainMenuItem.Items.Add(stylesMenuItem);
                    }
                }
                else if (mainMenuItemName == "Sources")
                {
                    foreach (string sourcesMenuItemName in sources)
                    {
                        MenuItem sourcesMenuItem = new MenuItem();
                        sourcesMenuItem.Header = sourcesMenuItemName;
                        sourcesMenuItem.Click += PromptHelperMenuItem_Click;
                        sourcesMenuItem.StaysOpenOnClick = true;
                        mainMenuItem.Items.Add(sourcesMenuItem);
                    }
                }
                else if (mainMenuItemName == "Effects")
                {
                    foreach (string effectsMenuItemName in effects)
                    {
                        MenuItem effectsMenuItem = new MenuItem();
                        effectsMenuItem.Header = effectsMenuItemName;
                        effectsMenuItem.Click += PromptHelperMenuItem_Click;
                        effectsMenuItem.StaysOpenOnClick = true;
                        mainMenuItem.Items.Add(effectsMenuItem);
                    }
                }
                else if (mainMenuItemName == "Mediums")
                {
                    foreach (string mediumsMenuItemName in mediums)
                    {
                        MenuItem mediumsMenuItem = new MenuItem();
                        mediumsMenuItem.Header = mediumsMenuItemName;
                        mediumsMenuItem.Click += PromptHelperMenuItem_Click;
                        mediumsMenuItem.StaysOpenOnClick = true;
                        mainMenuItem.Items.Add(mediumsMenuItem);
                    }
                }
            }
        }

        /// <summary>
        /// Removes characters from the prompt that would disallow the generation to run
        /// </summary>
        private string CleanPrompt(string promptText)
        {
            string output = "";

            if (promptText != null)
            {
                output = promptText;

                StringBuilder stringBuilder = new StringBuilder();
                foreach (char c in promptText)
                {
                    // rebuild the string, adding back only the following allowed characters
                    if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ' || c == '.' || c == '_' || c == ',' 
                        || c == '/' || c == '?' || c == '!' || c == '&' || c == '+' || c == '$' || c == '%' || c == '^' || c == '#' || c == '@'
                        || c == '(' || c == ')' || c == ':' || c == '-' || c == '\\')
                    {
                        stringBuilder.Append(c);
                    }
                }
                output = stringBuilder.ToString();

                if ((bool)chk_AlternateToken.IsChecked)
                {
                    output = CreateAlternatePromptTokens(output);
                }
            }

            return output;
        }

        /// <summary>
        /// Changes the spaces to underscores to slightly change the tokens used to generate the image
        /// </summary>
        /// <param name="promptText"></param>
        /// <returns></returns>
        private string CreateAlternatePromptTokens(string promptText)
        {
            string output = "";

            if (promptText != null)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (char c in promptText)
                {
                    if (c == ' ')
                    {
                        stringBuilder.Append('_');
                    }
                    else if (c == '_')
                    {
                        stringBuilder.Append(' ');
                    }
                    else
                    {
                        stringBuilder.Append(c);
                    }
                }

                output = stringBuilder.ToString();
            }

            return output;
        }

        /// <summary>
        /// Add selected menuItem's name to the prompt box
        /// </summary>
        /// <param name="sender">MenuItem</param>
        private void PromptHelperMenuItem_Click2(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_PromptHelper.Text != "")
                {
                    char[] prompt = txt_PromptHelper.Text.ToCharArray();
                    if (prompt.Last() == ' ')
                    {
                        txt_PromptHelper.Text += ((MenuItem)sender).Tag;
                    }
                    else
                    {
                        txt_PromptHelper.Text += $" {((MenuItem)sender).Tag}";
                    }
                }
                else
                {
                    txt_PromptHelper.Text += ((MenuItem)sender).Tag;
                }
                expander_settings.IsExpanded = true;
            }
            catch { return; }
        }

        /// <summary>
        /// Add selected menuItem's name to the prompt box
        /// </summary>
        /// <param name="sender">MenuItem</param>
        private void PromptHelperMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                char[] prompt = txt_Prompt.Text.ToCharArray();
                if (prompt.Last() == ' ')
                {
                    txt_PromptHelper.Text += ((MenuItem)sender).Header;
                }
                else
                {
                    txt_PromptHelper.Text += $" {((MenuItem)sender).Header}";
                }
                expander_settings.IsExpanded = true;
            }
            catch { return; }
        }

        /// <summary>
        /// Right click menu item for images: Recreate Prompt
        /// </summary>
        private void MenuItemRecreatePrompt_Click(object sender, RoutedEventArgs e)
        {
            RenderedImage image = (RenderedImage)((MenuItem)sender).Tag;

            txt_Prompt.Text = image.prompt;
            txt_Scale.Text = image.promptWeight;
            txt_Seed.Text = image.seed;
            txt_Steps.Text = image.diffusionSteps;
            txt_Width.Text = image.width;
            txt_Height.Text = image.height;
            txt_ImagePrompt.Text = image.imagePrompt;
            txt_ImagePromptWeight.Text = image.imagePromptWeight;

            StartRendering(image.prompt, false);
        }

        /// <summary>
        /// Right click menu item for images: Open Containing Folder
        /// </summary>
        private void MenuItemOpenContainingFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string folderPath = ((MenuItem)sender).Tag.ToString();
                Process.Start("explorer.exe", $"/select,\"{folderPath}\"");
            }
            catch { return; }
        }

        /// <summary>
        /// Right click menu item for images: Save As
        /// </summary>
        private void MenuItemSaveAs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string file = ((MenuItem)sender).Tag.ToString();
                string[] fileParts = file.Split('\\');
                
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    DefaultExt = "png",
                    FileName = $"{fileParts.Last()}",
                    Filter = "png|*.png"
                };
                if (saveFileDialog.ShowDialog() == true)
                    File.Copy(file, saveFileDialog.FileName);
            }
            catch
            {
                lbl_Status.Content = "Failed to save png";
            }
        }

        /// <summary>
        /// Right click menu item for images: Delete Image
        /// </summary>
        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RenderedImage renderedImage = (RenderedImage)((MenuItem)sender).Tag;

                foreach (Image image in stack_Images.Items)
                {
                    if (((RenderedImage)image.Tag).filePath.Equals(renderedImage.filePath))
                    {
                        stack_Images.Items.Remove(image);
                        break;
                    }
                }

                File.Delete(renderedImage.filePath);
            }
            catch { }
        }

        /// <summary>
        /// Right click menu item for images: Create Variations
        /// </summary>
        private void MenuItemCreateVariation_Click(object sender, RoutedEventArgs e)
        {
            RenderedImage renderedImage = (RenderedImage)((MenuItem)sender).Tag;

            txt_Prompt.Text = renderedImage.prompt;
            txt_Seed.Text = renderedImage.seed;
            txt_ImagePromptWeight.Text = renderedImage.imagePromptWeight;

            if (int.Parse(renderedImage.width) > 640 || int.Parse(renderedImage.height) > 576)
            {
                chk_HighRes.IsChecked = false;
                chk_IncrementSeed.IsChecked = false;
                //txt_Seed.Text = image.seed;
                StartRendering();
                lbl_Status.Content = "Can't set upscaled image as image source, creating downrezed version instead...";
            }
            else
            {
                txt_ImagePrompt.Text = renderedImage.filePath;
                txt_Width.Text = renderedImage.width;
                txt_Height.Text = renderedImage.height;

                if (lbl_Status.Content.ToString() == "Created downrezed version.")
                {
                    chk_IncrementSeed.IsChecked = true;
                    chk_HighRes.IsChecked = true;
                }

                StartRendering();
            }
        }

        /// <summary>
        /// Kill's a process by it's ID.
        /// -1 = currentCMDProcessID (read as last run cmd process ID)
        /// </summary>
        /// <param name="processID"></param>
        private void StopRendering(int processID = -1)
        {
            if (processID == -1)
            {
                processID = currentCMDProcessID;
            }

            if (processID != 0)
            {
                try
                {
                    Process process = Process.GetProcessById(processID);
                    process.Kill();
                }
                catch
                {
                    // Failed to kill the specified process
                    return;
                }
            }
        }

        /// <summary>
        /// Stop button click event
        /// </summary>
        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            if (chk_ContinuouslyPrompt.IsChecked != true)
            {
                StopRendering();
            }
            else
            {
                chk_ContinuouslyPrompt.IsChecked = false;
                lbl_Status.Content = "Stopped continuously prompting.";
            }
        }

        /// <summary>
        /// Saves the setting if the sender is a TextBox with the setting property name as it's tag
        /// </summary>
        private void SettingsTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (((System.Windows.Controls.TextBox)sender).Tag != null)
                {
                    string settingValue = ((TextBox)sender).Text;
                    string setting = ((TextBox)sender).Tag.ToString();

                    // Save the setting from the value of the text box with the matching tag
                    foreach (SettingsProperty property in Settings.Default.Properties)
                    {
                        if (property.Name == setting)
                        {
                            Settings.Default[property.Name] = settingValue;
                            Settings.Default.Save();
                        }
                    }
                }
            }
            catch { return; }
        }

        /// <summary>
        /// Saves the setting if the sender is a TextBox with the property name as it's tag
        /// </summary>
        /// TODO: make it not hurt anymore
        private void PromptPresetTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<string> presets = new List<string>();
                foreach (StackPanel stack in stack_PromptHelperPresets.Children)
                {
                    foreach (Control control in stack.Children)
                    {
                        if (control is TextBox)
                        {
                            presets.Add(((TextBox)control).Text);
                        }
                    }
                }
                StringCollection stringCollection = new StringCollection();
                stringCollection.AddRange(presets.ToArray());
                Settings.Default["PromptHelperPresets"] = stringCollection;
                Settings.Default.Save();
            }
            catch { return; }

        }

        /// <summary>
        /// Save all settings text box values to the program's setting registry
        /// </summary>
        private void SaveAllSettings()
        {
            foreach (SettingsProperty property in Settings.Default.Properties)
            {
                foreach (Control control in stack_settings.Children)
                {
                    if (control is TextBox)
                    {
                        if (((TextBox)control).Tag == property.Name)
                        {
                            Settings.Default[property.Name] = ((TextBox)control).Text;
                        }
                    }
                }
            }
            Settings.Default.Save();
        }

        /// <summary>
        /// Loads the settings text boxes with saved propery values
        /// </summary>
        private void InitializeSettings()
        {
            List<TextBox> textBoxes = new List<TextBox>();

            // Get all the settings text boxes
            foreach (Control control in stack_settings.Children)
            {
                if (control is TextBox)
                {
                    textBoxes.Add((TextBox)control);
                }
            }

            // Check all the settings text boxes and fill in the applicable value from saved properties
            foreach (SettingsProperty property in Settings.Default.Properties)
            {
                foreach (TextBox textBox in textBoxes)
                {
                    if (textBox.Tag != null)
                    {
                        if (property.Name == textBox.Tag.ToString())
                        {
                            textBox.Text = Settings.Default[property.Name].ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sets the width and height settings and saves to the settings registry
        /// </summary>
        private void setAspectRatio(string width, string height, object? sender = null)
        {
            txt_Width.Text = width;
            txt_Height.Text = height;
            SaveAllSettings();

            if (sender != null)
            {
                foreach (Control control in stack_aspectRatio.Children)
                {
                    if (control is Button)
                    {
                        ((Button)control).Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
                    }
                }
            ((Button)sender).Background = new SolidColorBrush(Colors.AliceBlue);
            }
        }

        /// <summary>
        /// 1:1
        /// </summary>
        private void btn_11_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] widthheight = Settings.Default["AspectRatio11"].ToString().Split(':');
                setAspectRatio(widthheight[0], widthheight[1], sender);
            }
            catch { }
        }

        /// <summary>
        /// 3:2
        /// </summary>
        private void btn_32_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] widthheight = Settings.Default["AspectRatio32"].ToString().Split(':');
                setAspectRatio(widthheight[0], widthheight[1], sender);
            }
            catch { }
        }

        /// <summary>
        /// 16:9
        /// </summary>
        private void btn_169_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] widthheight = Settings.Default["AspectRatio169"].ToString().Split(':');
                setAspectRatio(widthheight[0], widthheight[1], sender);
            }
            catch { }
        }

        /// <summary>
        /// 2:3
        /// </summary>
        private void btn23_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] widthheight = Settings.Default["AspectRatio23"].ToString().Split(':');
                setAspectRatio(widthheight[0], widthheight[1], sender);
            }
            catch { }
        }

        /// <summary>
        /// 9:20
        /// </summary>
        private void btn920_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] widthheight = Settings.Default["AspectRatio920"].ToString().Split(':');
                setAspectRatio(widthheight[0], widthheight[1], sender);
            }
            catch { }
        }

        private void slider_imagePromptWeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_ImagePromptWeight.Text = Math.Round(slider_imagePromptWeight.Value, 3).ToString();
        }

        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            txt_ImagePrompt.Text = "";
        }

        /// <summary>
        /// Detects the enter key and starts rendering when pressed, works while any control is focused
        /// </summary>
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                StartRendering();
            }
        }

        private void btn_BrowseFolder_Click(object sender, RoutedEventArgs e)
        {
            using (var FolderSelector = new FolderBrowserDialog())
            {
                DialogResult result = FolderSelector.ShowDialog();

                if (!string.IsNullOrWhiteSpace(FolderSelector.SelectedPath))
                {
                    txt_PromptFolder.Text = FolderSelector.SelectedPath;
                }
            }
        }

        private void btn_StartPromptFolder_Click(object sender, RoutedEventArgs e)
        {
            StartPromptFolderExperiment(txt_PromptFolder.Text);
        }

        void StartPromptFolderExperiment(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath);

            foreach (string file in files)
            {
                txt_ImagePrompt.Text = $"\"{file}\"";
                StartRendering();
            }
        }
        private void btn_AddPromptPreset_Click(object sender, RoutedEventArgs e)
        {
            // add the prompt helper editor text boxes and buttons
            StackPanel stackPanel = new StackPanel
            {
                Orientation = System.Windows.Controls.Orientation.Horizontal,
                //Tag = helperMenuItem
            };
            TextBox textBox = new TextBox
            {
                Text = "",
                FontSize = 16,
                Padding = new Thickness(2),
                Margin = new Thickness(4),
                Width = 420,
                //Tag = menuHeader
            };
            textBox.TextChanged += PromptPresetTextChanged;
            Button btn_Delete = new Button
            {
                Content = "Delete",
                Padding = new Thickness(2),
                Margin = new Thickness(4),
                Tag = stackPanel
            };
            btn_Delete.Click += Btn_Delete_Click;
            stackPanel.Children.Add(textBox);
            stackPanel.Children.Add(btn_Delete);
            stack_PromptHelperPresets.Children.Add(stackPanel);
        }
    }
}
