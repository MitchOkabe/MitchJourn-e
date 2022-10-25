# MitchJourn-e

Features
Text-To-Image prompting
Image-To-Image Prompting
Upscaling
Face correction (while upscaling is enabled)
Settings saved between sessions including image generation dimentions, Seed, Prompt Weight, and Upresolution scale.

Right-click menu:
Create vaiations
Save As
Open Containing Folder
Recreate Prompt
Delete File

Installation:
(alternatively follow this guide: https://github.com/invoke-ai/InvokeAI/blob/main/docs/installation/INSTALL_WINDOWS.md)

1. Install Anaconda3 (miniconda3 version) from https://docs.anaconda.com/anaconda/install/windows/
2. Install Git from https://git-scm.com/download/win
3. Launch Anaconda from the Windows Start menu. This will bring up a command window. Type all the remaining commands in this window.
4. Run the command:
git clone https://github.com/invoke-ai/InvokeAI.git

This will create stable-diffusion folder where you will follow the rest of the steps.

5. Enter the newly-created InvokeAI folder. From this step forward make sure that you are working in the InvokeAI directory!

cd InvokeAI

6.Run the following two commands:

conda env create
conda activate invokeai

This will install all python requirements and activate the "invokeai" environment which sets PATH and other environment variables properly.

Note that the long form of the first command is conda env create -f environment.yml. If the environment file isn't specified, conda will default to environment.yml. You will need to provide the -f option if you wish to load a different environment file at any point.

7. Run the command:

python scripts\preload_models.py

This installs several machine learning models that stable diffusion requires.

Note: This step is required. This was done because some users may might be blocked by firewalls or have limited internet connectivity for the models to be downloaded just-in-time.

8. Now you need to install the weights for the big stable diffusion model.

For running with the released weights, you will first need to set up an acount with Hugging Face (https://huggingface.co).
Use your credentials to log in, and then point your browser at https://huggingface.co/CompVis/stable-diffusion-v-1-4-original.
You may be asked to sign a license agreement at this point.
Click on "Files and versions" near the top of the page, and then click on the file named sd-v1-4.ckpt. You'll be taken to a page that prompts you to click the "download" link. Now save the file somewhere safe on your local machine.
The weight file is >4 GB in size, so downloading may take a while.

9. Now run the following commands from within the InvokeAI directory to copy the weights file to the right place:

mkdir -p models\ldm\stable-diffusion-v1
copy C:\path\to\sd-v1-4.ckpt models\ldm\stable-diffusion-v1\model.ckpt
Please replace C:\path\to\sd-v1.4.ckpt with the correct path to wherever you stashed this file. If you prefer not to copy or move the .ckpt file, you may instead create a shortcut to it from within models\ldm\stable-diffusion-v1\.

10. Now relaunch MitchJourn-E, and you can start generating images
