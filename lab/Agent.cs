using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab
{
    abstract class Agent:IRunnable
    {
        private bool _HasFinished;
        public int ID;
        private int frequency;

        public Agent(int ID,int frequency)
        {
            this.ID = ID;
            this.frequency = frequency;
        }
        public bool HasFinished
        {
            get { return _HasFinished; }
            set { _HasFinished = value;}
        }

        public IEnumerator<float> CoroutineUpdate()
        {
            for (float i=0; !HasFinished; i++)
            {
                Update();
                Thread.Sleep(frequency);
                yield return i;
                
            }
            yield break;
        }

        public void Run()
        {
            for(; !HasFinished;)
            {
                Update();
                Thread.Sleep(frequency);
            }
        }

        abstract public void Update();

    }
}
