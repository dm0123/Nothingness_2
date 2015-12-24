using Nothingness_2.Controller;
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
    /// Interaction logic for RecordsTable.xaml
    /// </summary>
    public partial class RecordsTable : Window
    {
        public RecordsTable()
        {
            InitializeComponent();
            dataGrid.ItemsSource = XMLWorker.Instance.Load();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Game.Instance.state = Game.State.Play;
        }
    }
}
