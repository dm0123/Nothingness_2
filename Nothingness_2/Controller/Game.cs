using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nothingness_2.View;
using Nothingness_2.Model;

namespace Nothingness_2.Controller
{
    public sealed class Game
    {
        public enum State { Play, Pause };

        /// <summary>
        /// простенький синглтон.
        /// объект игры может существовать только один.
        /// </summary>
        private static Game inst;

        public static Game Instance
        {
            get
            {
                if (inst == null)
                    inst = new Game();
                return inst;
            }
        }

        public State state = State.Pause;

        private Screen screen;
        private Input input;
        private Shape currentShape;
        private ScoreCount scores;
        private ShapePool shapePool;

        public Screen Screen { get { return screen; } }

        public Shape CurrentShape { get { return currentShape; } }

        private Game()
        {
            shapePool = new ShapePool();
            screen = new Screen();
            input = new Input();
            scores = new ScoreCount();
        }

        public MainWindow Win
        {
            set
            {
                screen.win = value;
            }
            get
            {
                return screen.win;
            }
        }

        public void Run(object sender, EventArgs args)
        {
            if(currentShape == null || currentShape.destroyed == true)
            {
                currentShape = shapePool.GetOne();
            }


            screen.DrawFrame();
        }

        private void Reset()
        {
            // стереть все с экрана, запомнить результат
            screen.Clear();
            scores.Store();
        }

        public void Pause()
        {
            if (this.state == State.Play)
                this.state = State.Pause;
            else
                this.state = State.Play;
        }

        private bool checkCollisions()
        {
            // not implemented
            // где-то тут форма будет возвращаться в пул
            return false;
        }
    }
}
