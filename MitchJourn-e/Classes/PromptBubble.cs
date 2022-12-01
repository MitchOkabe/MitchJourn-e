using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Brushes = System.Windows.Media.Brushes;

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
                Margin = new Thickness(4),
                Tag = this
            };
            RootControl = outputStackPanel;
            
            Border borderPrompt = new Border
            {
                CornerRadius = new CornerRadius(8),
                Background = primaryColour
            };
            outputStackPanel.Children.Add(borderPrompt);

            TextBox textPrompt = new TextBox
            {
                Text = prompt,
                MinWidth = 42,
                Margin = new Thickness(8),
                FontSize = 16,
                Background = transparent,
                BorderBrush = null,
                AutoWordSelection = true,
                FocusVisualStyle = null,
                BorderThickness = new Thickness(0)
            };
            textPrompt.TextChanged += TextPrompt_TextChanged;
            textPrompt.SpellCheck.IsEnabled = true;
            textPrompt.GotKeyboardFocus += TextPrompt_GotKeyboardFocus;
            borderPrompt.Child = textPrompt;

            Label labelPower = new Label
            {
                Content = power,
                Margin = new Thickness(-19, -6, 0, 0),
                IsHitTestVisible = false
            };
            outputStackPanel.Children.Add(labelPower);

            StackPanel stackPowerButtons = new StackPanel();
            outputStackPanel.Children.Add(stackPowerButtons);

            Border borderPowerPlus = new Border
            {
                CornerRadius = new CornerRadius(8),
                Background = primaryColour,
                Margin = new Thickness(2, 0, 2, 0)
            };
            stackPowerButtons.Children.Add(borderPowerPlus);

            Button buttonPowerPlus = new Button
            {
                Content = "+",
                Background = null,
                BorderBrush = null,
                Tag = labelPower
            };
            buttonPowerPlus.Click += AdjustPower_Click;
            borderPowerPlus.Child = buttonPowerPlus;

            Border borderPowerMinus = new Border
            {
                CornerRadius = new CornerRadius(8),
                Background = primaryColour,
                Margin = new Thickness(2, 0, 2, 0)
            };
            stackPowerButtons.Children.Add(borderPowerMinus);

            Button buttonPowerMinus = new Button
            {
                Content = "-",
                Background = null,
                BorderBrush = null,
                Tag = labelPower
            };
            buttonPowerMinus.Click += AdjustPower_Click;
            borderPowerMinus.Child = buttonPowerMinus;

            this.prompt = prompt;
            this.power = power;

            return outputStackPanel;
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
