using System.Collections.Generic;

namespace Trivia
{
    public class Players
    {
        public List<string> players = new List<string>();
        public int[] places = new int[6];
        public int[] purses = new int[6];
        public bool[] inPenaltyBox = new bool[6];
        public int currentPlayer = 0;

        public Players()
        {
        }
    }
}