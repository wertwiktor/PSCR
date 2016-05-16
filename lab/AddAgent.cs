using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab
{
    class AddAgent : IRunnable
    {
        public static Mutex mut = new Mutex();
        private bool _HasFinished;
        private int _numTryEnters;
        private int _numEnterFails;
        private int _numEnterSucceses;


        int howMuch;
        private static SpinLock _spinlock = new SpinLock(false);
        private static Object tLock = new Object();
        private static int usingResource = 0;
        Bank bank;

        public AddAgent(Bank bank, int howMuch)
        {
            this.bank = bank;
            this.howMuch = howMuch;
        }

        public bool HasFinished
        {
            get { return _HasFinished; }
            set { _HasFinished = value; }
        }

        public IEnumerator<float> CoroutineUpdate()
        {
            Update();

            yield break;
        }
        public void Run()
        {
            //lock(tLock)
            //UpdateAtomic();

            //UpdateWithSpin();
            UpdateWithSpinTryEnter();
        }

        private void UpdateWithSpin()
        {
            bool lockTaken = false;

            try
            {
                _spinlock.Enter(ref lockTaken);
                Random rnd = new Random(1);
                int temp = bank.GetValue(); //odczytaj
                Thread.Sleep(rnd.Next(0, 50));
                temp = howMuch + temp; //dodaj
                Thread.Sleep(rnd.Next(0, 50));
                bank.Set(temp);//zapisz
                Console.WriteLine("{0}", temp);

            }
            finally
            {
                if (lockTaken) _spinlock.Exit(false);
            }
        }


        private void UpdateWithSpinTryEnter()
        {
            bool lockTaken = false;

            try
            {
                while (!lockTaken)
                {
                    _numTryEnters++;
                    _spinlock.TryEnter(ref lockTaken);
                    if (lockTaken) _numEnterSucceses++;
                    else _numEnterFails++;
                }
                
                Random rnd = new Random(1);
                int temp = bank.GetValue(); //odczytaj
                Thread.Sleep(rnd.Next(0, 50));
                temp = howMuch + temp; //dodaj
                Thread.Sleep(rnd.Next(0, 50));
                bank.Set(temp);//zapisz
                Console.WriteLine("{0}", temp);
                Console.WriteLine("Runnable id {3} Spinlock enters: {0}  fails {1}   successes {2}", _numTryEnters, _numEnterFails, _numEnterSucceses,
                    Thread.CurrentThread.ManagedThreadId);

            }
            finally
            {
                if (lockTaken) _spinlock.Exit(false);
            }
        }

        private void UpdateAtomic()
        {
            while(0 == Interlocked.Exchange(ref usingResource, 1))
            {
             Random rnd = new Random(1);
            int temp = bank.GetValue(); //odczytaj
            Thread.Sleep(rnd.Next(0, 50));
            temp = howMuch + temp; //dodaj
            Thread.Sleep(rnd.Next(0, 50));
            bank.Set(temp);//zapisz
            Console.WriteLine("{0}", temp);
            Interlocked.Exchange(ref usingResource, 0);
            }
            

        }

        private void Update()
        {

            Random rnd = new Random(1);
            //mut.WaitOne();
            int temp = bank.GetValue(); //odczytaj
            Thread.Sleep(rnd.Next(0, 50));
            temp = howMuch + temp; //dodaj
            Thread.Sleep(rnd.Next(0, 50));
            bank.Set(temp);//zapisz
            Console.WriteLine("{0}", temp);
            //mut.ReleaseMutex();


        }
    }
}

