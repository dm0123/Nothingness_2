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
        public event KeyEventHandler inputEvent;
        public ScoreCount.ScoresEventHandler AddPersonEvent;
        System.Windows.Threading.DispatcherTimer dispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();
            inputEvent += Game.Instance.Input.OnMoveEvent;
        }

        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            Game.Instance.Win = this;
            Game.Instance.GameOverEvent -= OnGameOver;
            Game.Instance.Scores.ScoreEvent -= OnScoreEvent;
            Game.Instance.GameOverEvent += OnGameOver;
            Game.Instance.Scores.ScoreEvent += OnScoreEvent;
            Game.Instance.Scores.ClearEvent -= OnClearScores;
            Game.Instance.Scores.ClearEvent += OnClearScores;
            AddPersonEvent -= Game.Instance.Scores.OnAddPersonEvent;
            AddPersonEvent += Game.Instance.Scores.OnAddPersonEvent;
            dispatcherTimer.Tick += new EventHandler(Game.Instance.Run);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
            canCreateWindow += Game.Instance.Screen.CreateWin;
            
            canCreateWindow(this, new EventArgs());
            buttonNew.IsEnabled = false;
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            inputEvent(sender, e);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            KeyEventArgs args = new KeyEventArgs(e.KeyboardDevice, e.InputSource, e.Timestamp, Key.None);
            inputEvent(sender, args);
        }

        public void OnGameOver(object sender, EventArgs e)
        {
            buttonNew.IsEnabled = true;
            //MessageBox.Show("Game over!");
            dispatcherTimer.Stop();
            Records recordsDlg = new Records();
            recordsDlg.Owner = this;
            recordsDlg.ShowDialog();
        }

        public void OnScoreEvent(object sender, ScoresEventArgs e)
        {
            Scores.Content = "Очки: " + e.Message;
        }

        public void OnClearScores(object sender, EventArgs e)
        {
            Scores.Content = "Очки:";
        }

        public void OnPersonStoreScore(object sender, Records.PersonEventArgs e)
        {
            AddPersonEvent(sender, new ScoresEventArgs(e.Message));
        }
    }
}
