using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.RPG
{
    class RpgCharacter
    {
        private int name;


        private Stats stats;
        private BattleStats battleStats;
        private bool isDead = true;

        public delegate void RpgCharacterEvent(RpgCharacter rpgchar);
        public event RpgCharacterEvent OnDeath;


        // constructor, default stats
        public RpgCharacter()
        {
            stats = new Stats() { Health = 100, Armor = 0, Accuracy = 100, MaxAttack = 50, MinAttack = 50, Speed = 100 };
            battleStats = new BattleStats() { MaxHealth = 100, CurHealth = 100, Armor = 0, Accuracy = 100, MaxAttack = 50, MinAttack = 50, Speed = 100 };
        }

        public void Attack(RpgCharacter target)
        {
            double attackDamage = UnityEngine.Random.Range((float)battleStats.MinAttack, (float)battleStats.MaxAttack);

            target.Defend(attackDamage);
        }

        public void Defend(double damage)
        {
            double result = damage - battleStats.Armor;

            if (damage > 0)
            {
                battleStats.CurHealth -= damage;

                if (battleStats.CurHealth <= 0)
                {
                    isDead = true;
                    OnDeath.Invoke(this);
                }
            }
        }

        // Properties!
	    public int Name
	    {
		    get { return name;}
		    set { name = value;}
	    }
        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }
        public Stats Stats
        {
            get { return stats; }
            set { stats = value; }
        }
        public BattleStats BattleStats
        {
            get { return battleStats; }
            set { battleStats = value; }
        }
    }
}
