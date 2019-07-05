using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService5.REST
{
    public class Attack
    {
        public string PartitionKey;
        public string RowKey;

        public IEntity Attacker;
        public IEntity Defender;
        public int DamageDealt;
        public int DefenderDead;
        public int AttackCount;

        public Attack(IEntity attacker, IEntity defender, int damagedealt, int defenderdead)
        {
            PartitionKey = "Attack";
            RowKey = "1";
            AttackCount = 0;

            Attacker = attacker;
            Defender = defender;
            DamageDealt = damagedealt;
            DefenderDead = defenderdead;
        }
    }
}