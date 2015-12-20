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
        public enum Angle { A0, A90, A180, A270 };
        public List<Block> blocks;
        public bool destroyed = false;

        private Block _leftBlock;
        private Block _rightBlock;
        private Block _topBlock;
        private Block _bottomBlock;

        public Block LeftBlock { get { return _leftBlock; } }
        public Block RightBlock { get { return _rightBlock; } }
        public Block TopBlock { get { return _topBlock; } }
        public Block BottomBlock { get { return _bottomBlock; } }



        private Type _type;
        private Angle _angle;
        private int x;
        private int y;
        private string name;
        public int center;

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

        public Angle CurrentAngle { get { return _angle; } }

        public Shape(Type type, Angle angle)
        {
            SetState(type, angle);
            _leftBlock = findLeft();
            _rightBlock = findRight();
            _topBlock = findTop();
            _bottomBlock = findBottom();
            StringBuilder nameBuilder = new StringBuilder("Shape");
            nameBuilder.Append(DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
            name = nameBuilder.ToString();
            foreach(var block in blocks)
            {
                block.name = name;
            }
        }

        private Block findLeft()
        {
            Block leftBlock = null;
            int min = 1000;
            foreach(var block in blocks)
            {
                if (block.X < min)
                {
                    leftBlock = block;
                    min = leftBlock.X;
                }
            }
            return leftBlock;
        }

        private Block findTop()
        {
            Block topBlock = null;
            int min = 1000;
            foreach (var block in blocks)
            {
                if (block.Y < min)
                {
                    topBlock = block;
                    min = topBlock.Y;
                }
                
            }
            return topBlock;
        }

        private Block findBottom()
        {
            Block bottomBlock = null;
            int max = -1000;
            foreach (var block in blocks)
            {
                if (block.Y > max)
                {
                    bottomBlock = block;
                    max = bottomBlock.Y;
                }                    
            }
            return bottomBlock;
        }

        private Block findRight()
        {
            Block rightBlock = null;
            int max = -1000;
            foreach (var block in blocks)
            {
                if (block.X > max)
                {
                    rightBlock = block;
                    max = rightBlock.X;
                }
            }
            return rightBlock;
        }

        public void SetState(Type type, Angle angle)
        {
            blocks = ShapeBuilder.getShapeByType(type, angle);
            this.type = type;
            _angle = angle;
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
                block.X += to_x;
                block.Y += to_y;
            }
            return true;
        }

        public void rotate()
        {
            Angle a = Angle.A0;
            int offsetX = blocks[0].X - blocks[0].Initial_X;
            int offsetY = blocks[0].Y - blocks[0].Initial_Y;
            switch(_angle)
            {
                case Angle.A0:
                    a = Angle.A90;
                    break;
                case Angle.A90:
                    a = Angle.A180;
                    break;
                case Angle.A180:
                    a = Angle.A270;
                    break;
                case Angle.A270:
                    a = Angle.A0;
                    break;

            }
            SetState(_type, a);
            _leftBlock = findLeft();
            _rightBlock = findRight();
            _topBlock = findTop();
            _bottomBlock = findBottom();
            foreach (var block in blocks)
            {
                block.name = name;
            }
            move(offsetX, offsetY);
        }
    }
}
