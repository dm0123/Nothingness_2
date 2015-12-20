using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nothingness_2.Model;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using Nothingness_2.Controller;

namespace Nothingness_2.View
{
    public class Screen
    {
        public const int WIDTH = 10;
        public const int HEIGHT = 20;

        public Block[][] blocks;
        public List<Model.Shape> shapes = new List<Model.Shape>();
        public MainWindow win;
        private Dictionary<string, Rectangle> canvasRectangles = new Dictionary<string, Rectangle>();
        private bool removeFlag = false;
        public List<int> rowsToRemove = new List<int>();

        public Screen()
        {
            blocks = CreateJaggedArray<Block[][]>(HEIGHT, WIDTH);

            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    blocks[i][j] = new Block();
                }
            }
        }

        public void CreateWin(object sender, EventArgs args)
        {
            if (this.win != null)
            {
                // было бы неплохо сделать так, чтобы рисовались
                // всегда все блоки на своих захардкоженных мес-
                // тах, и этот класс просто заливал нужные цветом
                for (int i = 0; i < HEIGHT; i++)
                {
                    for (int j = 0; j < WIDTH; j++)
                    {
                        win.Screen.Dispatcher.Invoke((Action)(() =>
                        {
                            Rectangle rect = new Rectangle();
                            StringBuilder nameBuilder = new StringBuilder();
                            string name;
                            nameBuilder.Append("block_");
                            nameBuilder.Append(i.ToString());
                            nameBuilder.Append("_");
                            nameBuilder.Append(j.ToString());
                            name = nameBuilder.ToString();
                            rect.Name = name;

                            //blocksDic[name] = rect;

                            rect.Width = Block.SIZE;
                            rect.Height = Block.SIZE;

                            rect.Stroke = new SolidColorBrush(Colors.White);
                            rect.Fill = new SolidColorBrush(Colors.Black);

                            blocks[i][j].X = j;
                            blocks[i][j].Y = i;
                            blocks[i][j].name = "";

                            Canvas.SetLeft(rect, blocks[i][j].Screen_X);
                            Canvas.SetTop(rect, blocks[i][j].Screen_Y);
                            win.Screen.Children.Add(rect);
                        }));
                    }
                }
                var rectangles = win.Screen.Children.OfType<Rectangle>().ToList();
                foreach(var rect in rectangles)
                {
                    canvasRectangles[rect.Name] = rect;
                }
            }
        }

        public void removeRow()
        {
            removeFlag = true;
        }

        public void Clear()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    blocks[i][j].Reset();
                }
            }
            shapes.Clear();
        }

        public void DrawFrame()
        {
            // clean ------------------------
            foreach(var rect in canvasRectangles)
            {
                rect.Value.Fill = new SolidColorBrush(Colors.Black);
            }
            for(int i = 0; i < HEIGHT; i++)
            {
                for(int j = 0; j < WIDTH; j++)
                {
                    blocks[i][j].in_use = false;
                }
            }
            //--------------------------------

            if(removeFlag == true)
            {
                foreach(var shape in shapes)
                {
                    //foreach(var block in shape.blocks)
                    for(int i = shape.blocks.Count - 1; i >= 0; i--)
                    {
                        if (shape.blocks[i].Y < HEIGHT)
                        {
                            shape.blocks[i].Y += 1;
                        }
                        else
                        {
                            shape.blocks.RemoveAt(i);
                        }
                    }
                }
                removeFlag = false;
            }

            foreach(var shape in shapes)
            {
                foreach (var block in /*Game.Instance.CurrentShape.blocks*/shape.blocks)
                {
                    StringBuilder nameBuilder = new StringBuilder();
                    string name;
                    nameBuilder.Append("block_");
                    nameBuilder.Append(block.Y.ToString());
                    nameBuilder.Append("_");
                    nameBuilder.Append(block.X.ToString());
                    name = nameBuilder.ToString();
                    if(block.Y < HEIGHT && block.X < WIDTH)
                    {
                        blocks[block.Y][block.X].in_use = true;
                        blocks[block.Y][block.X].name = block.name;
                        try
                        {
                            canvasRectangles[name].Fill = new SolidColorBrush(Colors.Green);
                        }
                        catch (KeyNotFoundException)
                        {
                        }
                    }
                }
            }
            
        }

            static T CreateJaggedArray<T>(params int[] lengths)
        {
            return (T)InitializeJaggedArray(typeof(T).GetElementType(), 0, lengths);
        }

        static object InitializeJaggedArray(Type type, int index, int[] lengths)
        {
            Array array = Array.CreateInstance(type, lengths[index]);
            Type elementType = type.GetElementType();

            if (elementType != null)
            {
                for (int i = 0; i < lengths[index]; i++)
                {
                    array.SetValue(
                        InitializeJaggedArray(elementType, index + 1, lengths), i);
                }
            }

            return array;
        }
    }
}
