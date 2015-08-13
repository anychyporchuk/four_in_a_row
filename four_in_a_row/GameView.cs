using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace four_in_a_row
{
    public partial class GameView : Form
    {
        public GameView()
        {
            InitializeComponent();
            InitializeBoard();
            InitializePlayers();
        }
        private CoinView[,] boardView;
        private Color[] playerColors;

        private const int SIZE = 60;

        private void button1_Click(object sender, EventArgs e)
        {
            RestartGame();
        }


        #region Game events
        /// <summary>
        /// Changes player and checks the combination
        /// </summary>
        /// <param name="row">the desired row</param>
        /// <param name="col">the desired column</param>
        private void Changeturn(int row, int col)
        {
            if (GameController.CheckResult(row, col))
            {
                label1.Text = "Player " + (GameController.CurrentPlayer + 1) + " wins!";
                GameController.IsCanPlay = false;
            }
            else
            {
                GameController.CurrentPlayer = (GameController.CurrentPlayer == 1) ? 0 : 1;
                label1.Text = "Player's " + (GameController.CurrentPlayer + 1) + " turn";
            }
        }

        /// <summary>
        /// Reinitializes board view
        /// </summary>
        private void RestartGame()
        {
            label1.Text = "Player's " + (GameController.CurrentPlayer + 1) + " turn";
            GameController.ClearInfo();
            for (int i = 0; i < Constants.COLUMNSC; i++)
            {
                try
                {
                    CoinView coin = (CoinView)panel1.Controls.Find("cv_" + i, false)[0];
                    Graphics g = coin.CreateGraphics();
                    g.Clear(this.BackColor);

                    for (int j = 0; j < Constants.ROWSC; j++)
                    {
                        coin = (CoinView)panel1.Controls.Find("cv_" + j + i, false)[0];
                        g = coin.CreateGraphics();
                        g.Clear(this.BackColor);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("CoilView wasn't found" + ex.ToString());
                }
            }
        }
        #endregion

        #region Initialize view
        /// <summary>
        /// Initializes player's colours and current player
        /// </summary>
        private void InitializePlayers()
        {
            playerColors = new Color[2];
            playerColors[0] = Color.Red;
            playerColors[1] = Color.Blue;
            GameController.CurrentPlayer = 0;
            label1.Text = "Player's " + (GameController.CurrentPlayer + 1) + " turn";
        }

        /// <summary>
        /// Initializes board's view
        /// </summary>
        private void InitializeBoard()
        {
            boardView = new CoinView[Constants.ROWSC, Constants.COLUMNSC];
            for (int i = 0; i < Constants.ROWSC + 1; i++)
                for (int j = 0; j < Constants.COLUMNSC; j++)
                {

                    CoinView currPB = new CoinView();
                    currPB.Location = new Point(j * SIZE, panel1.Height - (i + 1) * SIZE);

                    currPB.Size = new Size(SIZE, SIZE);
                    if (i != Constants.ROWSC)
                    {
                        currPB.Name = "cv_" + i + j;
                        currPB.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else
                    {
                        currPB.Name = "cv_" + j;
                        currPB.MouseEnter += coinView_MouseEnter;
                        currPB.MouseLeave += coinView_MouseLeave;
                        currPB.MouseClick += coinView_MouseClick;

                    }

                    panel1.Controls.Add(currPB);

                }
        }
        #endregion

        #region Mouse events for controlled coin
        private void coinView_MouseEnter(object sender, EventArgs e)
        {
            if (GameController.IsCanPlay)
            {
                Graphics g = (sender as CoinView).CreateGraphics();
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(new SolidBrush(playerColors[GameController.CurrentPlayer]), 0, 0,
                    SIZE, SIZE);
            }

        }
        private void coinView_MouseLeave(object sender, EventArgs e)
        {
            if (GameController.IsCanPlay)
            {
                Graphics g = (sender as CoinView).CreateGraphics();
                g.Clear(this.BackColor);
            }

        }
        private void coinView_MouseClick(object sender, EventArgs e)
        {
            if (GameController.IsCanPlay)
            {
                Control cv = sender as CoinView;
                int col = Convert.ToInt32(cv.Name.Split(new char[] { '_' })[1]);

                int row = GameController.GetPosition(GameController.CurrentPlayer, col);

                if (row != (int)GameController.CountOfUsedCoins.COLUMNFULL && row != (int)GameController.CountOfUsedCoins.FULL)
                {
                    try
                    {
                        var coin = panel1.Controls.Find("cv_" + row + col, false)[0];
                        Graphics g = coin.CreateGraphics();
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.FillEllipse(new SolidBrush(playerColors[GameController.CurrentPlayer]), 0, 0,
                            SIZE, SIZE);

                        Changeturn(row, col);
                        g = cv.CreateGraphics();
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.FillEllipse(new SolidBrush(playerColors[GameController.CurrentPlayer]), 0, 0,
                            SIZE, SIZE);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("CoilView wasn't found" + ex.ToString());
                    }


                }
                else if (row == (int)GameController.CountOfUsedCoins.FULL)
                {
                    label1.Text = "Friendship wins!";
                    GameController.IsCanPlay = false;
                }
            }

        }
        #endregion

        


    }
}
