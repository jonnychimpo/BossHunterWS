using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebService5.REST.Helper;

namespace WebService5.REST.Relics
{
    public interface IRelic
    {
        string GetRelicStat(RelicType type, RelicStat stat);
        BaseRelic GetRelic(string relic);
    }
}