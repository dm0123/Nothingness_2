using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Nothingness_2.Model;
using Nothingness_2.View;

namespace Nothingness_2.Model
{
    /// <summary>
    /// простенький пул объектов.
    /// создание объектов занимает очень много ресурсов,
    /// поэтому прибегаем к помощи этой штуки. Типизирована
    /// Shape'ом, чтобы не заморачиваться
    /// </summary>
    public class ShapePool
    {
        private ConcurrentBag<Shape> bag = new ConcurrentBag<Shape>();
        private Random rnd = new Random();
        private Array values = Enum.GetValues(typeof(Shape.Type));
        private Array a_values = Enum.GetValues(typeof(Shape.Angle));
        public Shape GetOne()
        {
            Shape shape;
            if (bag.TryTake(out shape))
            {
                shape.SetState((Shape.Type)values.GetValue(rnd.Next(values.Length)), /*(Shape.Angle)a_values.GetValue(rnd.Next(values.Length))*/0);
            }
            else shape = new Shape((Shape.Type)values.GetValue(rnd.Next(values.Length)), /*(Shape.Angle)a_values.GetValue(rnd.Next(a_values.Length))*/0);
            //if(shape.BottomBlock.Y < 0)
            //{
            //    shape.move(0, - shape.BottomBlock.Y);
            //}
            bool flag = false;
            int offsetX = 0;
            int offsetY = 1;
            foreach(var block in shape.blocks)
            {
                if (block.Y < offsetY)
                {
                    flag = true;
                    offsetY = -block.Y;
                }
                if(block.X < 0)
                {
                    flag = true;
                    offsetX = -block.X;
                }
                if(block.X >= Screen.WIDTH - offsetX)
                {
                    flag = true;
                    offsetX = Screen.WIDTH - block.X;
                }
            }
            if (flag == true)
                shape.move(offsetX, offsetY);
            return shape;
        }

        public void Release(ref Shape one)
        {
            one.ResetState();
            bag.Add(one);
            one = null;
        }
    }
}
