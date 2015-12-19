using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nothingness_2.View;
using System.Windows.Shapes;

namespace Nothingness_2.Model
{
    public class Block
    {
        public const int SIZE = 25;

        private int x;
        private int y;

        private int initialX;
        private int initialY;

        public int Initial_X { get { return initialX; } set { initialX = value; } }
        public int Initial_Y { get { return initialY; } set { initialY = value; } }

        public bool in_use;
        public string name = "";

        public int X
        {
            set
            {
                //if (value < 0 || value > Screen.WIDTH)
                //    throw new IndexOutOfRangeException("Invalid x");
                x = value;
            }

            get
            {
                return x;
            }
        }

        public int Y
        {
            set
            {
                //if (value < 0 || value > Screen.HEIGHT)
                //    throw new IndexOutOfRangeException("Invalid y");
                y = value;
            }
            get
            {
                return y;
            }
        }

        public int Screen_X
        {
            get
            {
                return x * SIZE;
            }
        }

        public int Screen_Y
        {
            get
            {
                return y * SIZE;
            }
        }

        public Block()
        {
            this.Reset();
        }

        public Block(int relX, int relY)
        {
            X = relX;
            Y = relY;
            initialX = relX;
            initialY = relY;
        }

        public void Reset()
        {
            X = 0;
            Y = 0;
            initialX = 0;
            initialY = 0;
            in_use = false;
            name = "";
        }
    }
}
