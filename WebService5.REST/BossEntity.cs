using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService5.REST
{
    public class BossEntity : Entity
    {
        public BossEntity()
        {

        }

        public BossEntity(string rKey)
        {
            PartitionKey = "Hero";
            RowKey = "Boss1";
            Name = "AngryCyclops";
            Attack = 10;
            Health = 100;
            Speed = 1;
            Accuracy = 85;
            Image = "C:\\Users\\jthoma\\Pictures\\HeroMytes\\Boss-AngryCyclops.jpg";
            DefaultHealth = Health;
            ThresholdValue = 10;
            CurrentThreshold = 0;
            origSpeed = Speed;
        }
    }
}