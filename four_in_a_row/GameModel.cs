using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace four_in_a_row
{
    public static class GameModel
    {
        public static int[,] Board;
        public static int[] index_empty_coins; //service array indexes of desired empty places in a board

        static GameModel()
        {
            Board = new int[Constants.ROWSC, Constants.COLUMNSC];
            index_empty_coins = new int[Constants.COLUMNSC];
        }

        /// <summary>
        /// Adds the coin to the GameModel.Board
        /// </summary>
        /// <param name="currpl">index of current player</param>
        /// <param name="column">the desired column</param>
        public static int AddCoin(int currpl, int column)
        {
            Board[index_empty_coins[column], column] = currpl;
            index_empty_coins[column]++;

            if (index_empty_coins.Sum() == (Constants.COLUMNSC) * (Constants.ROWSC))
                return (int)GameController.CountOfUsedCoins.FULL;
            else return 0;
        }

        /// <summary>
        /// Reinitializes GameModel.Board information
        /// </summary>
        public static void ClearInfo()
        {
            GameModel.Board = new int[Constants.ROWSC, Constants.COLUMNSC];
            index_empty_coins = new int[Constants.COLUMNSC];
        }
    }
}
