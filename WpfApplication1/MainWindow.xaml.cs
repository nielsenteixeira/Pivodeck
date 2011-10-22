using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Pivodeck.Core;
using Pivodeck.Core.Extensions;
using PivotalTracker.FluentAPI.Domain;

namespace Pivodeck
{
    public partial class MainWindow
    {
        private readonly ExtendedNotifyIcon _extendedNotifyIcon;

        private readonly Storyboard _gridFadeInStoryBoard;
        private readonly Storyboard _gridFadeOutStoryBoard;
        private PivotalNotifier _pivotalNotifier;

        public MainWindow()
        {
            _pivotalNotifier = null;
            _extendedNotifyIcon = new ExtendedNotifyIcon();
            _extendedNotifyIcon.MouseLeave += extendedNotifyIcon_OnHideWindow;
            _extendedNotifyIcon.MouseMove += extendedNotifyIcon_OnShowWindow;
            _extendedNotifyIcon.targetNotifyIcon.Text = Properties.Resources.MainWindow_MainWindow_Popup_Text;
            SetNotifyIcon("Red");

            InitializeComponent();

            SetWindowToBottomRightOfScreen();
            Opacity = 0;
            uiGridMain.Opacity = 0;

            _gridFadeOutStoryBoard = (Storyboard)TryFindResource("gridFadeOutStoryBoard");
            _gridFadeOutStoryBoard.Completed += GridFadeOutStoryBoardCompleted;
            _gridFadeInStoryBoard = (Storyboard)TryFindResource("gridFadeInStoryBoard");
            _gridFadeInStoryBoard.Completed += GridFadeInStoryBoardCompleted;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Configurado)
                InitializePivotalNotifier();
            else
            {
                MessageBox.Show("Não foi detectado nenhuma configuração de projeto, é necessário a configurção para o funcionamento do programa", "Pivodeck", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                ButtonClick(sender, e);
            }
        }

        private void SetNotifyIcon(string iconPrefix)
        {
            Stream iconStream = Application.GetResourceStream(new Uri("pack://application:,,/Images/" + iconPrefix + "Orb.ico")).Stream;
            _extendedNotifyIcon.targetNotifyIcon.Icon = new Icon(iconStream);
        }

        private void SetWindowToBottomRightOfScreen()
        {
            Left = SystemParameters.WorkArea.Width - Width - 10;
            Top = SystemParameters.WorkArea.Height - Height;
        }

        private void extendedNotifyIcon_OnShowWindow()
        {
            _gridFadeOutStoryBoard.Stop();
            Opacity = 1; // Show the window (backing)
            Topmost = true;
            if (uiGridMain.Opacity > 0 && uiGridMain.Opacity < 1)
            {
                uiGridMain.Opacity = 1;
            }
            else if (uiGridMain.Opacity == 0)
            {
                _gridFadeInStoryBoard.Begin();
            }
        }

        private void extendedNotifyIcon_OnHideWindow()
        {
            _gridFadeInStoryBoard.Stop(); // Stop the fade in storyboard if running.

            if (uiGridMain.Opacity == 1 && Opacity == 1)
                _gridFadeOutStoryBoard.Begin();
            else // Just hide the window and grid
            {
                uiGridMain.Opacity = 0;
                Opacity = 0;
            }
        }

        private void UiWindowMainNotificationMouseEnter(object sender, MouseEventArgs e)
        {
            _extendedNotifyIcon.StopMouseLeaveEventFromFiring();
            _gridFadeOutStoryBoard.Stop();
            uiGridMain.Opacity = 1;
        }

        private void UiWindowMainNotificationMouseLeave(object sender, MouseEventArgs e)
        {
            extendedNotifyIcon_OnHideWindow();
        }

        private void GridFadeOutStoryBoardCompleted(object sender, EventArgs e)
        {
            Opacity = 0;
        }

        private void GridFadeInStoryBoardCompleted(object sender, EventArgs e)
        {
            Opacity = 1;
        }

        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            //if (PinButton.IsChecked == true)
            //    PinImage.Source = new BitmapImage(new Uri("pack://application:,,/Images/Pinned.png"));
            //else
            //    PinImage.Source = new BitmapImage(new Uri("pack://application:,,/Images/Un-Pinned.png"));
        }

        private void colourRadioButton_Click(object sender, RoutedEventArgs e)
        {
            SetNotifyIcon(((RadioButton)sender).Tag.ToString());
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            _extendedNotifyIcon.Dispose();
            _pivotalNotifier.Dispose();
            Close();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var window = new Config();
            window.ShowDialog();

            InitializePivotalNotifier();
        }

        private void InitializePivotalNotifier()
        {
            _pivotalNotifier = new PivotalNotifier(Properties.Settings.Default.Token, Properties.Settings.Default.ProjectId)
                                   {
                                       NewTask = Properties.Settings.Default.TaskCriada,
                                       DeletedTask = Properties.Settings.Default.TaskDeletada,
                                       DeliveredTask = Properties.Settings.Default.TaskEntregue,
                                       FinishedTask = Properties.Settings.Default.TaskFinalizada,
                                       StartedTask = Properties.Settings.Default.TaskIniciada
                                   };

            _pivotalNotifier.OnCreatedTask += _pivotalNotifier_OnCreatedTask;
            _pivotalNotifier.OnStartedTask += _pivotalNotifier_OnStartedTask;
            _pivotalNotifier.OnDeletedTask += _pivotalNotifier_OnDeletedTask;
            _pivotalNotifier.OnDeliveredTask += _pivotalNotifier_OnDeliveredTask;
            _pivotalNotifier.OnFinishedTask += _pivotalNotifier_OnFinishedTask;
        }

        void _pivotalNotifier_OnFinishedTask(Story story)
        {
            ShowStory(story, "Tarefa finalizada");
        }

        void _pivotalNotifier_OnDeliveredTask(Story story)
        {
            ShowStory(story, "Tarefa entregue");
        }

        void _pivotalNotifier_OnDeletedTask(Story story)
        {
            ShowStory(story, "Tarefa deletada");
        }

        private void _pivotalNotifier_OnStartedTask(Story story)
        {
            ShowStory(story, "Tarefa iniciada");
        }

        private void _pivotalNotifier_OnCreatedTask(Story story)
        {
            ShowStory(story, "Tarefa criada");
        }

        private void ShowStory(Story story, string oQueAconteceu)
        {
            Action acao = () =>
            {
                lblAcao.Text = string.Format("{0} - Exibido em {1}", oQueAconteceu, DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss"));
                extendedNotifyIcon_OnShowWindow();
                lblNome.Text = story.Name;
                lblPontos.Text = story.Estimate.ToString();
                lblTipo.Source = new BitmapImage(new Uri(string.Format("/Images/{0}.png", (int)story.Type),UriKind.Relative));
                lblDescricao.Text = story.Description;

                if (story.Labels.Any())
                    foreach (var label in story.Labels)
                        lblLabels.Text += string.Format("{0},", label);
                else
                    lblLabels.Text = string.Empty;

                if (story.Notes.Any())
                    foreach (var note in story.Notes)
                        lblComentarios.Text += note.ToMyString();
                else
                    lblComentarios.Text = string.Empty;

                if (story.Tasks.Any())
                    foreach (var task in story.Tasks)
                        lblTask.Text += task.Description + "\n";
                else
                    lblTask.Text = string.Empty;
                var timer = new Timer(5000);
                timer.Start();
                timer.Elapsed += (x, y) => Dispatcher.Invoke(new Action(() =>
                                                                            {
                                                                                extendedNotifyIcon_OnHideWindow();
                                                                                timer.Stop();
                                                                            }));
            };
            Dispatcher.Invoke(acao);
        }

        private void BtnCreateTaskClick(object sender, RoutedEventArgs e)
        {
            var window = new CreateTask(_pivotalNotifier);
            window.ShowDialog();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _extendedNotifyIcon.Dispose();
            _pivotalNotifier.Dispose();
        }
    }
}
