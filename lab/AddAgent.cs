using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class AddAgent : IRunnable
    {
        private bool _HasFinished;
        int howMuch;
        int id;
        public int suma1
        {
            get { return suma; }

        }
        int suma = 0;
        List<int> L1;
        public AddAgent(int id, List<int> L1, int howMuch)
        {
            this.L1 = L1;
            this.id = id;
            this.howMuch = howMuch;
            suma = 0;
        }

        public bool HasFinished
        {
            get { return _HasFinished; }
            set { _HasFinished = value; }
        }

        public IEnumerator<float> CoroutineUpdate()
        {

            for (int i = 0; L1.Any(); i++)
            {
                Update();
                yield return i;
            }
            HasFinished = true;
            yield break;
        }
        public void Run()
        {
            lock (L1)
            {
                Update();
                L1.Insert(0, suma);
                suma = 0;
            }
        }

        public void Update()
        {

            Console.Write("{0}ADD ", id);
            for (int j = 0; (j < howMuch) && L1.Any(); j++)
            {
                var item = L1.First();
                L1.Remove(item);
                Console.Write("{0} ", item);
                suma = suma + item;
            }
            Console.Write("={0}\n", suma);

        }
    }
}

