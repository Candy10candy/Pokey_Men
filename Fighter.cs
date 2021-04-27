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
     
        public int energy = 10;
        public Fighter(int a, int h) : base(a, h) {

         
             
        }


        
        

        public override string ToString()
        {
            return "Health: " +  this.Health + " Attack: " + this.Attack + " Energy: " + energy;
        }

    }
}
