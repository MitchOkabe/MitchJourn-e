using MetadataExtractor;
using MitchJourn_e.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MitchJourn_e.Windows
{
    /// <summary>
    /// Second window to manage files after the images have left memory
    /// </summary>
    public partial class ImageSorter : Window
    {
        List<string> SortList = new List<string>();
        public ImageSorter()
        {
            InitializeComponent();
            startBackgroundSorting();
            this.Show();
        }

        /// <summary>
        /// Run the the following in the background thread
        /// </summary>
        private async void startBackgroundSorting()
        {
            await Task.Run(new Action(() => StartSorting()));
        }

        /// <summary>
        /// Sorts images based on specified prompts
        /// </summary>
        public async void StartSorting()
        {
            // Get the image output directory
            string imageDirectory = $"{Settings.Default["MainPath"]}\\outputs\\img-samples\\";

            if (System.IO.Directory.Exists(imageDirectory))
            {
                // Get all .png files
                DirectoryInfo directoryInfo = new DirectoryInfo(imageDirectory);
                FileInfo[] Files = directoryInfo.GetFiles("*.png");

                if (Files.Length > 0)
                {
                    foreach (FileInfo file in Files)
                    {
                        // Check the metadata for each png
                        List<MetadataExtractor.Directory> directories = (List<MetadataExtractor.Directory>)ImageMetadataReader.ReadMetadata(file.FullName.ToString());
                        foreach (MetadataExtractor.Directory directory in directories)
                        {
                            string[] sortList = ((string)Settings.Default["SortList"]).Split(',');
                            string metadata = "";

                            // search the prompt metadata
                            foreach (Tag tag in directory.Tags)
                            {
                                if (tag.ToString().Contains("[PNG-tEXt]"))
                                {
                                    metadata += tag.ToString().ToLower();
                                }
                            }

                            // foreach specified prompt, move file into respective folder
                            foreach (string sortItem in sortList)
                            {
                                if (metadata.Contains(sortItem.ToLower()))
                                {
                                    System.IO.Directory.CreateDirectory($"{imageDirectory}\\Sorted\\{sortItem}");
                                    string outputDirectory = $"{imageDirectory}\\Sorted\\{sortItem}\\{file.Name}";
                                    try
                                    {
                                        File.Move(file.FullName, outputDirectory);
                                        break;
                                    }
                                    catch { break; }
                                }
                            }
                        }
                    }

                    // Close window when processing is complete
                    Application.Current.Dispatcher.Invoke((Action)async delegate { Close(); });
                }
            }
        }

        /// <summary>
        /// Cancel button, closes the window
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
