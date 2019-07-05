using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebService5.REST.Helper;

namespace WebService5.REST.Relics
{
    public class Relic : IRelic
    {
        BaseRelic tempBaseRelic = new BaseRelic();

        public BaseRelic GetRelic(string relic)
        {
            RelicType result;
            if (Enum.TryParse<RelicType>(relic, out result))
            {
                switch(result)
                {
                    case RelicType.Attack:
                        tempBaseRelic = new AttackRelic();
                        break;
                    case RelicType.Health:
                        tempBaseRelic = new HealthRelic();
                        break;
                    case RelicType.Speed:
                        tempBaseRelic = new SpeedRelic();
                        break;
                    case RelicType.Accuracy:
                        tempBaseRelic = new AccuracyRelic();
                        break;
                }
                
            }

            return tempBaseRelic;
        }

        public string GetRelicStat(RelicType type, RelicStat stat)
        {
            switch (type)
            {
                case RelicType.Attack:
                    tempBaseRelic = new AttackRelic();
                    break;
                case RelicType.Health:
                    tempBaseRelic = new HealthRelic();
                    break;
                case RelicType.Speed:
                    tempBaseRelic = new SpeedRelic();
                    break;
                case RelicType.Accuracy:
                    tempBaseRelic = new AccuracyRelic();
                    break;
            }

            switch (stat)
            {
                case RelicStat.PartitionKey:
                    return tempBaseRelic.PartitionKey;
                case RelicStat.RowKey:
                    return tempBaseRelic.RowKey;
                case RelicStat.Stat2Mod:
                    return tempBaseRelic.Stat2Mod.ToString();
                case RelicStat.ModValue:
                    return tempBaseRelic.ModValue.ToString();
                case RelicStat.Desc:
                    return tempBaseRelic.Desc;
                default:
                    return "No Stat Defined"; // No Stat Defined
            }
        }
    }
}