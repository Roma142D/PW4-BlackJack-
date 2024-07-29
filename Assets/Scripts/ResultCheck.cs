using UnityEngine;

namespace RomanDoliba.Core
{
    public static class ResultCheck
    {
        public static bool IsBlackJack(int score)
        {
            if (score == 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsTooMuch(int score)
        {
            if (score > 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsPlayerWin(int playerScore, int botScore)
        {
            return playerScore > botScore;
        }
    }
}
