using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class SineGeneratingAgent : Agent
    {
        public SineGeneratingAgent(int ID, int frequency) : base(ID, frequency) { }
        private float i = 0;

        public double Output { get; private set; }

        public override void Update()
        {
            Output = Math.Sin(i);
            i += 0.1f;
            if (i >= ID % 10)        // 20 % 10 
            {
                HasFinished = true;
                Console.Write("SineGeneratingAgent {0}\n", ID);
            }
        }
    }
}
