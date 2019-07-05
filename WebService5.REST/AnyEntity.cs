using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService5.REST
{
    public class AnyEntity : BossEntity
    {
        //public HeroEntity aHero;
        //public BossEntity aBoss;

        public AnyEntity(HeroEntity theHero)
        {
            PartitionKey = theHero.PartitionKey;
            RowKey = theHero.RowKey;
            Health = theHero.Health;
            Attack = theHero.Attack;
            Speed = theHero.Speed;
            Accuracy = theHero.Accuracy;
            XP = theHero.XP;
            AttackCount = theHero.AttackCount;
            Image = theHero.Image;
            AddedHealth = theHero.AddedHealth;
            RespawnCount = theHero.RespawnCount;
            Score = theHero.Score;
        }

        public AnyEntity(BossEntity theBoss)
        {
            PartitionKey = theBoss.PartitionKey;
            RowKey = theBoss.RowKey;
            Attack = theBoss.Attack;
            Health = theBoss.Health;
            Speed = theBoss.Speed;
            Accuracy = theBoss.Accuracy;
            ThresholdValue = theBoss.ThresholdValue;
            Image = theBoss.Image;
            DefaultHealth = theBoss.DefaultHealth;
            CurrentThreshold = theBoss.CurrentThreshold;
            origSpeed = theBoss.origSpeed;
        }

    }
}