using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebService5.REST.Helper;

namespace WebService5.REST.Relics
{
    public class AttackRelic : BaseRelic
    {
        public AttackRelic()
        {
            ID = 1;
            PartitionKey = "Relic";
            RowKey = "Attack";
            Stat2Mod = EntityStat.Attack;
            ModValue = 1;
            Desc = "Increase Attack";
            Image = "C:\\Users\\jthoma\\Pictures\\HeroMytes\\sword.png";
            OnClick = "IncreaseStat('/IncreaseStat/BossHuntv1/User/Attack')";
        }
    }
}