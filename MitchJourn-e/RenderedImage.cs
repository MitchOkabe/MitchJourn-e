using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MitchJourn_e
{
    /// <summary>
    /// Used for storing all the prompt info for creating an image, and the image itself
    /// </summary>
    class RenderedImage
    {
        public Image image;
        public string filePath;
        public string prompt;
        public string promptWeight;
        public string seed;
        public string diffusionSteps;
        public string width;
        public string height;
        public string imagePrompt;
        public string imagePromptWeight;
        public bool WasUpscalled;

        public RenderedImage(){}

        public RenderedImage CreateRenderedImage(Image image, string filePath, string prompt, string promptWeight, string seed, string diffusionSteps, string width, string height, string imagePrompt, string ImagePromptWeight, bool WasUpscalled = false)
        {
            this.image = image;
            this.filePath = filePath;
            this.prompt = prompt;
            this.promptWeight = promptWeight;
            this.seed = seed;
            this.diffusionSteps = diffusionSteps;
            this.width = width;
            this.height = height;
            this.imagePrompt = imagePrompt;
            this.imagePromptWeight = ImagePromptWeight;
            this.WasUpscalled = WasUpscalled;
            return this;
        }
    }
}
