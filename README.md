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
(alternatively follow this guide: https://github.com/lstein/stable-diffusion/wiki/Easy-peasy-Windows-install)
1. Download Zip file and extract root directory somewhere or clone respository.
2. Run Setup.exe from the extracted folder. Part of the setup may fail, that's fine.
3. Download Zip file and extract rood directoy https://github.com/lstein/stable-diffusion or clone respository.
4. Download and install Python https://www.python.org/ftp/python/3.10.7/python-3.10.7-amd64.exe
use all the default installation options
5. Download and install Anaconda https://repo.anaconda.com/archive/Anaconda3-2022.05-Windows-x86_64.exe. Use the default installation options.
6. From the start menu, run Anaconda Prompt (anaconda3) and enter this command:
cd (the file path that you extracted step 3 to example: cd C:\Users\Mitch\Desktop\stable-diffusion-main)
7. run the command: conda env create -f environment.yaml
8. Run the command: activate ldm

