using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebService5.REST.Helper;

namespace WebService5.REST
{
    public class BaseRelic
    {
        public string PartitionKey; // This will always be "Relic"
        public string RowKey; // Name of the Relic

        public EntityStat Stat2Mod;
        public int ModValue; // Value to increment the User's Stats by
        public string Desc;
        public string Image;
        public string OnClick;
        public int ID;

        public BaseRelic(string name, EntityStat stat, int modvalue, string desc)
        {
            //PartitionKey = "Relic";
            //RowKey = name;
            //Stat2Mod = stat;
            //ModValue = modvalue;
            //Desc = desc;
        }

        public BaseRelic()
        {

        }
    }
}