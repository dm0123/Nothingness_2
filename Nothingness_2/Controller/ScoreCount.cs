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
        public event EventHandler ClearEvent;

        public ScoreCount()
        {
        }

        public bool Store()
        {
            return true;
        }

        public void Add()
        {
            currentScore += 10 * (currentScore + 3) - currentScore * 9 + rnd.Next(64);
            ScoreEvent(this, new ScoresEventArgs(currentScore.ToString()));
        }

        public void OnAddPersonEvent(object sender, ScoresEventArgs e)
        {
            XMLWorker.Instance.Save(e.Message, currentScore.ToString());
            ClearEvent(this, new EventArgs());
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
