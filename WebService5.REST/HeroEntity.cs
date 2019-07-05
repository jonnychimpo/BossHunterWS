using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService5.REST
{
    public class HeroEntity : Entity
    {
        public HeroEntity()
        {

        }

        public HeroEntity(string rKey)
        {
            PartitionKey = "Hero";
            RowKey = "User";
            Name = "JaggedRenn";
            AddedHealth = 0;
            Attack = 1;
            DefaultHealth = 11;
            Health = DefaultHealth + AddedHealth;
            Speed = 5;
            Accuracy = 50;
            Image = "C:\\Users\\jthoma\\Pictures\\HeroMytes\\creature.png";
            XP = 0;
            AttackCount = 0;
            RespawnCount = 0;
            Score = 0;
        }
    }
}