using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class ConstantCountingAgent:Agent
    {
        public ConstantCountingAgent(int ID, int frequency) : base(ID, frequency) { }
        private int i = 0;

        public override void Update()
        {
            i++;
            if (i == 10)
            {
                Console.WriteLine("CountingAgent{0}: {1}", ID, i);
            }
        }
    }
}
