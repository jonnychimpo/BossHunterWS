using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebService5.REST.Relics;
using static WebService5.REST.Helper;

namespace WebService5.REST
{
    public class Hero : IEntity
    {
        Entity newHero = new HeroEntity();

        public Entity LoadEntity(string jsonData)
        {
            //Entity newHero = new Entity();
            newHero = JsonConvert.DeserializeObject<HeroEntity>(jsonData);
            SyncProperties();
            return newHero;
        }

        public Entity CreateBlankEntity()
        {
            //Entity newHero = new Entity();
            newHero = new HeroEntity("new");
            SyncProperties();
            //Hero blankHero = JsonConvert.DeserializeObject<Hero>(JsonConvert.SerializeObject(newHero));
            //this.Name = "Test";
            return newHero;
        }

        public void CalculateEntityScore()
        {
            newHero.ScoreChanged();
            SyncProperties();
        }

        public void ChangeEntityStat(EntityStat stat, ChangeStatDirection dir, int amt2Change)
        {
            newHero.ChangeStat(stat, dir, amt2Change);
            SyncProperties();
        }

        public void ChangeEntityStatByRelic(string relic)
        {
            IRelic loadRelic = new Relic();
            BaseRelic tRelic = new BaseRelic();
            tRelic = loadRelic.GetRelic(relic);
                        
            newHero.ChangeStat(tRelic.Stat2Mod, ChangeStatDirection.Up, tRelic.ModValue);
            SyncProperties();
        }

        public string GetEntityStat(EntityStat stat)
        {
            switch (stat)
            { 
                case EntityStat.Speed:
                    return newHero.Speed.ToString();
                case EntityStat.Attack:
                    return newHero.Attack.ToString();
                case EntityStat.Health:
                    return newHero.Health.ToString();
                case EntityStat.Accuracy:
                    return newHero.Accuracy.ToString();
                case EntityStat.AttackCount:
                    return newHero.AttackCount.ToString();
                case EntityStat.RespawnCount:
                    return newHero.RespawnCount.ToString();
                case EntityStat.Score:
                    return newHero.Score.ToString();
                case EntityStat.DefaultHealth:
                    return newHero.DefaultHealth.ToString();
                case EntityStat.PartitionKey:
                    return newHero.PartitionKey;
                case EntityStat.RowKey:
                    return newHero.RowKey;
                case EntityStat.AddedHealth:
                    return newHero.AddedHealth.ToString();
                default:
                    return "No Stat Defined"; // No Stat Defined
            }
        }

        public int CheckEntityHit()
        {
            Random randNum = new Random();
            int num = randNum.Next(100);

            return num <= newHero.Accuracy ? 1 : 0;
        }

        public int GetEntityThresholdValue(int threshold)
        {
            return 0;
        }

        public void SyncProperties()
        {
            this.PartitionKey = newHero.PartitionKey;
            this.RowKey = newHero.RowKey;
            this.Name = newHero.Name;
            this.Attack = newHero.Attack;
            this.Health = newHero.Health;
            this.Speed = newHero.Speed;
            this.Accuracy = newHero.Accuracy;
            this.Image = newHero.Image;
            this.XP = newHero.XP;
            this.AttackCount = newHero.AttackCount;
            this.RespawnCount = newHero.RespawnCount;
            this.Score = newHero.Score;
            this.DefaultHealth = newHero.DefaultHealth;
            this.AddedHealth = newHero.AddedHealth;
            this.CurrentThreshold = newHero.CurrentThreshold;
            this.ThresholdValue = newHero.ThresholdValue;
            this.origSpeed = newHero.origSpeed;
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