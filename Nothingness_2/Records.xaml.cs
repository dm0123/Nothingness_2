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
using System.Windows.Shapes;

namespace Nothingness_2
{
    /// <summary>
    /// Interaction logic for Records.xaml
    /// </summary>
    public partial class Records : Window
    {
        public delegate void PersonalEventHandler(object sender, PersonEventArgs e);
        public event PersonalEventHandler StorePersonalScoresEvent;


        public class PersonEventArgs : EventArgs
        {
            public PersonEventArgs(string message)
            {
                msg = message;
            }
            private string msg;

            public string Message { get { return msg; } }
        }

        public Records()
        {
            InitializeComponent();
        }

        //public void bindEvent()
        //{
        //    StorePersonalScoresEvent += ((MainWindow)GetWindow(Parent)).OnPersonStoreScore;
        //}

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            StorePersonalScoresEvent(this, new PersonEventArgs(textBox.Text));
            Close();
        }
    }
}
