using System.Linq;
using System.Windows;
using Pivodeck.Core;
using PivotalTracker.FluentAPI.Domain;

namespace Pivodeck
{
    public partial class CreateTask
    {
        private readonly PivotalNotifier _pivotalNotifier;

        public CreateTask(PivotalNotifier pivotalNotifier)
        {
            _pivotalNotifier = pivotalNotifier;
            InitializeComponent();
        }

        private void BtnOkClick(object sender, RoutedEventArgs e)
        {
            if (Valido())
            {
                dynamic d = cmbTipo.SelectedValue;
                var storyTypeEnum = (StoryTypeEnum) d.Value;
                _pivotalNotifier.CreateTask(txtNome.Text, storyTypeEnum, txtLabels.Text,
                                            (int) sldPontos.Value, txtDescricao.Text);
            }
            Close();
        }

        private bool Valido()
        {
            var returnValue = !string.IsNullOrWhiteSpace(txtNome.Text) &&
                              !string.IsNullOrWhiteSpace(txtProjeto.Text) &&
                              !string.IsNullOrWhiteSpace(txtLabels.Text) &&
                              !string.IsNullOrWhiteSpace(txtDescricao.Text) &&
                              cmbTipo.SelectedIndex != -1;

            return returnValue;
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
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