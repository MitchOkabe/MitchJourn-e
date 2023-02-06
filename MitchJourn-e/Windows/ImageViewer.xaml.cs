using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// New window that will show the latest generated image in fullscreen stretched to window size
    /// </summary>
    public partial class ImageViewer : Window
    {
        readonly MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        bool displayFirstImage = true;
        public ImageViewer()
        {
            InitializeComponent();
            mainWindow.imageViewer = this;
            DisplayLastImage();
        }

        //public ImageViewer(BitmapImage image)
        //{
        //    InitializeComponent();
        //    mainWindow.imageViewer = this;
        //    DisplayImage(image);
        //}

        /// <summary>
        /// Displays the last image in the MainWindow's image stack
        /// </summary>
        private void DisplayLastImage()
        {
            ItemsControl stackImages = mainWindow.stack_Images;

            if (stackImages.HasItems == true)
            {
                if (stackImages.Items.GetItemAt(0) != null)
                {
                    mainImage.Source = ((Image)stackImages.Items.GetItemAt(0)).Source;

                    if (displayFirstImage)
                    {
                        this.Width = mainImage.Width;
                        this.Height = mainImage.Height;
                        displayFirstImage = false;
                    }
                }
                
            }
        }

        /// <summary>
        /// Displays a specified image with a predefied upscale factor
        /// </summary>
        /// <param name="bitmapImageInput"></param>
        /// <param name="scale">Upscale image multiplier. Similar to stretching the image, has no effect in full screen</param>
        public void DisplayImage(BitmapImage bitmapImageInput, RenderedImage renderedImage, double scale = 1)
        {
            // recreate the image at the upscaled size
            var bitmapImage = new TransformedBitmap(bitmapImageInput,
            new ScaleTransform(
                (bitmapImageInput.PixelWidth * scale) / bitmapImageInput.PixelWidth,
                (bitmapImageInput.PixelHeight * scale) / bitmapImageInput.PixelHeight));

            mainImage.Source = bitmapImage;

            if (displayFirstImage)
            {
                this.Width = mainImage.Width;
                this.Height = mainImage.Height;
                displayFirstImage = false;
            }

            // Add the right click menu to the image
            ContextMenu rightClickMenu = new ContextMenu();

            mainImage.ContextMenu = rightClickMenu;

            // Create Variation
            MenuItem menuItemCreateVariation = new MenuItem();
            menuItemCreateVariation.Header = "Create Variations";
            menuItemCreateVariation.Tag = renderedImage;
            menuItemCreateVariation.Click += mainWindow.MenuItemCreateVariation_Click;
            rightClickMenu.Items.Add(menuItemCreateVariation);

            // Recreate prompt
            MenuItem menuItemRecreatePrompt = new MenuItem();
            menuItemRecreatePrompt.Header = "Recreate Prompt";
            menuItemRecreatePrompt.Tag = renderedImage;
            menuItemRecreatePrompt.Click += mainWindow.MenuItemRecreatePrompt_Click;
            rightClickMenu.Items.Add(menuItemRecreatePrompt);

            // Use as Image-To-Image
            MenuItem menuItemImageToImage = new MenuItem();
            menuItemImageToImage.Header = "Use as Image-To-Image";
            menuItemImageToImage.Tag = renderedImage;
            menuItemImageToImage.Click += mainWindow.menuItemImageToImage_Click;
            rightClickMenu.Items.Add(menuItemImageToImage);

            // ----
            rightClickMenu.Items.Add(new Separator());

            // Save As
            MenuItem menuItemSaveAs = new MenuItem();
            menuItemSaveAs.Header = "Save As";
            menuItemSaveAs.Tag = renderedImage.filePath;
            menuItemSaveAs.Click += mainWindow.MenuItemSaveAs_Click;
            rightClickMenu.Items.Add(menuItemSaveAs);

            // Get File Path
            MenuItem menuItemGetFilePath = new MenuItem();
            menuItemGetFilePath.Header = "Get File Path";
            menuItemGetFilePath.Tag = renderedImage.filePath;
            menuItemGetFilePath.Click += mainWindow.MenuItemGetFilePath_Click;
            rightClickMenu.Items.Add(menuItemGetFilePath);

            // Open Containing Folder
            MenuItem menuItemOpenContainingFolder = new MenuItem();
            menuItemOpenContainingFolder.Header = "Open Containing Folder";
            menuItemOpenContainingFolder.Tag = renderedImage.filePath;
            menuItemOpenContainingFolder.Click += mainWindow.MenuItemOpenContainingFolder_Click;
            rightClickMenu.Items.Add(menuItemOpenContainingFolder);

            // ----
            rightClickMenu.Items.Add(new Separator());

            // Delete
            MenuItem menuItemDelete = new MenuItem();
            menuItemDelete.Header = "Delete";
            menuItemDelete.Tag = renderedImage;
            menuItemDelete.Click += mainWindow.MenuItemDelete_Click;
            rightClickMenu.Items.Add(menuItemDelete);
        }

    }
}
