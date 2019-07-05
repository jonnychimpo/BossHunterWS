using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebService5.REST.Helper;

namespace WebService5.REST.Relics
{
    public class AccuracyRelic : BaseRelic
    {
        public AccuracyRelic()
        {
            ID = 4;
            PartitionKey = "Relic";
            RowKey = "Accuracy";
            Stat2Mod = EntityStat.Accuracy;
            ModValue = 5;
            Desc = "Increase Accuracy";
            Image = "C:\\Users\\jthoma\\Pictures\\HeroMytes\\accuracy.png";
            OnClick = "IncreaseStat('/IncreaseStat/BossHuntv1/User/Accuracy')";
        }
    }
}