using System.Windows;
using Pivodeck.Core;

namespace Pivodeck
{
    public partial class Config
    {
        public Config(PivotalNotifier pivotalNotifier)
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtToken.Text))
                MainWindow._pivotalNotifier = new PivotalNotifier(txtToken.Text, int.Parse(txtProjeto.Text));

            MainWindow._pivotalNotifier.NewTask = cbCriada.IsChecked.Value;
            MainWindow._pivotalNotifier.DeletedTask = cbDeletada.IsChecked.Value;
            MainWindow._pivotalNotifier.DeliveredTask = cbEntregue.IsChecked.Value;
            MainWindow._pivotalNotifier.FinishedTask = cbFinalizada.IsChecked.Value;
            MainWindow._pivotalNotifier.StartedTask = cbIniciada.IsChecked.Value;

            Close();
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
