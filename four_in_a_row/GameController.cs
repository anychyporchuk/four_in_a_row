using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace four_in_a_row
{
    /// <summary>
    /// Class, which is responsible for game logic
    /// </summary>
    static class GameController
    {
        
        public static int CurrentPlayer;
        public static bool IsCanPlay;
        static GameController()
        {
            CurrentPlayer = 0;
            IsCanPlay = true;
        }

        public enum CountOfUsedCoins : int
        {
            FULL = 256,
            COLUMNFULL = 512
        }

        /// <summary>
        /// Finds right position in the column
        /// </summary>
        /// <param name="currpl">index of current player</param>
        /// <param name="column">the desired column</param>
        /// <returns>the empty right place in the column</returns>
        public static int GetPosition(int currpl, int column)
        {
            int res;
            currpl = currpl == 0 ? -1 : 1;
            res = GameModel.index_empty_coins[column];

            if (res < Constants.ROWSC)
            {
                int isfull = GameModel.AddCoin(currpl, column);
                return isfull == (int)CountOfUsedCoins.FULL ? (int)CountOfUsedCoins.FULL : res;
            }
            else
                return (int)CountOfUsedCoins.COLUMNFULL;
        }

        



        /// <summary>
        /// Searches the combination all possible ways
        /// </summary>
        /// <param name="row">the desired row</param>
        /// <param name="col">the desired column</param>
        /// <returns>result of searching</returns>
        public static bool CheckResult(int row, int col)
        {
            return (СheckHorizontal(row)) || (СheckVertical(col)) ||
            (СheckLeftDiagonal1(row, col)) || (СheckRightDiagonal1(row, col));
        }

        #region Service functions for searching

        /// <summary>
        /// Searches the combination horisontally
        /// </summary>
        /// <param name="row">the desired row</param>
        /// <returns>result of searching</returns>
        public static bool СheckHorizontal(int row)
        {
            int countOfIdent = 1;
            for (int i = 1; i < Constants.COLUMNSC; i++)
                if (GameModel.Board[row, i] != 0 && GameModel.Board[row, i - 1] == GameModel.Board[row, i])
                {
                    countOfIdent++;
                    if (countOfIdent == Constants.COUNTFORWIN) return true;
                }
                else countOfIdent = 1;
            return (countOfIdent == Constants.COUNTFORWIN) ? true : false;
        }

        /// <summary>
        /// Searches the combination vertically
        /// </summary>
        /// <param name="col">the desired row</param>
        /// <returns>result of searching</returns>
        public static bool СheckVertical(int col)
        {
            int countOfIdent = 1;
            for (int i = 1; i < Constants.ROWSC; i++)
                if (GameModel.Board[i, col] != 0 && GameModel.Board[i - 1, col] == GameModel.Board[i, col])
                {
                    countOfIdent++;
                    if (countOfIdent == Constants.COUNTFORWIN) return true;
                }
                else countOfIdent = 1;
            return (countOfIdent == Constants.COUNTFORWIN) ? true : false;
        }

        /// <summary>
        /// Searches the combination from the left top to the right bottom diagonally
        /// </summary>
        /// <param name="row">the desired row</param>
        /// <param name="col">the desired column</param>
        /// <returns>result of searching</returns>
        public static bool СheckLeftDiagonal1(int row, int col)
        {
            for (int i = 0; i < Constants.COUNTFORWIN; i++)
            {
                int countOfIdent = 0;
                for (int j = 0; j < Constants.COUNTFORWIN; j++)
                {
                    if (row + j - i >= 0 && row + j - i < Constants.ROWSC
                        && col - j + i >= 0 && col - j + i < Constants.COLUMNSC
                        && GameModel.Board[row, col] == GameModel.Board[row + j - i, col - j + i])
                    {
                        countOfIdent++;
                    }
                    if (countOfIdent >= Constants.COUNTFORWIN) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Searches the combination from the right top to the left bottom diagonally
        /// </summary>
        /// <param name="row">the desired row</param>
        /// <param name="col">the desired column</param>
        /// <returns>result of searching</returns>
        public static bool СheckRightDiagonal1(int row, int col)
        {
            for (int i = 0; i < Constants.COUNTFORWIN; i++)
            {
                int countOfIdent = 0;
                for (int j = 0; j < Constants.COUNTFORWIN; j++)
                {
                    if (row + j - i >= 0 && row + j - i < Constants.ROWSC
                        && col + j - i >= 0 && col + j - i < Constants.COLUMNSC
                        && GameModel.Board[row, col] == GameModel.Board[row + j - i, col + j - i])
                    {
                        countOfIdent++;
                    }
                    if (countOfIdent >= Constants.COUNTFORWIN) return true;
                }
            }
            return false;
        }
        #endregion

        public static void RestartGame()
        {
            CurrentPlayer = 0;
            IsCanPlay = true;
            GameModel.ClearInfo();
        }
    }
}
