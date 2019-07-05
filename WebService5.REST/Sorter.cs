using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService5.REST
{
    public class Sorter : IComparer<LeaderBoardEntry>
    {
        public int Compare(LeaderBoardEntry x, LeaderBoardEntry y)
        {
            if (x.Score == 0 || y.Score == 0)
            {
                return 0;
            }

            // CompareTo() method 
            return x.Score.CompareTo(y.Score);
        }
    }
}