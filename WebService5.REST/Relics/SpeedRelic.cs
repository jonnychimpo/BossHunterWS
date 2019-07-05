using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebService5.REST.Helper;

namespace WebService5.REST.Relics
{
    public class SpeedRelic : BaseRelic
    {
        public SpeedRelic()
        {
            ID = 3;
            PartitionKey = "Relic";
            RowKey = "Speed";
            Stat2Mod = EntityStat.Speed;
            ModValue = 1;
            Desc = "Increase Speed";
            Image = "C:\\Users\\jthoma\\Pictures\\HeroMytes\\speed.png";
            OnClick = "IncreaseStat('/IncreaseStat/BossHuntv1/User/Speed')";
        }
    }
}