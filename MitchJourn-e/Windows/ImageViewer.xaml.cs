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

        public ImageViewer(BitmapImage image)
        {
            InitializeComponent();
            mainWindow.imageViewer = this;
            DisplayImage(image);
        }

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
                    Image.Source = ((Image)stackImages.Items.GetItemAt(0)).Source;

                    if (displayFirstImage)
                    {
                        this.Width = Image.Width;
                        this.Height = Image.Height;
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
        public void DisplayImage(BitmapImage bitmapImageInput, double scale = 1)
        {
            // recreate the image at the upscaled size
            var bitmapImage = new TransformedBitmap(bitmapImageInput,
            new ScaleTransform(
                (bitmapImageInput.PixelWidth * scale) / bitmapImageInput.PixelWidth,
                (bitmapImageInput.PixelHeight * scale) / bitmapImageInput.PixelHeight));

            Image.Source = bitmapImage;

            if (displayFirstImage)
            {
                this.Width = Image.Width;
                this.Height = Image.Height;
                displayFirstImage = false;
            }
        }

    }
}
