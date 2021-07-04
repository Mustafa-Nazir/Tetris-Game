using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TetrisGame.Library.Concrate;
using TetrisGame.Library.Enum;

namespace TetrisGame.Library.Abstract
{
    public abstract class GameObject
    {
        public BaseObject[] _baseObjects;

        public void MoveToObjects(Direction direction)
        {
            if (!Control(direction)) return;

            foreach (var item in _baseObjects)
            {
                item.Move(direction);
            }
        }

        private bool Control(Direction direction)
        {
            bool situation = true;

            if (direction == Direction.Left)
            {
                foreach (var item in _baseObjects)
                {
                    if (item.Left == 0) 
                    {
                        situation = false;
                        break;
                    }
                }
            }
            else if (direction == Direction.Right)
            {
                foreach (var item in _baseObjects)
                {
                    if (item.Right == item.AreaSize.Width)
                    {
                        situation = false;
                        break;
                    }
                }
            }

            return situation;
        }
    }
}
