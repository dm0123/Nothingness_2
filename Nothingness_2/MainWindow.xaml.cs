using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Nothingness_2.Controller;

namespace Nothingness_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public event EventHandler canCreateWindow;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            Game.Instance.Win = this;
            dispatcherTimer.Tick += new EventHandler(Game.Instance.Run);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            canCreateWindow += Game.Instance.Screen.CreateWin;
            canCreateWindow(this, new EventArgs());
            this.buttonNew.IsEnabled = false;
        }

        private void buttonPause_Click(object sender, RoutedEventArgs e)
        {
            if (Game.Instance.state == Game.State.Play)
                buttonPause.Content = "Продолжить";
            else
                buttonPause.Content = "Пауза";
            Game.Instance.Pause();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Screen.Background = new SolidColorBrush(Colors.Black);
        }
    }
}
