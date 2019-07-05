using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService5.REST
{
    public class LeaderBoardEntry
    {
        public string PartitionKey;
        public string RowKey;

        public int AttackCount;
        public int RespawnCount;
        public int Score;
        public string AttackDate;
        public int LeaderBoardEntryID;

        public LeaderBoardEntry()
        {
            PartitionKey = "Leaderboard";
        }
    }
}