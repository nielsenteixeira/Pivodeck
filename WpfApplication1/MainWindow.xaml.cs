using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Pivodeck.Core;
using PivotalTracker.FluentAPI.Domain;

namespace Pivodeck
{
    public partial class MainWindow
    {
        private readonly ExtendedNotifyIcon _extendedNotifyIcon;

        private readonly Storyboard _gridFadeInStoryBoard;
        private readonly Storyboard _gridFadeOutStoryBoard;
        public static PivotalNotifier _pivotalNotifier;

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

        void _pivotalNotifier_OnCreatedTask(Story story)
        {
            Action acao = () =>
                              {
                                  extendedNotifyIcon_OnShowWindow();
                                  lblContent.Text = story.Name;
                              };
            Dispatcher.Invoke(acao);
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
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
            Close();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var window = new Config(_pivotalNotifier);
            window.ShowDialog();

            _pivotalNotifier.OnCreatedTask += _pivotalNotifier_OnCreatedTask;
            _pivotalNotifier.OnStartedTask += _pivotalNotifier_OnStartedTask;
        }

        private void _pivotalNotifier_OnStartedTask(Story story)
        {
            Action acao = () =>
            {
                extendedNotifyIcon_OnShowWindow();
                lblContent.Text = "Task iniciada: " + story.Name;
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
        }
    }
}
