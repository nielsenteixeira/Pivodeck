using System.Windows;
using System.Linq;
using PivotalTracker.FluentAPI.Domain;
using Pivodeck.Core;
using System.Collections.Generic;

namespace Pivodeck
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var v = new EnumUtil<StoryTypeEnum>();
            cmbTipo.ItemsSource = v.ReturnAsDictionary<int>().Select(x => new
            {
                x.Key,
                x.Value,
                ImageValue = string.Format("/Images/{0}.png", x.Value)
            });
            cmbTipo.DisplayMemberPath = "Key";
            cmbTipo.SelectedValuePath = "Value";
            cmbTipo.SelectedIndex = 1;
        }
    }
}
