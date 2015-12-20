using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Nothingness_2.Model;

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
                shape.SetState((Shape.Type)values.GetValue(rnd.Next(values.Length)), (Shape.Angle)a_values.GetValue(rnd.Next(values.Length)));
            }
            else shape = new Shape((Shape.Type)values.GetValue(rnd.Next(values.Length)), (Shape.Angle)a_values.GetValue(rnd.Next(a_values.Length)));
            if(shape.BottomBlock.Y < 0)
            {
                shape.move(0, - shape.BottomBlock.Y);
            }
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
