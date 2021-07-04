using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TetrisGame.Library.Enum;

namespace TetrisGame.Library.Concrate
{
    public class BaseObject : PictureBox
    {

        public Size AreaSize { get;}

        private int distance { get; set; }


        public new int Right 
        {
            get => base.Right;
            set => Left = value - Width;
        }

        public new int Bottom
        {
            get => base.Bottom;
            set => Top = value - Height;
        }

        public BaseObject(Size AreaSize)
        {
            Image = Image.FromFile(@"image\block.png");

            SizeMode = PictureBoxSizeMode.AutoSize;

            this.AreaSize = AreaSize;
            distance = Width;
        }

        public bool Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return MoveToUp();

                case Direction.Down:
                    return MoveToDown();

                case Direction.Left:
                    return MoveToLeft();

                case Direction.Right:
                    return MoveToRight();

                default:
                    return false;
            }
        }

        private bool MoveToUp()
        {
            if (Top == 0) return true;

            var newTop = Top - distance;

            Top = newTop < 0 ? 0 : newTop;

            return Top == 0;
        }

        private bool MoveToLeft()
        {
            if (Left == 0) return true;

            var newLeft = Left - distance;

            Left = newLeft < 0 ? 0 : newLeft;

            return Left == 0;
        }

        private bool MoveToRight()
        {
            if (Right == AreaSize.Width) return true;

            var newRight = Right + distance;

            Right = newRight > AreaSize.Width ? AreaSize.Width : newRight;

            return Right == AreaSize.Width;
        }

        private bool MoveToDown()
        {
            if (Bottom == AreaSize.Height) return true;

            var newBottom = Bottom + distance;

            Bottom = newBottom > AreaSize.Height ? AreaSize.Height : newBottom;

            return Bottom == AreaSize.Height;
        }

    }
}
