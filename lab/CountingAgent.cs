using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class CountingAgent:Agent
    {
        public CountingAgent(int ID,int frequency) : base(ID, frequency) { }
        private int i=0;

        public override void Update()
        {
            i++;
            if (i == ID)
            {
                Console.WriteLine("CountingAgent{0}: {1}", ID, i);
            }
        }
    }
}
