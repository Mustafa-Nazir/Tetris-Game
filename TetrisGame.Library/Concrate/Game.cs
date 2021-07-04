using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using TetrisGame.Library.Enum;

namespace TetrisGame.Library.Concrate
{
    public class Game
    {
        private Panel gamePanel;
        private Label scorLabel;

        Tetromino _currentlyObject;

        private bool isOpen = false;
        private int scor = 0;

        List<BaseObject> _otherObjects = new List<BaseObject>();

        private Timer _MoveTimer = new Timer { Interval = 200 };

        public Game(Panel gamePanel , Label scorLabel)
        {
            this.gamePanel = gamePanel;

            this.scorLabel = scorLabel;
            this.scorLabel.Text = "Scor: " + scor.ToString();
            
            _MoveTimer.Tick += MoveTimer_Tick;
        }

        private void MoveTimer_Tick(object sender , EventArgs e)
        {
            MoveToObject(Direction.Down);
        }

        public void MoveToObject(Direction direction)
        {
            if (!isOpen) return; 

            if (!Control())
            {
                for(int i = 0; i < _currentlyObject._baseObjects.Length; i++)
                {
                    _otherObjects.Add(_currentlyObject._baseObjects[i]);
                }

                ControlLine();
                ControlTheEnd();

                _currentlyObject = new Tetromino(gamePanel);
            }

            if (!L_R_Control(direction)) return;

            _currentlyObject.MoveToObjects(direction);
        }

        private void ControlLine()
        {
            int pieces = gamePanel.Height / _otherObjects[0].Height;

            for(int i = 0; i < pieces; i++ )
            {
                int total = 0;
                int Top = i * _otherObjects[0].Height;

                foreach (var item in _otherObjects)
                {
                    if(Top == item.Top)
                    {
                        total += _otherObjects[0].Width;
                    }
                }

                if (total == gamePanel.Width) DeleteLine(Top);
            }
        }

        private void DeleteLine(int Top)
        {
            for(int i = _otherObjects.Count -1 ; i >= 0; i--)
            {
                if(_otherObjects[i].Top == Top )
                {
                    gamePanel.Controls.Remove(_otherObjects[i]);
                    _otherObjects.Remove(_otherObjects[i]);
                }
                else if(_otherObjects[i].Top < Top)
                {
                    _otherObjects[i].Move(Direction.Down);
                }
                
            }

            scor += 10;
            scorLabel.Text = "Score: " + scor.ToString(); 
        }

        private bool L_R_Control(Direction direction)
        {
            if(_otherObjects.Count != 0 && direction == Direction.Left)
            {
                foreach (var _others in _otherObjects)
                {
                    foreach (var item in _currentlyObject._baseObjects)
                    {
                        if (_others.Right == item.Left  && _others.Top == item.Bottom) return false; ;
                    }
                }
            }
            else if(_otherObjects.Count != 0 && direction == Direction.Right)
            {
                foreach (var _others in _otherObjects)
                {
                    foreach (var item in _currentlyObject._baseObjects)
                    {
                        if (_others.Left == item.Right && _others.Top == item.Bottom) return false; ;
                    }
                }
            }

            return true;
        }

        private bool Control()
        {

            for(int i = 0; i < _currentlyObject._baseObjects.Length; i++)
            {
                if (_currentlyObject._baseObjects[i].Bottom == gamePanel.Height) return false;

                if(_otherObjects.Count != 0)
                {
                    foreach (var item in _otherObjects)
                    {
                        if (item.Top == _currentlyObject._baseObjects[i].Bottom && item.Left == _currentlyObject._baseObjects[i].Left) return false;
                    }
                }
            }

            return true;
        }

        private void ControlTheEnd()
        {
            if (_otherObjects.Count == 0) return;

            foreach (var item in _otherObjects)
            {
                if(item.Top == _currentlyObject._baseObjects[0].Height)
                {
                    Stop();
                    RestratButton();
                    break;
                }
            }
        }

        public void RotateObject()
        {
            if (!isOpen) return;

            _currentlyObject.RotateToObject();
        }

        private void RestratButton()
        {
            Button _restart = new Button();
            _restart.Text = "Play Again!";
            _restart.Left = (gamePanel.Width - _restart.Width) / 2;
            _restart.Top = gamePanel.Height / 2;
            _restart.BackColor = Color.White;

            _restart.Click += RestartButton_Click;

            foreach (var item in _otherObjects)
            {
                gamePanel.Controls.Remove(item);
            }

            gamePanel.Controls.Add(_restart);
        }

        private void RestartButton_Click(object sender , EventArgs e)
        {
            Application.Restart();
        }

        public void Start()
        {
            if (isOpen) return;

            _MoveTimer.Start();

            isOpen = true;
            _currentlyObject = new Tetromino(gamePanel);
            
        }

        private void Stop()
        {
            if (!isOpen) return;

            _MoveTimer.Stop();
            isOpen = false;
        }
    }
}
