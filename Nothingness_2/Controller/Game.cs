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

        public event EventHandler GameOverEvent;
        public State state = State.Play;

        private Screen screen;
        private Input input;
        private Shape currentShape;
        private ScoreCount scores;
        private ShapePool shapePool;
        private int ticks = 0;
        private int speed = 20;
        private int gravitySpeed = 50;
        private int gravityTicks = 0;
        private bool needCheckFill = false;

        public ShapePool ShapePool { get { return shapePool; } }

        public Screen Screen { get { return screen; } }
        public Input Input { get { return input; } }

        public ScoreCount Scores { get { return scores; } }

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
                            while(currentShape.LeftBlock.X > Screen.WIDTH && currentShape.RightBlock.X < 0 && currentShape.TopBlock.Y > Screen.HEIGHT - 1 && currentShape.BottomBlock.Y < 0)
                            {
                                currentShape = shapePool.GetOne();
                            }
                            currentShape.move(/*rnd.Next(9)*/4, 0);
                            screen.shapes.Add(currentShape);
                        }
                        if(needCheckFill)
                        {
                            checkFill();
                        }
                        moveShape();

                        //if (gravityTicks > gravitySpeed)
                        //{
                            // gravity
                            if (currentShape.destroyed == false)
                                currentShape.move(0, 1);
                        //    gravityTicks = 0;
                        //}
                        //else gravityTicks++;

                        screen.DrawFrame();
                        checkOver();

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
                needCheckFill = true;

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
                        speed = 3;
                        break;
                    case Input.Move.Rotate:
                        if(checkRotation() == true)
                            currentShape.rotate();
                        break;
                    case Input.Move.No:
                        speed = 20;
                        break;
                }
            }
        }

        private void checkFill()
        {
            screen.rowsToRemove.Clear();
            for (int j = 0; j < Screen.HEIGHT; j++)
            {
                int count = 0;
                for (int i = 0; i < Screen.WIDTH; i++)
                {
                    if (screen.blocks[/*Screen.HEIGHT - 1*/j][i].in_use == true)
                        count++;
                }
                if (count == Screen.WIDTH)
                {
                    //screen.removeRow();
                    screen.rowsToRemove.Add(j);
                    scores.Add();
                }
            }
            screen.removeRow();
        }

        private void checkOver()
        {
            bool flag = false;
            //for(int i = 0; i < Screen.WIDTH; i++)
            //{
            //    if(screen.blocks[0][i].in_use == true)
            //    {
            //        flag = true;
            //        break;
            //    }
            //}
            foreach(var shape in screen.shapes)
            {
                foreach(var block in shape.blocks)
                {
                    if(block.Y - 1 < 0)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            if(flag)
            {
                //state = State.Pause;
                input.State = Input.Move.No;
                Reset();
                GameOverEvent(this, new EventArgs());
            }
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

        public bool checkRotation()
        {
            bool res = true;
            switch (currentShape.type)
            {
                case Shape.Type.I:
                    if(currentShape.CurrentAngle == Shape.Angle.A0 || currentShape.CurrentAngle == Shape.Angle.A180)
                    {
                        if (currentShape.LeftBlock.X - 4 < 0 || currentShape.RightBlock.X + 4 >= Screen.WIDTH ||
                            screen.blocks[currentShape.LeftBlock.Y][currentShape.LeftBlock.X - 4].in_use == true || screen.blocks[currentShape.LeftBlock.Y][currentShape.RightBlock.X + 4].in_use == true)
                            res = false;
                    }
                    else if(currentShape.CurrentAngle == Shape.Angle.A90 || currentShape.CurrentAngle == Shape.Angle.A270)
                    {
                        if (currentShape.LeftBlock.Y - 4 < 0 || currentShape.RightBlock.Y + 4 >= Screen.HEIGHT ||
                            screen.blocks[currentShape.BottomBlock.Y - 4][currentShape.BottomBlock.X].in_use == true || screen.blocks[currentShape.TopBlock.Y + 4][currentShape.TopBlock.X].in_use == true)
                            res = false;
                    }
                    break;
                case Shape.Type.L:
                case Shape.Type.J:
                case Shape.Type.S:
                case Shape.Type.T:
                case Shape.Type.Z:
                    if (currentShape.LeftBlock.X - 1 < 0 || currentShape.RightBlock.X + 1 >= Screen.WIDTH)
                        res = false;
                    else if(screen.blocks[currentShape.LeftBlock.Y][currentShape.LeftBlock.X - 1].in_use == true || 
                            screen.blocks[currentShape.LeftBlock.Y][currentShape.RightBlock.X + 1].in_use == true)
                            res = false;
                    break;
                case Shape.Type.O:
                    break;                    
            }
            return res;
        }
    }
}
