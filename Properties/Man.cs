using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokey_Men.Properties
{
    public class Man
    {
        

    private int attack;
        public int Attack
        {
            get
            { return attack; }
            set
            { attack = value; }

        }

        private int health;
        public int Health
        {
            get
            { return health; }
            set
            { health = value; }

        }

        private int energy;
        public int Energy
        {
            get
            { return energy; }
            set
            { energy = value; }

        }

        public Man(int a, int h)
        {
            this.attack = a;
            this.health = h;
            this.energy = 10;
         
        
        }

        public void Heal(int h)
        { 
            
            this.Health += h; 
        
        }

        public void Hurt(int d)
        {
            Health -= d;

        }

    }
}
