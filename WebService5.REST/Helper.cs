using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebService5.REST.Relics;

namespace WebService5.REST
{
    public static class Helper
    {
        public static string sAS = "?sv=2018-03-28&ss=bfqt&srt=sco&sp=rwdlacup&se=2020-04-29T23:58:30Z&st=2019-04-29T15:58:30Z&spr=https&sig=%2FOQ2bbOuOhO8ygpdD9RIfWzUbuf4F6%2Bu4OFG62UltWE%3D";
        public static string storageName = "pinhighprogdata";
        public static string newLine = "<br>";

        public enum RelicStat
        {
            PartitionKey,
            RowKey,
            Stat2Mod,
            ModValue,
            Desc
        }

        public enum RelicType
        {
            Attack,
            Health,
            Speed,
            Accuracy
        }

        public enum EntityStat
        {
            Attack,
            Health,
            Speed,
            Accuracy,
            AttackCount,
            RespawnCount,
            Score,
            DefaultHealth,
            PartitionKey,
            RowKey,
            CurrentThreshold,
            OrigSpeed,
            XP,
            AddedHealth
        }

        public enum ChangeStatDirection
        {
            Up,
            Down,
            Set
        }

        //public static HeroEntity CalculateScore(HeroEntity tempHero)
        //{
        //    int AttackMultiplier = 1;
        //    int RespawnMultiplier = 5;

        //    tempHero.Score = (tempHero.AttackCount * AttackMultiplier) + (tempHero.RespawnCount * RespawnMultiplier);

        //    return tempHero;
        //}

        //public static IEntity GetEntity(EntityType type)
        //{
        //    switch(type)
        //    {
        //        case EntityType.User:
        //            return new Hero();
        //        case EntityType.Boss:
        //            return new Boss();
        //        default:
        //            throw new NotSupportedException();
        //    }
        //}

        public static int GetBossThresholdValue(AnyEntity theBoss, int thresholdPCT)
        {
            // thresholdPCT is the PCT value of total health that each threshold is hit.
            // For example if the value is 25, then the threshold will be "hit" when the boss's health hits 25% less
            int theThreshold = 0;

            double tempVar = 100 - (double)theBoss.Health / (double)theBoss.DefaultHealth * 100;
            theThreshold = Convert.ToInt32(Math.Floor(tempVar / thresholdPCT));

            return theThreshold;
            //return Convert.ToInt32(tempVar % thresholdPCT);
        }
        //public static int CheckAccuracyHit(AnyEntity tempEntity)
        //{
        //    // return 1 if hit, and 0 if miss
        //    Random randNum = new Random();
        //    int num = randNum.Next(100);

        //    return num <= tempEntity.Accuracy ? 1 : 0;
        //}
        public static Attack SwitchRoles(Attack currentAttack, IEntity currentAttacker, IEntity currentDefender)
        {
            Attack tAttack = currentAttack;

            tAttack.Attacker = currentDefender;
            tAttack.Defender = currentAttacker;

            return tAttack;
        }
        public static HeroEntity IncreaseStat(HeroEntity tempHero, string stat2inc)
        {

            switch (stat2inc)
            {
                case "Attack":
                    tempHero.Attack++;
                    break;
                case "Health":
                    tempHero.Health += 3;
                    tempHero.AddedHealth += 3;
                    //tempHero.DefaultHealth++;
                    break;
                case "Speed":
                    tempHero.Speed++;
                    break;
                case "Accuracy":
                    tempHero.Accuracy += 5;
                    break;
            }

            return tempHero;
        }

        public static int CreateRelics(string table)
        {
            int responseCode;
            string jsonGetData = string.Empty;

            // #########################################################################################
            // Create the Attack Relic
            BaseRelic AtkRelic = new AttackRelic();
            responseCode = JTAzureTableRESTCommands.GetAllEntities(storageName, sAS, table + "(PartitionKey='" + AtkRelic.PartitionKey + "',RowKey='" + AtkRelic.RowKey + "')", out jsonGetData);
            if (responseCode == 7)
            {
                responseCode = JTAzureTableRESTCommands.ChangeEntity("POST", storageName, sAS, table, JsonConvert.SerializeObject(AtkRelic), string.Empty, string.Empty);
            }
            else
            {
                responseCode = 201;
            }
            // #########################################################################################

            // #########################################################################################
            // Create the Health Relic
            BaseRelic HPRelic = new HealthRelic();
            responseCode = JTAzureTableRESTCommands.GetAllEntities(storageName, sAS, table + "(PartitionKey='" + HPRelic.PartitionKey + "',RowKey='" + HPRelic.RowKey + "')", out jsonGetData);
            if (responseCode == 7)
            {
                responseCode = JTAzureTableRESTCommands.ChangeEntity("POST", storageName, sAS, table, JsonConvert.SerializeObject(HPRelic), string.Empty, string.Empty);
            }
            else
            {
                responseCode = 201;
            }
            // #########################################################################################

            // #########################################################################################
            // Create the Speed Relic
            BaseRelic SPRelic = new SpeedRelic();
            responseCode = JTAzureTableRESTCommands.GetAllEntities(storageName, sAS, table + "(PartitionKey='" + SPRelic.PartitionKey + "',RowKey='" + SPRelic.RowKey + "')", out jsonGetData);
            if (responseCode == 7)
            {
                responseCode = JTAzureTableRESTCommands.ChangeEntity("POST", storageName, sAS, table, JsonConvert.SerializeObject(SPRelic), string.Empty, string.Empty);
            }
            else
            {
                responseCode = 201;
            }
            // #########################################################################################

            // #########################################################################################
            // Create the Accuracy Relic
            BaseRelic AccRelic = new AccuracyRelic();
            responseCode = JTAzureTableRESTCommands.GetAllEntities(storageName, sAS, table + "(PartitionKey='" + AccRelic.PartitionKey + "',RowKey='" + AccRelic.RowKey + "')", out jsonGetData);
            if (responseCode == 7)
            {
                responseCode = JTAzureTableRESTCommands.ChangeEntity("POST", storageName, sAS, table, JsonConvert.SerializeObject(AccRelic), string.Empty, string.Empty);
            }
            else
            {
                responseCode = 201;
            }
            // #########################################################################################

            return responseCode;
        }
    }
}