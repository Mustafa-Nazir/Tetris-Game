using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TetrisGame.Library.Concrate;
using TetrisGame.Library.Enum;

namespace TetrisGame.Desktop
{
    public partial class MainForm : Form
    {
        private Game _game;

        public MainForm()
        {
            InitializeComponent();
            Width = 300;
            panel2.Width = 12;
            panel3.Width = 12;
            _game = new Game(GamePanel , scorLabel);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    _game.Start();
                    GamePanel.Controls.Remove(infoLabel);
                    break;

                case Keys.Left:
                    _game.MoveToObject(Direction.Left);
                    break;

                case Keys.Right:
                    _game.MoveToObject(Direction.Right);
                    break;

                case Keys.Up:
                    _game.RotateObject();
                    break;
            }
        }
    }
}
