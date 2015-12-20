using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nothingness_2
{
    public class ScoreCount
    {
        private int currentScore = 0;
        private string currentName = "(None)";
        Random rnd = new Random();
        public delegate void ScoresEventHandler(object sender, ScoresEventArgs e);

        public event ScoresEventHandler ScoreEvent;

        public ScoreCount()
        {
        }

        public bool Store()
        {
            return true;
        }

        public void Add()
        {
            currentScore += 10 * (currentScore + 3) - currentScore * 8 + rnd.Next(64);
            ScoreEvent(this, new ScoresEventArgs(currentScore.ToString()));
        }

       
    }

    public class ScoresEventArgs : EventArgs
    {
        public ScoresEventArgs(string message)
        {
            msg = message;
        }
        private string msg;

        public string Message { get { return msg; } }
    }
}
