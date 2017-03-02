using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Launchy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        private readonly CommandHandler handler;
        private RegisterKeyBinder keybinder;
        private List<string> previousCommands = new List<string>();
        int currentIndex = -1;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.SearchBox.KeyUp += SearchBox_KeyDown;
            this.handler = new CommandHandler();
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                this.previousCommands.Add(this.SearchBox.Text);
                this.handler.Parse(this.SearchBox.Text);
                this.SearchBox.Text = string.Empty;
                this.Visibility = Visibility.Hidden;
                currentIndex = -1;
            }
            else if(e.Key == Key.Up)
            {
                this.GoBackInHistory();
            }
            else if (e.Key == Key.Down)
            {
                this.GoForwardInHistory();
            }
            else
            {
                string bestMatch = handler.FuzzyMatch(this.SearchBox.Text);
                this.SuggestionBox.Text = bestMatch;
            }
        }

        private void GoBackInHistory()
        {
            if (previousCommands.Count == 0)
            {
                return;
            }

            // If we are at the top set the pointer to last command
            if (currentIndex == -1)
            {
                currentIndex = previousCommands.Count - 1;
            }
            else
            {
                // Else decrement
                currentIndex--;
            }

            // If we've gone too low then get back to safety and return
            if (currentIndex < 0)
            {
                currentIndex++;
                return;
            }

            this.SetSearchBoxText(previousCommands[currentIndex]);
        }

        private void GoForwardInHistory()
        {            
            // If we haven't gone back yet we can't go forward so stop
            if (currentIndex == -1)
            {
                if (!string.IsNullOrEmpty(this.SuggestionBox.Text))
                {
                    this.SetSearchBoxText(this.SuggestionBox.Text);
                }

                return;
            }
            else
            {
                // Else we can go forward
                currentIndex++;
            }

            if (previousCommands.Count == 0)
            {
                return;
            }

            // If we are at the top then get rid of text and set index to top
            if (currentIndex == this.previousCommands.Count)
            {
                this.currentIndex = -1;
                this.SearchBox.Text = string.Empty;
                return;
            }

            this.SetSearchBoxText(previousCommands[currentIndex]);
        }

        private void SetSearchBoxText(string text)
        {
            this.SearchBox.Text = text;
            this.SearchBox.SelectionStart = this.SearchBox.Text.Length;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.keybinder = new RegisterKeyBinder(this);
            this.Visibility = Visibility.Hidden;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

        public void OnHotKeyKeyPressed()
        {
            this.ToggleWindow();
        }

        private void ToggleWindow()
        {
            if (this.Visibility == Visibility.Hidden || this.Visibility == Visibility.Collapsed)
            {
                this.Visibility = Visibility.Visible;
                this.SearchBox.Focus();
            }
            else
            {
                this.Visibility = Visibility.Hidden;
            }
        }
    }
}