using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TetrisGame.Library.Abstract;
using TetrisGame.Library.Enum;

namespace TetrisGame.Library.Concrate
{
    class Tetromino : GameObject
    {

        private Panel gamePanel;
        private int situation;
        private int whichObject;


        public Tetromino(Panel gamePanel)
        {
            this.gamePanel = gamePanel;
            _baseObjects = new BaseObject[4];
            situation = 1;

            whichObject = CreateObject();
        }

        private int CreateObject()
        {
            Random r = new Random();
            int value = r.Next(1,6);

            AddTheObjects();
            RandomField();
            ChooseAnObject(value);
            AddToPanel();

            return value;
        }

        private void ChooseAnObject(int value)
        {
            switch (value)
            {

                case 1:
                    CreateObject_Z();
                    break;

                case 2:
                    CreateObject_I();
                    break;

                case 3:
                    CreateObject_L();
                    break;

                case 4:
                    CreateObject_T();
                    break;

                case 5:
                    CreateObject_O();
                    break;
            }
        }

        private void CreateObject_Z()
        {


            _baseObjects[1].Left += _baseObjects[0].Width + _baseObjects[0].Left;

            _baseObjects[2].Left = _baseObjects[1].Left;
            _baseObjects[2].Top += _baseObjects[1].Height;

            _baseObjects[3].Left += _baseObjects[2].Width + _baseObjects[2].Left;
            _baseObjects[3].Top = _baseObjects[2].Height;

            AddToPanel();

        }

        private void CreateObject_I()
        {

            for(int i = 1; i < _baseObjects.Length;i++)
            {
                _baseObjects[i].Left = _baseObjects[i - 1].Left;
                _baseObjects[i].Top += _baseObjects[i - 1].Bottom;
            }

        }

        private void CreateObject_L()
        {

            for(int i = 1; i < _baseObjects.Length-1; i++)
            {
                _baseObjects[i].Left = _baseObjects[i - 1].Left;
                _baseObjects[i].Top += _baseObjects[i - 1].Bottom;
            }

            _baseObjects[3].Left += _baseObjects[3].Width + _baseObjects[2].Left;
            _baseObjects[3].Top = _baseObjects[2].Top;

        }

        private void CreateObject_T()
        {
            for (int i = 1; i < _baseObjects.Length - 1; i++)
            {
                _baseObjects[i].Left = _baseObjects[i - 1].Left + _baseObjects[i].Width;
            }

            _baseObjects[3].Left = _baseObjects[1].Left;
            _baseObjects[3].Top = _baseObjects[1].Bottom;
        }

        private void CreateObject_O()
        {
            for(int i = 1; i < _baseObjects.Length; i++)
            {
                if (i == 1)
                {
                    _baseObjects[i].Left = _baseObjects[i - 1].Left + _baseObjects[i].Width;
                    continue;
                }

                _baseObjects[i].Left = _baseObjects[i - 2].Left;
                _baseObjects[i].Top = _baseObjects[i - 2].Bottom;
            }
        }

        private void RandomField()
        {
            int total_field = gamePanel.Width / _baseObjects[0].Width;

            Random r = new Random();

            int field = r.Next(1 , total_field - 2);

            _baseObjects[0].Left = _baseObjects[0].Width * field;
        }

        private void AddTheObjects()
        {
            for (int i = 0; i < 4; i++)
            {
                _baseObjects[i] = new BaseObject(gamePanel.Size);
            }
        }

        private void AddToPanel()
        {
            foreach (var item in _baseObjects)
            {
                gamePanel.Controls.Add(item);
            }
        }

        public void RotateToObject()
        {
            switch (whichObject)
            {
                case 1:
                    RotateObject_Z();
                    break;

                case 2:
                    RotateObject_I();
                    break;

                case 3:
                    RotateObject_L();
                    break;

                case 4:
                    RotateObject_T();
                    break;
         
            }
        }

        private void RotateObject_Z()
        {
            switch (situation)
            {
                case 1:
                    Z_1To2();
                    situation += 1;
                    break;

                case 2:
                    Z_2To1();
                    situation = 1;
                    break;
            }
        }

        private void RotateObject_I()
        {
            switch (situation)
            {
                case 1:
                    I_1To2();
                    situation += 1;
                    break;

                case 2:
                    I_2To1();
                    situation = 1;
                    break;

                
            }
        }

        private void RotateObject_L()
        {
            switch (situation)
            {
                case 1:
                    L_1To2();
                    situation += 1;
                    break;

                case 2:
                    L_2To3();
                    situation += 1;
                    break;

                case 3:
                    L_3To4();
                    situation += 1;
                    break;

                case 4:
                    L_4To1();
                    situation = 1;
                    break;
       
                
            }
        }

        private void RotateObject_T()
        {
            switch (situation)
            {
                case 1:
                    T_1To2();
                    situation += 1;
                    break;

                case 2:
                    T_2To3();
                    situation += 1;
                    break;

                case 3:
                    T_3To4();
                    situation += 1;
                    break;

                case 4:
                    T_4To1();
                    situation = 1;
                    break;
            }
        }

        #region RotateToZ
        private void Z_1To2()
        {
            _baseObjects[0].Move(Direction.Right);
            _baseObjects[0].Move(Direction.Up);

            _baseObjects[2].Move(Direction.Up);
            _baseObjects[2].Move(Direction.Left);

            _baseObjects[3].Move(Direction.Left);
            _baseObjects[3].Move(Direction.Left);
        }

        private void Z_2To1()
        {
            
            if (_baseObjects[0].Right == gamePanel.Width)
            {
                foreach (var item in _baseObjects)
                {
                    item.Move(Direction.Left);
                }

                Z_2To1();
            }
            else
            {

                _baseObjects[0].Move(Direction.Down);
                _baseObjects[0].Move(Direction.Left);

                _baseObjects[2].Move(Direction.Right);
                _baseObjects[2].Move(Direction.Down);

                _baseObjects[3].Move(Direction.Right);
                _baseObjects[3].Move(Direction.Right);
            }

        }

        #endregion

        #region RotateToI

        private void I_1To2()
        {
            if (_baseObjects[0].Right == gamePanel.Width)
            {
                foreach (var item in _baseObjects)
                {
                    item.Move(Direction.Left);
                }

                I_1To2();
            }
            else if (_baseObjects[0].Left == 0)
            {
                foreach (var item in _baseObjects)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        item.Move(Direction.Right);
                    }
                }

                I_1To2();
            }
            else
            {

                _baseObjects[0].Move(Direction.Down);
                _baseObjects[0].Move(Direction.Right);

                _baseObjects[2].Move(Direction.Up);
                _baseObjects[2].Move(Direction.Left);

                _baseObjects[3].Move(Direction.Up);
                _baseObjects[3].Move(Direction.Up);
                _baseObjects[3].Move(Direction.Left);
                _baseObjects[3].Move(Direction.Left);
            }

        }

        private void I_2To1()
        {
            _baseObjects[0].Move(Direction.Left);
            _baseObjects[0].Move(Direction.Up);

            _baseObjects[2].Move(Direction.Right);
            _baseObjects[2].Move(Direction.Down);

            _baseObjects[3].Move(Direction.Right);
            _baseObjects[3].Move(Direction.Right);
            _baseObjects[3].Move(Direction.Down);
            _baseObjects[3].Move(Direction.Down);
        }

        #endregion

        #region RotateToL

        private void L_1To2()
        {
            if (_baseObjects[0].Left == 0)
            {
                foreach (var item in _baseObjects)
                {
                    item.Move(Direction.Right);
                }

                L_1To2();
            }
            else
            {

                _baseObjects[0].Move(Direction.Down);
                _baseObjects[0].Move(Direction.Right);

                _baseObjects[2].Move(Direction.Up);
                _baseObjects[2].Move(Direction.Left);

                _baseObjects[3].Move(Direction.Left);
                _baseObjects[3].Move(Direction.Left);
            }
        }

        private void L_2To3()
        {
            _baseObjects[0].Move(Direction.Left);
            _baseObjects[0].Move(Direction.Down);

            _baseObjects[2].Move(Direction.Right);
            _baseObjects[2].Move(Direction.Up);

            _baseObjects[3].Move(Direction.Up);
            _baseObjects[3].Move(Direction.Up);

        }

        private void L_3To4()
        {
            if (_baseObjects[0].Right == gamePanel.Width)
            {
                foreach (var item in _baseObjects)
                {
                    item.Move(Direction.Left);
                }

                L_3To4();
            }

            else
            {

                _baseObjects[0].Move(Direction.Up);
                _baseObjects[0].Move(Direction.Left);

                _baseObjects[2].Move(Direction.Down);
                _baseObjects[2].Move(Direction.Right);

                _baseObjects[3].Move(Direction.Right);
                _baseObjects[3].Move(Direction.Right);
            }
        }

        private void L_4To1()
        {
            _baseObjects[0].Move(Direction.Right);
            _baseObjects[0].Move(Direction.Up);

            _baseObjects[2].Move(Direction.Left);
            _baseObjects[2].Move(Direction.Down);

            _baseObjects[3].Move(Direction.Down);
            _baseObjects[3].Move(Direction.Down);
        }


        #endregion

        #region RotateToT

        private void T_1To2()
        {
            _baseObjects[0].Move(Direction.Right);
            _baseObjects[0].Move(Direction.Up);

            _baseObjects[2].Move(Direction.Left);
            _baseObjects[2].Move(Direction.Down);

            _baseObjects[3].Move(Direction.Up);
            _baseObjects[3].Move(Direction.Left);
        }

        private void T_2To3()
        {
            if (_baseObjects[0].Right == gamePanel.Width)
            {
                foreach (var item in _baseObjects)
                {
                    item.Move(Direction.Left);
                }

                T_2To3();
            }
            else
            {

                _baseObjects[0].Move(Direction.Down);
                _baseObjects[0].Move(Direction.Right);

                _baseObjects[2].Move(Direction.Up);
                _baseObjects[2].Move(Direction.Left);

                _baseObjects[3].Move(Direction.Right);
                _baseObjects[3].Move(Direction.Up);
            }
        }

        private void T_3To4()
        {
            _baseObjects[0].Move(Direction.Left);
            _baseObjects[0].Move(Direction.Down);

            _baseObjects[2].Move(Direction.Right);
            _baseObjects[2].Move(Direction.Up);

            _baseObjects[3].Move(Direction.Down);
            _baseObjects[3].Move(Direction.Right);
        }

        private void T_4To1()
        {
            if (_baseObjects[0].Left == 0)
            {
                foreach (var item in _baseObjects)
                {
                    item.Move(Direction.Right);
                }

                T_4To1();
            }
            else
            {

                _baseObjects[0].Move(Direction.Up);
                _baseObjects[0].Move(Direction.Left);

                _baseObjects[2].Move(Direction.Down);
                _baseObjects[2].Move(Direction.Right);

                _baseObjects[3].Move(Direction.Left);
                _baseObjects[3].Move(Direction.Down);
            }
        }

        #endregion
    }
}
