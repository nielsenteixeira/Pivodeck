using System.Windows;

namespace Pivodeck.UI
{
    /// <summary>
    /// Interaction logic for CreateTask.xaml
    /// </summary>
    public partial class CreateTask : Window
    {
        public CreateTask()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
