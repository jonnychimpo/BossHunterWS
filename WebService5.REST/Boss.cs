using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebService5.REST.Helper;

namespace WebService5.REST
{
    public class Boss : IEntity
    {
        Entity newBoss = new BossEntity();

        public Entity LoadEntity(string jsonData)
        {
            //Entity newHero = new Entity();
            newBoss = JsonConvert.DeserializeObject<HeroEntity>(jsonData);
            SyncProperties();
            return newBoss;
        }

        public Entity CreateBlankEntity()
        {
            //Entity newHero = new Entity();
            newBoss = new BossEntity("new");
            SyncProperties();
            //Hero blankBoss = JsonConvert.DeserializeObject<Hero>(JsonConvert.SerializeObject(newBoss));
            //return newBoss;
            return newBoss;
        }

        public void CalculateEntityScore()
        {
            newBoss.ScoreChanged();
            SyncProperties();
        }

        public void ChangeEntityStat(EntityStat stat, ChangeStatDirection dir, int amt2Change)
        {
            newBoss.ChangeStat(stat, dir, amt2Change);
            SyncProperties();
        }

        public void ChangeEntityStatByRelic(string relic)
        {
            //newBoss.ChangeStat(stat, dir, amt2Change);
            //SyncProperties();
        }

        public string GetEntityStat(EntityStat stat)
        {
            switch (stat)
            {
                case EntityStat.Speed:
                    return newBoss.Speed.ToString();
                case EntityStat.Attack:
                    return newBoss.Attack.ToString();
                case EntityStat.Health:
                    return newBoss.Health.ToString();
                case EntityStat.Accuracy:
                    return newBoss.Accuracy.ToString();
                case EntityStat.AttackCount:
                    return newBoss.AttackCount.ToString();
                case EntityStat.RespawnCount:
                    return newBoss.RespawnCount.ToString();
                case EntityStat.Score:
                    return newBoss.Score.ToString();
                case EntityStat.DefaultHealth:
                    return newBoss.DefaultHealth.ToString();
                case EntityStat.PartitionKey:
                    return newBoss.PartitionKey;
                case EntityStat.RowKey:
                    return newBoss.RowKey;
                case EntityStat.CurrentThreshold:
                    return newBoss.CurrentThreshold.ToString();
                case EntityStat.AddedHealth:
                    return newBoss.AddedHealth.ToString();
                default:
                    return "No Stat Defined"; // No Stat Defined
            }
        }

        public int CheckEntityHit()
        {
            Random randNum = new Random();
            int num = randNum.Next(100);

            return num <= newBoss.Accuracy ? 1 : 0;
        }

        public int GetEntityThresholdValue(int threshold)
        {
            return newBoss.GetBossThresholdValue(threshold);
        }

        public void SyncProperties()
        {
            this.PartitionKey = newBoss.PartitionKey;
            this.RowKey = newBoss.RowKey;
            this.Name = newBoss.Name;
            this.Attack = newBoss.Attack;
            this.Health = newBoss.Health;
            this.Speed = newBoss.Speed;
            this.Accuracy = newBoss.Accuracy;
            this.Image = newBoss.Image;
            this.XP = newBoss.XP;
            this.AttackCount = newBoss.AttackCount;
            this.RespawnCount = newBoss.RespawnCount;
            this.Score = newBoss.Score;
            this.DefaultHealth = newBoss.DefaultHealth;
            this.AddedHealth = newBoss.AddedHealth;
            this.CurrentThreshold = newBoss.CurrentThreshold;
            this.ThresholdValue = newBoss.ThresholdValue;
            this.origSpeed = newBoss.origSpeed;
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Name { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public double Accuracy { get; set; }
        public string Image { get; set; }
        public int XP { get; set; }
        public int AttackCount { get; set; }
        public int RespawnCount { get; set; }
        public int Score { get; set; }
        public int DefaultHealth { get; set; }
        public int AddedHealth { get; set; }
        public int CurrentThreshold { get; set; }
        public int ThresholdValue { get; set; }
        public int origSpeed { get; set; }
    }
}