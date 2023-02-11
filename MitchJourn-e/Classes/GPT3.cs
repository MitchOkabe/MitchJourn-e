using OpenAI_API;
using OpenAI_API.Completions;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MitchJourn_e.Classes
{
    class GPT3
    {
        OpenAIAPI api;

        public string CompletionResult { get; set; }

        public GPT3()
        {
            string apiKey = Properties.Settings.Default["OpenAIAPIKey"].ToString();
            if (apiKey != "")
            {
                api = new OpenAIAPI(apiKey);
            }
        }

        /// <summary>
        /// Sends a request to GPT3 in the format "{gptPrompt} {input}"
        /// </summary>
        /// <param name="input"></param>
        /// <param name="gptPrompt"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task PromptToGPT(string gptPrompt, string input)
        {
            string prompt = $"Using only nouns and adjectives, describe a highly detailed image graphic with the following metadata: {input}";
            string returnedRequest = "";
            string output = "";
            
            if (gptPrompt != "")
            {
                prompt = $"{gptPrompt} {input}";
            }

            if (Properties.Settings.Default["OpenAIAPIKey"].ToString() != "")
            {
                try
                {
                    await api.Completions.StreamCompletionAsync(
                    new CompletionRequest(prompt: prompt, model: Model.DavinciText, max_tokens: 64, temperature: 0.7, top_p: 1, numOutputs: 1, presencePenalty: null, frequencyPenalty: 0,
                    logProbs: null, echo: false, stopSequences: null),
                    res => returnedRequest += res.ToString());

                    // for example
                    await foreach (var token in api.Completions.StreamCompletionEnumerableAsync(new CompletionRequest(prompt, Model.DavinciText, 200, 0.5, presencePenalty: 0.1, frequencyPenalty: 0.1)))
                    {
                        string safeToken = SafeString(token);
                        if (safeToken != "" && safeToken != "\n")
                        {
                            Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                                ((MainWindow)Application.Current.MainWindow).txt_PromptHelper.Text += safeToken;
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


        //public async System.Threading.Tasks.Task ElaborateSlow(string input)
        //{
        //    string prompt = $"Using only nouns and adjectives, describe with detail from the perspective of image graphics: {input}";
        //    string returnedRequest = "";
        //    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        //    PromptBubble promptBubble = new PromptBubble();
        //    await api.Completions.StreamCompletionAsync(
        //    new CompletionRequest(prompt: prompt, model: Model.DavinciText, max_tokens: 256, temperature: 0.7, top_p: 1, numOutputs: 1, presencePenalty: 0.5, frequencyPenalty: 0.1,
        //    logProbs: null, echo: false, stopSequences: null),
        //    res => returnedRequest += res.ToString());
        //    CompletionResult = returnedRequest;
        //    mainWindow.txt_PromptHelper.Text += mainWindow.CleanPrompt(CompletionResult);

        //    //mainWindow.wrp_PromptBubbles.Children.Add(promptBubble.CreatePromptBubble(mainWindow.CleanPrompt(CompletionResult)));
        //}

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
