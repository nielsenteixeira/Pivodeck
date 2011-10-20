using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using PivotalTracker.FluentAPI.Domain;
using Pivodeck.Core;

namespace Pivodeck
{
    public partial class MainWindow : Window
    {
        private readonly ExtendedNotifyIcon extendedNotifyIcon;

        private readonly Storyboard gridFadeInStoryBoard;
        private readonly Storyboard gridFadeOutStoryBoard;
        private readonly PivotalNotifier pivotalNotifier;

        public MainWindow()
        {
            extendedNotifyIcon = new ExtendedNotifyIcon();
            extendedNotifyIcon.MouseLeave += extendedNotifyIcon_OnHideWindow;
            extendedNotifyIcon.MouseMove += extendedNotifyIcon_OnShowWindow;
            extendedNotifyIcon.targetNotifyIcon.Text = "Popup Text";
            SetNotifyIcon("Red");

            InitializeComponent();

            SetWindowToBottomRightOfScreen();
            Opacity = 0;
            uiGridMain.Opacity = 0;

            gridFadeOutStoryBoard = (Storyboard)TryFindResource("gridFadeOutStoryBoard");
            gridFadeOutStoryBoard.Completed += gridFadeOutStoryBoard_Completed;
            gridFadeInStoryBoard = (Storyboard)TryFindResource("gridFadeInStoryBoard");
            gridFadeInStoryBoard.Completed += gridFadeInStoryBoard_Completed;

            pivotalNotifier = new PivotalNotifier();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void SetNotifyIcon(string iconPrefix)
        {
            Stream iconStream = Application.GetResourceStream(new Uri("pack://application:,,/Images/" + iconPrefix + "Orb.ico")).Stream;
            extendedNotifyIcon.targetNotifyIcon.Icon = new Icon(iconStream);
        }

        private void SetWindowToBottomRightOfScreen()
        {
            Left = SystemParameters.WorkArea.Width - Width - 10;
            Top = SystemParameters.WorkArea.Height - Height;
        }

        private void extendedNotifyIcon_OnShowWindow()
        {
            gridFadeOutStoryBoard.Stop();
            Opacity = 1; // Show the window (backing)
            Topmost = true;
            if (uiGridMain.Opacity > 0 && uiGridMain.Opacity < 1)
            {
                uiGridMain.Opacity = 1;
            }
            else if (uiGridMain.Opacity == 0)
            {
                gridFadeInStoryBoard.Begin();
            }
        }

        private void extendedNotifyIcon_OnHideWindow()
        {
            gridFadeInStoryBoard.Stop(); // Stop the fade in storyboard if running.

            if (uiGridMain.Opacity == 1 && Opacity == 1)
                gridFadeOutStoryBoard.Begin();
            else // Just hide the window and grid
            {
                uiGridMain.Opacity = 0;
                Opacity = 0;
            }
        }

        private void uiWindowMainNotification_MouseEnter(object sender, MouseEventArgs e)
        {
            extendedNotifyIcon.StopMouseLeaveEventFromFiring();
            gridFadeOutStoryBoard.Stop();
            uiGridMain.Opacity = 1;
        }

        private void uiWindowMainNotification_MouseLeave(object sender, MouseEventArgs e)
        {
            extendedNotifyIcon_OnHideWindow();
        }

        private void gridFadeOutStoryBoard_Completed(object sender, EventArgs e)
        {
            Opacity = 0;
        }

        private void gridFadeInStoryBoard_Completed(object sender, EventArgs e)
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            extendedNotifyIcon.Dispose();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new Config(pivotalNotifier);
            window.ShowDialog();
        }

        private void btnCreateTask_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateTask();
            window.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            extendedNotifyIcon.Dispose();
        }
    }
}
