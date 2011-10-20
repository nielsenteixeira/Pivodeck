using System.Windows;
using Pivodeck.Core;

namespace Pivodeck
{
    public partial class Config
    {
        public Config()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtToken.Text))
            {
                Properties.Settings.Default.Token = txtToken.Text;
                Properties.Settings.Default.ProjectId = int.Parse(txtProjeto.Text);
                Properties.Settings.Default.TaskCriada= cbCriada.IsChecked.Value;
                Properties.Settings.Default.TaskDeletada= cbDeletada.IsChecked.Value;
                Properties.Settings.Default.TaskEntregue = cbEntregue.IsChecked.Value;
                Properties.Settings.Default.TaskFinalizada = cbFinalizada.IsChecked.Value;
                Properties.Settings.Default.TaskIniciada= cbIniciada.IsChecked.Value;
                Properties.Settings.Default.Save();
            }

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
