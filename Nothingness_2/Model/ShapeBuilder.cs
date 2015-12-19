using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nothingness_2.Model
{
    public class ShapeBuilder
    {
        public static List<Block> getShapeByType(Shape.Type type)
        {
            List<Block> blocks = new List<Block>();
            switch (type)
            {
                case Shape.Type.I:
                    blocks.Add(new Block(0, 0));
                    blocks.Add(new Block(0, 1));
                    blocks.Add(new Block(0, 2));
                    blocks.Add(new Block(0, 3));
                    break;
                case Shape.Type.J:
                    blocks.Add(new Block(1, 0));
                    blocks.Add(new Block(1, 1));
                    blocks.Add(new Block(1, 2));
                    blocks.Add(new Block(0, 2));
                    break;
                case Shape.Type.L:
                    blocks.Add(new Block(0, 0));
                    blocks.Add(new Block(0, 1));
                    blocks.Add(new Block(0, 2));
                    blocks.Add(new Block(1, 2));
                    break;
                case Shape.Type.O:
                    blocks.Add(new Block(0, 0));
                    blocks.Add(new Block(0, 1));
                    blocks.Add(new Block(1, 0));
                    blocks.Add(new Block(1, 1));
                    break;
                case Shape.Type.S:
                    blocks.Add(new Block(1, 0));
                    blocks.Add(new Block(2, 0));
                    blocks.Add(new Block(0, 1));
                    blocks.Add(new Block(1, 1));
                    break;
                case Shape.Type.T:
                    blocks.Add(new Block(1, 0));
                    blocks.Add(new Block(0, 1));
                    blocks.Add(new Block(1, 1));
                    blocks.Add(new Block(2, 1));
                    break;
                case Shape.Type.Z:
                    blocks.Add(new Block(0, 0));
                    blocks.Add(new Block(1, 0));
                    blocks.Add(new Block(1, 1));
                    blocks.Add(new Block(1, 2));
                    break;
            }
            
            return blocks;
        }
    }
}
