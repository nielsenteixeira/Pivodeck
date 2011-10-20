using System.Windows;
using Pivodeck.Core;

namespace Pivodeck
{
    public partial class Config
    {
        private readonly PivotalNotifier pivotalNotifier;

        public Config(PivotalNotifier pivotalNotifier)
        {
            InitializeComponent();
            this.pivotalNotifier = pivotalNotifier;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtToken.Text))
                pivotalNotifier.SetToken(txtToken.Text);

            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
