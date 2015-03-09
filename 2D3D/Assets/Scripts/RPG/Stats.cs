using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.RPG
{
    public struct Stats
    {
        public int Health { get; set; }
        public int MinAttack { get; set; }
        public int MaxAttack { get; set; }
        public int Accuracy { get; set; }
        public int Armor { get; set; }
        public int Speed { get; set; }
    }

    public struct BattleStats
    {
        public int MaxHealth { get; set; }
        public int MinHealth { get; set; }
        public int MinAttack { get; set; }
        public int MaxAttack { get; set; }
        public int Accuracy { get; set; }
        public int Armor { get; set; }
        public int Speed { get; set; }     
   
    }
}
