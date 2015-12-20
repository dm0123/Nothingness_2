using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nothingness_2
{
    public class Input
    {
        public enum Move {No, Left, Right, Down, Rotate };

        private Move state = Move.No;

        public Move State { get { return state; } }

        public Input()
        {
            
        }

        public void OnMoveEvent(object sender, KeyEventArgs args)
        {
            switch(args.Key)
            {
                case Key.A:
                    state = Move.Left;
                    break;
                case Key.S:
                    state = Move.Down;
                    break;
                case Key.D:
                    state = Move.Right;
                    break;
                case Key.Left:
                    state = Move.Left;
                    break;
                case Key.Down:
                    state = Move.Down;
                    break;
                case Key.Right:
                    state = Move.Right;
                    break;
                case Key.R:
                    state = Move.Rotate;
                    break;
                case Key.None:
                    state = Move.No;
                    break;
            }
        }
    }
}
