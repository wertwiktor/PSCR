using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab
{
    class Program
    {

        //paramerty
        static int n = 5; //ilosc agentow
        static int m = 1000; //rozmiar listy
        static int e = 5;//ilosc etapów

        static List<IRunnable> GenerateRunnables()
        {
            var runnables = new List<IRunnable>();
            List<IRunnable> agenty = new List<IRunnable>();

            for (int i = 0; i < n; i++)
            {
                agenty.Add(new AddAgent(i, L1, m / (e * n)));
            }
            return agenty;
        }

        static List<int> L1;

        public static void MakeL1(int howMany)
        {
            Random rnd = new Random(1);
            L1 = new List<int>();
            for (int i = 0; i < howMany; i++)
            {
                L1.Add(rnd.Next(1, 100));
            }
        }

        public static void RunThreads()         // http://www.albahari.com/threading/
        {
            List<Thread> startedThread = new List<Thread>();
            List<IRunnable> agenty = new List<IRunnable>(GenerateRunnables());

            for (int i = 0; i < e; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Thread thread = new Thread(agenty.ElementAt(j).Run);
                    thread.Start();
                    startedThread.Add(thread);

                }
                foreach (Thread started in startedThread)
                    started.Join();

            }
        }

        public static void ShowList(List<int> L)
        {
            Console.WriteLine("");
            Console.WriteLine("Elementy listy:");
            foreach (int element in L)
            {
                Console.WriteLine("{0}", element);
            }
            Console.WriteLine("");
        }

        public static void SUMList(List<int> L)
        {

            int sumax = 0;
            foreach (int element in L)
            {
                sumax = sumax + element;
            }
            Console.WriteLine("SUMA LISTY: {0}", sumax);
        }


        static void RunFiber()
        {
            List<IRunnable> agenty = new List<IRunnable>(GenerateRunnables());
            var mlist = agenty.Select(p => p.CoroutineUpdate());
            while (agenty.Where(p => p.HasFinished == false).Any())
            {
                foreach (var a in mlist)
                {
                    a.MoveNext();
                }
                //Console.WriteLine("KOLEJNY ETAP");
            } 
            int sumay = 0;
           
            foreach(IRunnable agent in agenty)
                {
                sumay = sumay + agent.suma1;
                }
            Console.WriteLine("SUMA KONCOWA: {0}",sumay);
        }
        
        static void Main(string[] args)
        {

            //RunFiber();
            MakeL1(m);
            //ShowList(L1);
            SUMList(L1);
            RunFiber();

            Console.ReadKey();
        }
    }
}
