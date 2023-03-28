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
        public TextBox textPrompt = new TextBox();
        public bool isNegativePrompt = false; 

        SolidColorBrush primaryColour = (SolidColorBrush)new BrushConverter().ConvertFrom("#fcfcff");
        SolidColorBrush secondaryColour = (SolidColorBrush)new BrushConverter().ConvertFrom("#aad7ff"); 
        SolidColorBrush negativePrimaryColour = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffecf0");
        SolidColorBrush negativeSecondaryColour = (SolidColorBrush)new BrushConverter().ConvertFrom("#c98499");
        SolidColorBrush transparent = Brushes.Transparent;
        StackPanel RootControl= new StackPanel();
        Button btnDelete = new Button();
        Button btnMoveLeft = new Button();
        Button btnMoveRight = new Button();
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public void promptBubble()
        {

        }

        public StackPanel CreatePromptBubble(string prompt = "", int power = 0, bool isNegativePrompt = false)
        {
            this.isNegativePrompt = isNegativePrompt;

            StackPanel outputStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Tag = this
            };
            RootControl = outputStackPanel;

            Card card = new Card()
            {
                Padding = new Thickness(4)
            };
            card.MouseRightButtonDown += Card_MouseRightButtonDown;

            if (!isNegativePrompt)
            {
                card.Background = secondaryColour;
            }
            else
            {
                card.Background = negativeSecondaryColour;
            }

            card.MouseEnter += Card_MouseEnter;
            card.MouseLeave += Card_MouseLeave;
            DragDrop.SetIsDragSource(card, true);
            outputStackPanel.Children.Add(card);
            StackPanel innerStack = new StackPanel { Orientation = Orientation.Horizontal };
            card.Content = innerStack;

            btnDelete = new Button
            {
                Content = "x",
                Margin = new Thickness(-5,-38,0,0),
                Padding = new Thickness(-2,-4,-2,-4),
                BorderThickness = new Thickness(0),
                IsTabStop = false,
                FontSize = 12,
                Visibility = Visibility.Hidden
            };
            btnDelete.Click += DeleteBubble_Click;
            innerStack.Children.Add(btnDelete);

            textPrompt = new TextBox
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

            if (!isNegativePrompt)
            {
                textPrompt.Background = primaryColour;
            }
            else
            {
                textPrompt.Background = negativePrimaryColour;
            }

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

            btnMoveLeft = new Button
            {
                Content = "<",
                Margin = new Thickness(-42, 36,0,-2),
                Padding = new Thickness(0),
                BorderThickness = new Thickness(0),
                IsTabStop = false,
                FontSize = 12,
                Visibility = Visibility.Hidden
            };
            btnMoveLeft.Click += BtnMoveLeft_Click;
            innerStack.Children.Add(btnMoveLeft);

            btnMoveRight = new Button
            {
                Content = ">",
                Margin = new Thickness(-24, 36, 0, -2),
                Padding = new Thickness(0),
                BorderThickness = new Thickness(0),
                IsTabStop = false,
                FontSize = 12,
                Visibility = Visibility.Hidden
            };
            btnMoveRight.Click += BtnMoveRight_Click;
            innerStack.Children.Add(btnMoveRight);

            this.prompt = prompt;
            this.power = power;

            //var getFocus = new Thread(GetFocus);
            //getFocus.Start();

            return outputStackPanel;
        }

        private void Card_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void GetFocus()
        {
            if (textPrompt != null)
            {
                Thread.Sleep(10);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    textPrompt.Focus();
                });

            }
        }

        private int GetCurrentIndex()
        {
            UIElement thisBubble = new UIElement();
            WrapPanel parent = ((WrapPanel)RootControl.Parent);
            int currentIndex = 0;

            if (parent != null)
            {

                foreach (UIElement control in parent.Children)
                {
                    if (control == RootControl)
                    {
                        thisBubble = control;
                        break;
                    }
                    currentIndex++;
                }
            }

            return currentIndex;
        }

        private void BtnMoveRight_Click(object sender, RoutedEventArgs e)
        {
            UIElement thisBubble = new UIElement();
            WrapPanel parent = ((WrapPanel)RootControl.Parent);
            int currentIndex = 0;
            foreach (UIElement control in parent.Children)
            {
                if (control == RootControl)
                {
                    thisBubble = control;
                    break;
                }
                currentIndex++;
            }

            if (currentIndex + 1 < parent.Children.Count)
            {
                parent.Children.Remove(thisBubble);
                parent.Children.Insert(currentIndex + 1, thisBubble);
            }
        }

        private void BtnMoveLeft_Click(object sender, RoutedEventArgs e)
        {
            UIElement thisBubble = new UIElement();
            WrapPanel parent = ((WrapPanel)RootControl.Parent);
            int currentIndex = 0;
            foreach (UIElement control in parent.Children)
            {
                if (control == RootControl)
                {
                    thisBubble = control;
                    break;
                }
                currentIndex++;
            }

            if (currentIndex - 1 >= 1)
            {
                parent.Children.Remove(thisBubble);
                parent.Children.Insert(currentIndex - 1, thisBubble);
            }
        }

        private void Card_MouseLeave(object sender, MouseEventArgs e)
        {
            btnDelete.Visibility = Visibility.Hidden;
            btnMoveLeft.Visibility = Visibility.Hidden;
            btnMoveRight.Visibility = Visibility.Hidden;
        }

        private void Card_MouseEnter(object sender, MouseEventArgs e)
        {
            btnDelete.Visibility = Visibility.Visible;
            btnMoveLeft.Visibility = Visibility.Visible;
            btnMoveRight.Visibility = Visibility.Visible;
        }

        private void DeleteBubble_Click(object sender, RoutedEventArgs e)
        {
            DeleteBubble();
        }

        public void DeleteBubble()
        {
            UIElement thisBubble = new UIElement();
            WrapPanel parent = ((WrapPanel)RootControl.Parent);
            foreach (UIElement control in parent.Children)
            {
                if (control == RootControl)
                {
                    thisBubble = control;
                }
            }
            parent.Children.Remove(thisBubble);

            mainWindow.txt_Prompt.Text = mainWindow.ConvertPromptBubblesToString();
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

                if (secondPrompt == "") {
                    mainWindow.RequestRendering();
                }
                else
                {
                    ((TextBox)sender).Text = mainWindow.CleanPrompt(firstPrompt, false);
                    mainWindow.wrp_PromptBubbles.Children.Insert(GetCurrentIndex() + 1, new PromptBubble().CreatePromptBubble(mainWindow.CleanPrompt(secondPrompt, false), power, isNegativePrompt));
                }
            }
        }

        //public void UpdatePrompt()
        //{
        //    TextBox textBox = ((TextBox)sender);
        //    string fullPrompt = textBox.Text;
        //    string firstPrompt = "";
        //    string secondPrompt = "";

        //    for (int i = 0; i < fullPrompt.Length; i++)
        //    {
        //        if (i < textBox.SelectionStart)
        //        {
        //            firstPrompt += fullPrompt[i];
        //        }
        //        else
        //        {
        //            secondPrompt += fullPrompt[i];
        //        }
        //    }
        //        ((TextBox)sender).Text = firstPrompt;
        //    mainWindow.wrp_PromptBubbles.Children.Add(new PromptBubble().CreatePromptBubble(secondPrompt));
        //}

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
            //if (prompt == "")
            //{
            //    int totalBubbles = 0;
            //    WrapPanel parent = ((WrapPanel)RootControl.Parent);
            //    foreach (UIElement control in parent.Children)
            //    {
            //        if (control == RootControl)
            //        {
            //            thisBubble = control;
            //        }
            //        if (control is StackPanel)
            //        {
            //            totalBubbles++;
            //        }
            //    }
            //    if (totalBubbles != 1)
            //    {
            //        parent.Children.Remove(thisBubble);
            //    }
            //}
            
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
