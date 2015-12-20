using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nothingness_2.Model
{
    public class ShapeBuilder
    {
        /// <summary>
        /// центром поворота у всех сделан
        /// второй элемент списка. просто так.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Block> getShapeByType(Shape.Type type, Shape.Angle angle)
        {
            List<Block> blocks = new List<Block>();
            switch (type)
            {
                case Shape.Type.I:
                    switch(angle)
                    {
                        case Shape.Angle.A180:
                        case Shape.Angle.A0:
                            blocks.Add(new Block(0, 0));
                            blocks.Add(new Block(0, 1));
                            blocks.Add(new Block(0, 2));
                            blocks.Add(new Block(0, 3));
                            break;
                        case Shape.Angle.A90:
                        case Shape.Angle.A270:
                            blocks.Add(new Block(0, 0));
                            blocks.Add(new Block(1, 0));
                            blocks.Add(new Block(2, 0));
                            blocks.Add(new Block(3, 0));
                            break;
                    }
                    break;
                case Shape.Type.J:
                    switch(angle)
                    {
                        case Shape.Angle.A0:
                            blocks.Add(new Block(1, 0));
                            blocks.Add(new Block(1, 1));
                            blocks.Add(new Block(1, 2));
                            blocks.Add(new Block(0, 2));
                            break;
                        case Shape.Angle.A90:
                            blocks.Add(new Block(0, 0));
                            blocks.Add(new Block(0, 1));
                            blocks.Add(new Block(1, 1));
                            blocks.Add(new Block(2, 1));
                            break;
                        case Shape.Angle.A180:
                            blocks.Add(new Block(0, 0));
                            blocks.Add(new Block(1, 0));
                            blocks.Add(new Block(0, 1));
                            blocks.Add(new Block(0, 2));
                            break;
                        case Shape.Angle.A270:
                            blocks.Add(new Block(0, 0));
                            blocks.Add(new Block(1, 0));
                            blocks.Add(new Block(2, 0));
                            blocks.Add(new Block(2, 1));
                            break;
                    }
                    break;
                case Shape.Type.L:
                    switch(angle)
                    {
                        case Shape.Angle.A0:
                            blocks.Add(new Block(0, 0));
                            blocks.Add(new Block(0, 1));
                            blocks.Add(new Block(0, 2));
                            blocks.Add(new Block(1, 2));
                            break;
                        case Shape.Angle.A90:
                            blocks.Add(new Block(0, 0));
                            blocks.Add(new Block(0, 1));
                            blocks.Add(new Block(1, 0));
                            blocks.Add(new Block(2, 0));
                            break;
                        case Shape.Angle.A180:
                            blocks.Add(new Block(0, 0));
                            blocks.Add(new Block(1, 0));
                            blocks.Add(new Block(1, 1));
                            blocks.Add(new Block(1, 2));
                            break;
                        case Shape.Angle.A270:
                            blocks.Add(new Block(0, 1));
                            blocks.Add(new Block(1, 1));
                            blocks.Add(new Block(1, 2));
                            blocks.Add(new Block(0, 2));
                            break;
                    }
                    break;
                case Shape.Type.O:
                    blocks.Add(new Block(0, 0));
                    blocks.Add(new Block(0, 1));
                    blocks.Add(new Block(1, 0));
                    blocks.Add(new Block(1, 1));
                    break;
                case Shape.Type.S:
                    switch(angle)
                    {
                        case Shape.Angle.A0:
                        case Shape.Angle.A180:
                            blocks.Add(new Block(2, 0));
                            blocks.Add(new Block(1, 0));
                            blocks.Add(new Block(0, 1));
                            blocks.Add(new Block(1, 1));
                            break;
                        case Shape.Angle.A270:
                        case Shape.Angle.A90:
                            blocks.Add(new Block(0, 0));
                            blocks.Add(new Block(0, 1));
                            blocks.Add(new Block(1, 1));
                            blocks.Add(new Block(1, 2));
                            break;
                    }
                    break;
                case Shape.Type.T:
                    switch(angle)
                    {
                        case Shape.Angle.A0:
                            blocks.Add(new Block(0, 1));
                            blocks.Add(new Block(1, 0));
                            blocks.Add(new Block(1, 1));
                            blocks.Add(new Block(2, 1));
                            break;
                        case Shape.Angle.A90:
                            blocks.Add(new Block(0, 0));
                            blocks.Add(new Block(0, 1));
                            blocks.Add(new Block(1, 1));
                            blocks.Add(new Block(0, 2));
                            break;
                        case Shape.Angle.A180:
                            blocks.Add(new Block(0, 0));
                            blocks.Add(new Block(1, 0));
                            blocks.Add(new Block(1, 1));
                            blocks.Add(new Block(2, 0));
                            break;
                        case Shape.Angle.A270:
                            blocks.Add(new Block(1, 0));
                            blocks.Add(new Block(1, 1));
                            blocks.Add(new Block(0, 1));
                            blocks.Add(new Block(1, 2));
                            break;
                    }
                    break;
                case Shape.Type.Z:
                    switch(angle)
                    {
                        case Shape.Angle.A180:
                        case Shape.Angle.A0:
                            blocks.Add(new Block(0, 0));
                            blocks.Add(new Block(1, 0));
                            blocks.Add(new Block(1, 1));
                            blocks.Add(new Block(2, 1));
                            break;
                        case Shape.Angle.A90:
                        case Shape.Angle.A270:
                            blocks.Add(new Block(1, 0));
                            blocks.Add(new Block(1, 1));
                            blocks.Add(new Block(0, 1));
                            blocks.Add(new Block(0, 2));
                            break;
                    }
                    break;
            }
            
            return blocks;
        }
    }
}
