using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebService5.REST.Helper;

namespace WebService5.REST
{
    public class Entity
    {
        public string PartitionKey; // GameID
        public string RowKey; // Name of the Hero
        public string Name;
        public int Attack;
        public int Health;
        public int Speed;
        public double Accuracy;
        public string Image;
        public int XP;
        public int AttackCount;
        public int RespawnCount;
        public int Score;
        public int DefaultHealth;
        public int AddedHealth;
        public int CurrentThreshold;
        public int ThresholdValue;
        public int origSpeed;

        public Entity()
        {

        }

        public void ScoreChanged()
        {
            //ChangeStat(WhichStat.AttackCount, ChangeStatDirection.Up, 1);
            this.Score = CalculateScore(this.AttackCount, this.RespawnCount);
        }

        public static int CalculateScore(int atkCount, int rswnCount)
        {
            int AttackMultiplier = 1;
            int RespawnMultiplier = 5;

            return (atkCount * AttackMultiplier) + (rswnCount * RespawnMultiplier);
        }

        public void ChangeStat(EntityStat stat, ChangeStatDirection dir, int amt2Change)
        {
            // Change the indicated Stat by adding or subtracting the amt2Change
            // Subtraction is done by multiplying the amt2Change by -1 which creates a negative value that is then "added" to the existing stat.
            switch(stat)
            {
                case EntityStat.Health:
                    if (dir == ChangeStatDirection.Set)
                    {
                        this.Health = amt2Change;
                    }
                    else
                    {
                        if (dir == ChangeStatDirection.Down) { amt2Change *= (-1); }
                        this.Health += amt2Change;
                    }
                    break;
                case EntityStat.Attack:
                    if (dir == ChangeStatDirection.Set)
                    {
                        this.Attack = amt2Change;
                    }
                    else
                    {
                        if (dir == ChangeStatDirection.Down) { amt2Change *= (-1); }
                        this.Attack += amt2Change;
                    }
                    break;
                case EntityStat.AttackCount:
                    if (dir == ChangeStatDirection.Set)
                    {
                        this.AttackCount = amt2Change;
                    }
                    else
                    {
                        if (dir == ChangeStatDirection.Down) { amt2Change *= (-1); }
                        this.AttackCount += amt2Change;
                    }
                    break;
                case EntityStat.RespawnCount:
                    if (dir == ChangeStatDirection.Set)
                    {
                        this.RespawnCount = amt2Change;
                    }
                    else
                    {
                        if (dir == ChangeStatDirection.Down) { amt2Change *= (-1); }
                        this.RespawnCount += amt2Change;
                    }
                    break;
                case EntityStat.XP:
                    if (dir == ChangeStatDirection.Set)
                    {
                        this.XP = amt2Change;
                    }
                    else
                    {
                        if (dir == ChangeStatDirection.Down) { amt2Change *= (-1); }
                        this.XP += amt2Change;
                    }
                    break;
                case EntityStat.Accuracy:
                    if (dir == ChangeStatDirection.Set)
                    {
                        this.Accuracy = amt2Change;
                    }
                    else
                    {
                        if (dir == ChangeStatDirection.Down) { amt2Change *= (-1); }
                        this.Accuracy += amt2Change;
                    }
                    break;
                case EntityStat.Speed:
                    if (dir == ChangeStatDirection.Set)
                    {
                        this.Speed = amt2Change;
                    }
                    else
                    {
                        if (dir == ChangeStatDirection.Down) { amt2Change *= (-1); }
                        this.Speed += amt2Change;
                    }
                    break;
            }
        }

        public int GetBossThresholdValue(int thresholdPCT)
        {
            // thresholdPCT is the PCT value of total health that each threshold is hit.
            // For example if the value is 25, then the threshold will be "hit" when the boss's health hits 25% less
            double tempVar = 100 - (this.Health / this.DefaultHealth) * 100;

            return Convert.ToInt32(Math.Floor(tempVar / thresholdPCT)); 
        }

        public Entity(string rKey)
        {
            PartitionKey = "Blah";
            RowKey = rKey;
            AddedHealth = 0;
            Attack = 1;
            Health = 11 + AddedHealth;
            Speed = 5;
            Accuracy = 50;
            Image = "C:\\Users\\jthoma\\Pictures\\HeroMytes\\creature.png";
            XP = 0;
            AttackCount = 0;
            RespawnCount = 0;
            Score = 0;
        }

        public Entity(string pKey, string rKey, string attack, string health, string speed, string accuracy, string image)
        {
            PartitionKey = pKey;
            RowKey = rKey;
            Health = Convert.ToInt32(health);
            Attack = Convert.ToInt32(attack);
            Speed = Convert.ToInt32(speed);
            Accuracy = Convert.ToDouble(accuracy);
            Image = image;
        }
    }
}