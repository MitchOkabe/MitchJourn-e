using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Wpf.Ui.Controls;
using Brushes = System.Windows.Media.Brushes;
using Button = Wpf.Ui.Controls.Button;
using TextBox = Wpf.Ui.Controls.TextBox;
using DragDrop = GongSolutions.Wpf.DragDrop.DragDrop;

namespace MitchJourn_e.Classes
{
    internal class PromptBubble : StackPanel
    {
        public int power = 0;
        public string prompt = "";
        SolidColorBrush primaryColour = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFEFEFEF");
        SolidColorBrush transparent = Brushes.Transparent;
        StackPanel RootControl= new StackPanel();
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public void promptBubble()
        {

        }

        public StackPanel CreatePromptBubble(string prompt = "", int power = 0)
        {
            StackPanel outputStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Tag = this
            };
            RootControl = outputStackPanel;

            Card card = new Card()
            {
                Padding = new Thickness(4),
                Background = Brushes.White
            };
            DragDrop.SetIsDragSource(card, true);
            outputStackPanel.Children.Add(card);
            StackPanel innerStack = new StackPanel { Orientation = Orientation.Horizontal };
            card.Content = innerStack;

            TextBox textPrompt = new TextBox
            {
                Text = prompt,
                AutoWordSelection = true,
                FocusVisualStyle = null,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                MinHeight = 42,
                MinWidth = 42,
                Padding = new Thickness(4),
                Style = null
            };
            textPrompt.TextChanged += TextPrompt_TextChanged;
            textPrompt.SpellCheck.IsEnabled = true;
            textPrompt.GotKeyboardFocus += TextPrompt_GotKeyboardFocus;
            textPrompt.KeyDown += TextPrompt_KeyDown;
            innerStack.Children.Add(textPrompt);

            Label labelPower = new Label
            {
                Content = power,
                Margin = new Thickness(-16, 0, 0, 0),
                IsHitTestVisible = false
            };
            innerStack.Children.Add(labelPower);

            StackPanel stackPowerButtons = new StackPanel();
            innerStack.Children.Add(stackPowerButtons);

            Button buttonPowerPlus = new Button
            {
                Padding = new Thickness(2, 1, 2, 1),
                Tag = labelPower,
                Content = "+",
                IsTabStop = false
            };
            buttonPowerPlus.Click += AdjustPower_Click;
            stackPowerButtons.Children.Add(buttonPowerPlus);

            Button buttonPowerMinus = new Button
            {
                Padding = new Thickness(4, 1, 4, 1),
                Tag = labelPower,
                Content = "-",
                IsTabStop = false
            };
            buttonPowerMinus.Click += AdjustPower_Click;
            stackPowerButtons.Children.Add(buttonPowerMinus);

            this.prompt = prompt;
            this.power = power;

            return outputStackPanel;
        }

        private void TextPrompt_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                TextBox textBox = ((TextBox)sender);
                string fullPrompt = textBox.Text;
                string firstPrompt = "";
                string secondPrompt = "";

                for (int i = 0; i < fullPrompt.Length; i++)
                {
                    if (i < textBox.SelectionStart)
                    {
                        firstPrompt += fullPrompt[i];
                    }
                    else
                    {
                        secondPrompt += fullPrompt[i];
                    }
                }
                ((TextBox)sender).Text = firstPrompt;
                mainWindow.wrp_PromptBubbles.Children.Add(new PromptBubble().CreatePromptBubble(secondPrompt));
            }
        }

        private void TextPrompt_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            prompt = ((TextBox)sender).Text;
            if (prompt == "Enter your prompt here!")
            {
                ((TextBox)sender).Text = "";
            }
        }

        private void TextPrompt_TextChanged(object sender, TextChangedEventArgs e)
        {
            prompt = ((TextBox)sender).Text;

            UIElement thisBubble = new UIElement();
            if (prompt == "")
            {
                int totalBubbles = 0;
                WrapPanel parent = ((WrapPanel)RootControl.Parent);
                foreach (UIElement control in parent.Children)
                {
                    if (control == RootControl)
                    {
                        thisBubble = control;
                    }
                    if (control is StackPanel)
                    {
                        totalBubbles++;
                    }
                }
                if (totalBubbles != 1)
                {
                    parent.Children.Remove(thisBubble);
                }
            }
            
            mainWindow.txt_Prompt.Text = mainWindow.ConvertPromptBubblesToString();

        }

        private void AdjustPower_Click(object sender, RoutedEventArgs e)
        {
            Label powerLabel = (Label)((Button)sender).Tag;
            power = int.Parse(powerLabel.Content.ToString());

            if (((Button)sender).Content == "+")
            {
                if (power < 9)
                {
                    power++;
                }
            }
            else
            {
                if (power > -9)
                {
                    power--;
                }
            }

            powerLabel.Content = power;
            mainWindow.txt_Prompt.Text = mainWindow.ConvertPromptBubblesToString();
        }
    }
}
