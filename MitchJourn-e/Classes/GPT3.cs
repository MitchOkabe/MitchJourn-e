using OpenAI_API;
using OpenAI_API.Completions;
using OpenAI_API.Models;
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
        //public string CompletionResult { get; set; }

        public GPT3()
        {
            string apiKey = Properties.Settings.Default["OpenAIAPIKey"].ToString();

            if (apiKey != "")
            {
                api = new OpenAIAPI(apiKey);
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

                    MainWindow mainWindow = ((MainWindow)Application.Current.MainWindow);
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
    }
}
