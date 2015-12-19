using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nothingness_2.Model
{
    public class Shape
    {
        public enum Type { I, J, L, O, S, T, Z };
        public List<Block> blocks;
        public bool destroyed = false;

        

        private Type _type;
        private int x;
        private int y;

        public int Screen_X
        {
            set
            {
                this.x = value;
            }
            get
            {
                return this.x;
            }
        }

        public int Screen_Y
        {
            set
            {
                this.y = value;
            }
            get
            {
                return this.y;
            }
        }

        public Type type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public Shape(Type type)
        {
            SetState(type);
        }

        public void SetState(Type type)
        {
            blocks = ShapeBuilder.getShapeByType(type);
            this.type = type;
        }

        public void ResetState()
        {
            foreach (var block in blocks)
            {
                block.Reset();
            }
        }

        public bool move(int to_x, int to_y)
        {
            foreach (var block in blocks)
            {
                try
                {
                    block.X += to_x;
                    block.Y += to_y;
                }
                catch (IndexOutOfRangeException)
                {
                }
            }
            return true;
        }
    }
}
