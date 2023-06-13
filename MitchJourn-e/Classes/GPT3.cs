using Microsoft.VisualBasic;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Completions;
using OpenAI_API.Models;
using OpenAI_API.Moderation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MitchJourn_e.Classes
{
    /// <summary>
    /// Used for getting OpenAI GPT3 completion results.
    /// </summary>
    class GPT3
    {
        OpenAIAPI api;
        MainWindow mainWindow = ((MainWindow)Application.Current.MainWindow);
        //public string CompletionResult { get; set; }

        public GPT3()
        {
            InitializeAPI();
        }

        void InitializeAPI()
        {
            try
            {
                string apiKey = Properties.Settings.Default["OpenAIAPIKey"].ToString();

                if (apiKey != "")
                {
                    api = new OpenAIAPI(apiKey);
                }
                else
                {
                    apiKey = mainWindow.txt_OpenAIAPIKey.Text;
                    if (apiKey != "")
                    {
                        api = new OpenAIAPI(apiKey);
                    }
                    else
                    {
                        Console.WriteLine("Failed to initialize OpenAI API key.");
                        api = new();
                    }
                }
            }
            catch
            {
                Console.WriteLine("Failed to initialize OpenAI API key.");
                api = new();
            }
        }

        /// <summary>
        /// Sends a request to 
        /// 
        /// 3 in the format "{gptPrompt} {input}"
        /// </summary>
        /// <param name="input">Input from the user.</param>
        /// <param name="gptPrompt">Prompt/directions for the gpt3 model in plain English.</param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task PromptToGPT(string gptPrompt, string input, bool isNegativePrompt = false)
        {
            string prompt = $"Using only nouns and adjectives, describe a highly detailed image graphic with the following metadata: {input}" +
                $" Example output: beautiful highly detailed incredible high-contrast" +
                $"Output:";
            string returnedRequest = "";
            string output = "";
            
            if (gptPrompt != "")
            {
                prompt = $"{gptPrompt} {input}. Result:";
            }

            if (Properties.Settings.Default["OpenAIAPIKey"].ToString() != "")
            {
                try
                {
                    //await api.Completions.StreamCompletionAsync(
                    //new CompletionRequest(prompt: prompt, model: Model.DavinciText, max_tokens: 64, temperature: 1, top_p: 1, numOutputs: 1, presencePenalty: 0.5, frequencyPenalty: 0.5,
                    //logProbs: null, echo: false, stopSequences: null),
                    //res => returnedRequest += res.ToString());

                    StackPanel promptBubble = new PromptBubble().CreatePromptBubble("",0,isNegativePrompt);
                    mainWindow.wrp_PromptBubbles.Children.Add(promptBubble);

                    await foreach (var token in api.Completions.StreamCompletionEnumerableAsync(new CompletionRequest(
                        prompt: prompt, 
                        model: Model.DavinciText, 
                        max_tokens: 128, 
                        temperature: 1, 
                        top_p: 1, 
                        numOutputs: 1, 
                        presencePenalty: 2, 
                        frequencyPenalty: 2,
                        logProbs: null, 
                        echo: false, 
                        stopSequences: null
                        )))
                    {
                        string safeToken = SafeString(token);
                        if (safeToken != "" && safeToken != "\n")
                        {
                            Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                                if (((PromptBubble)promptBubble.Tag).textPrompt.Text.Length > 130 && safeToken.Contains(' '))
                                {
                                    promptBubble = new PromptBubble().CreatePromptBubble("", 0, isNegativePrompt);
                                    mainWindow.wrp_PromptBubbles.Children.Add(promptBubble);
                                }
                                ((PromptBubble)promptBubble.Tag).textPrompt.Text += safeToken;
                            });
                        }
                    }
                } 
                catch
                {
                    MessageBox.Show("Failed to get GPT response. Check API key in advanced settings.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter your OpenAI API Key in the advanced settings before using GPT3 features.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Return a string.Empty if null
        /// </summary>
        /// <returns>A String</returns>
        private static string SafeString(object maybeString)
        {
            string output = string.Empty;

            if (maybeString != null)
            {
                output = maybeString.ToString();
            }

            return output;
        }

        public async void Chat(string gptSystemMessage, string userInput, bool isNegativePrompt, RandomWord addRandomWords)
        {
            if (api == null)
            {
                InitializeAPI();
            }

            Conversation chat = api.Chat.CreateConversation();
            StackPanel promptBubble = new PromptBubble().CreatePromptBubble("Loading...", 0, isNegativePrompt);
            mainWindow.wrp_PromptBubbles.Children.Add(promptBubble);

            try
            {
                chat.Model = new Model("gpt-4");
                chat.RequestParameters.Temperature = 0.9;

                // give instruction as System
                chat.AppendSystemMessage(gptSystemMessage);
                chat.AppendSystemMessage("Do not output things like: Im sorry, but the prompt does not make sense. Could you please provide a valid prompt for me to generate a description?");

                // give a few examples as user and assistant
                chat.AppendUserInput("Convert the following input into a prompt: (incredibly detailed high quality)0.5 ()1 [(framed ugly tiling poorly drawn out of frame disfigured deformed blurry blurred watermark grainy signature cut off draft compressed)0.5] [():1]");
                chat.AppendExampleChatbotOutput("a collection of cute and charming art, paintings, images, photography, and video various styles and mediums such as oil painting, digital art, black and white photography, stop-motion animation, etc. heartwarming, whimsical, and delightful sourced from DeviantArt, Instagram, Artstation, etc. by a variety of talented artists like Pascal Campion, Mary Blair, Rob Hodgson, and Simone Giertz. ");
                chat.AppendUserInput("Convert the following input into a prompt: (Digital art of a cat in space)1");
                chat.AppendExampleChatbotOutput("masterpiece portrait of a cute adorable cat in space digital art 8k cgsociety highly detailed dramatic lightning rim light hyperrealistic photorealistic octante render elegant cinematic intricate graphic design 4k by Carl Barks");
                chat.AppendUserInput("Convert the following input into a prompt: (Photo of a lady)1.1");
                chat.AppendExampleChatbotOutput("beautiful portrait photo of a lady natural light hyper realistic ultra-detailed no filter high depth of field f/1.4 50mm 200iso 4k film grain 8k by lois van baarle");
                //chat.AppendUserInput("(elena zay airlines redefining)0.9");
                //chat.AppendExampleChatbotOutput("highly detailed photograph of elena zay airlines redefining 8k beautiful dramatic lighting 8k photoshop");

                // Add random words to prompts about random
                if (addRandomWords != null)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        userInput += addRandomWords.GetWord();
                    }
                }

                // Add user input
                chat.AppendUserInput($"Convert the following input into a prompt: {userInput}");

                // and get the response
                string response = await chat.GetResponseFromChatbot();
                string[] allWords = response.Split(' ');
                response = "";
                foreach (string word in allWords)
                {
                    if (response.Length < 130)
                    {
                        response += $"{word} ";
                    }
                    else
                    {
                        ((PromptBubble)promptBubble.Tag).textPrompt.Text = mainWindow.CleanPrompt(response);
                        promptBubble = new PromptBubble().CreatePromptBubble("", 0, isNegativePrompt);
                        mainWindow.wrp_PromptBubbles.Children.Add(promptBubble);
                        response = "";
                    }
                }
                ((PromptBubble)promptBubble.Tag).textPrompt.Text = mainWindow.CleanPrompt(response);
            }
            catch
            {
                ((PromptBubble)promptBubble.Tag).textPrompt.Text = ("[Failed. Check settings]");
            }
        }
    }
}
