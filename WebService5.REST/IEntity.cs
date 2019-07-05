using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebService5.REST.Helper;

namespace WebService5.REST
{
    public interface IEntity
    {
        Entity LoadEntity(string jsonData);
        Entity CreateBlankEntity();
        void CalculateEntityScore();
        string GetEntityStat(EntityStat stat);
        int CheckEntityHit();
        void ChangeEntityStat(EntityStat stat, ChangeStatDirection dir, int amt2Change);
        void ChangeEntityStatByRelic(string whichrelic);
        int GetEntityThresholdValue(int threshold);

        string PartitionKey { get; set; }
        string RowKey { get; set; }
        string Name { get; set; }
        int Attack { get; set; }
        int Health { get; set; }
        int Speed { get; set; }
        double Accuracy { get; set; }
        string Image { get; set; }
        int XP { get; set; }
        int AttackCount { get; set; }
        int RespawnCount { get; set; }
        int Score { get; set; }
        int DefaultHealth { get; set; }
        int AddedHealth { get; set; }
        int CurrentThreshold { get; set; }
        int ThresholdValue { get; set; }
        int origSpeed { get; set; }
    }
}