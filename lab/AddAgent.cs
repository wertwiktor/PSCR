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
        List<int> L1;
        List<int> L2;
        public AddAgent(List<int> L1, List<int> L2)
        {
            this.L1 = L1;
            this.L2 = L2;
        }

        public bool HasFinished
        {
            get { return _HasFinished; }
            set { _HasFinished = value; }
        }

        public IEnumerator<float> CoroutineUpdate()
        {
            for (; !HasFinished;)
            {
                Update();

            }
            yield break;
        }
        public void Run()
        {
            if (!L1.Any())
            {
                HasFinished = true;
                Console.WriteLine("KONIEC LISTY");
            }
            else
                Update();
            
        }

        public void Update()
        {
            int suma = 0;

                for (int i = 0; (i < 4) && L1.Any(); i++)
                {
                    var item = L1.First();
                    L1.Remove(item);
                    suma = suma + item;
                Console.WriteLine("suma{1}: {0}", suma,i);
            }
            
            // L2.Add(suma);
        }
        }
    }

