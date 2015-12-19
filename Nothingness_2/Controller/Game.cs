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
        private Random rnd = new Random();

        public static Game Instance
        {
            get
            {
                if (inst == null)
                    inst = new Game();
                return inst;
            }
        }

        public State state = State.Play;

        private Screen screen;
        private Input input;
        private Shape currentShape;
        private ScoreCount scores;
        private ShapePool shapePool;
        private int ticks = 0;
        private int speed = 50;

        public Screen Screen { get { return screen; } }
        public Input Input { get { return input; } }

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
            if (ticks > speed)
            {
                switch (state)
                {
                    case State.Play:
                        if (currentShape == null || currentShape.destroyed == true)
                        {
                            currentShape = shapePool.GetOne();
                            currentShape.move(rnd.Next(9), 0);
                            screen.shapes.Add(currentShape);
                        }
                        moveShape();
                        screen.DrawFrame();

                        break;

                    case State.Pause:
                        break;
                }
                ticks = 0;
            }
            else
                ticks++;
        }

        private bool checkCollisions()
        {
            var flag = false;
            var yFlag = false;
            switch (input.State)
            {
                case Input.Move.Left:
                    if (currentShape.LeftBlock.X - 1 < 0 || screen.blocks[currentShape.LeftBlock.Y][currentShape.LeftBlock.X - 1].in_use == true)
                        flag = true;
                    break;
                case Input.Move.Right:
                    if (currentShape.RightBlock.X + 2 > Screen.WIDTH || screen.blocks[currentShape.RightBlock.Y][currentShape.RightBlock.X + 1].in_use == true)
                        flag = true;
                    break;
            }

            foreach(var block in currentShape.blocks)
            {
                try
                {
                    if (block.name != screen.blocks[block.Y + 1][block.X].name && screen.blocks[block.Y + 1][block.X].in_use == true)
                        yFlag = true;
                }
                catch (IndexOutOfRangeException)
                {
                    yFlag = true;
                }
            }
            if(currentShape.BottomBlock.Y + 2 > Screen.HEIGHT || yFlag)
            {
                flag = true;
                currentShape.destroyed = true;
            }
            if (flag == true)
                return true;
            else return false;
        }

        private void moveShape()
        {
            if(checkCollisions() == false)
            {
                switch (input.State)
                {
                    case Input.Move.Left:
                        currentShape.move(-1, 0);
                        break;
                    case Input.Move.Right:
                        currentShape.move(1, 0);
                        break;
                    case Input.Move.Down:
                        // we need to speed things up
                        //currentShape.move(1, 0);
                        speed = 10;
                        break;
                    case Input.Move.No:
                        speed = 50;
                        break;
                }
            }
            if(currentShape.destroyed == false)
                currentShape.move(0, 1);
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
    }
}
