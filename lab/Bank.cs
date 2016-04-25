using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class Bank
    {
        private static Object tLock = new Object();
        private int suma;

        
        public Bank(int suma)
        {
            
           this.suma = suma;
            
        }

        public int Set(int ile)
        {
            
                suma = ile;
                return suma;
            
        }

        public int GetValue()
        {
                return suma;
            
        }
    }
}
