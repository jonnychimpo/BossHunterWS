using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebService5.REST.Helper;

namespace WebService5.REST.Relics
{
    public class HealthRelic : BaseRelic
    {
        public HealthRelic()
        {
            ID = 2;
            PartitionKey = "Relic";
            RowKey = "Health";
            Stat2Mod = EntityStat.Health;
            ModValue = 3;
            Desc = "Increase Health";
            Image = "C:\\Users\\jthoma\\Pictures\\HeroMytes\\redheart.png";
            OnClick = "IncreaseStat('/IncreaseStat/BossHuntv1/User/Health')";
        }
    }
}