using Pokey_Men.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokey_Men
{
    public class Fighter : Man
    {
        public Man opponent;
        public bool oCanHeal = true;
        public int energy = 10;
        public Fighter(int a, int h) : base(a, h) {

            opponent = new Man(a - 10, h + 5 );
             
        }


        public void CompTurn()
        {
           
            int damage = 10;

                if (damage >= 6)
                {
                    this.Health -= (opponent.Attack) / 5;
                    damage -= 4;
                }
                else if (damage >= 3)
                {
                    this.Health -= (opponent.Attack) / 6;
                    damage -= 1;
                }
                else if (damage >= 1)
                {
                    this.Health -= (opponent.Attack) / 8;
                    damage++;
                }
                else
                {
                    this.Health -= (opponent.Attack) / 10;
                damage += 2;

                }
            

        }

        public void a1()
        {
            opponent.Hurt(Attack / 5);
            energy -= 6;
        }

        public void a2()
        {
            opponent.Hurt(Attack / 6);
            energy -= 3;
        }

        public void a3()
        {
            opponent.Hurt(Attack / 7);
            energy -= 1;
        }

        public void a4()
        {
            opponent.Hurt(Attack / 10);
            
        }


    }
}
