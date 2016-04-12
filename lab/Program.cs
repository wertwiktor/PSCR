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

       

        static List<IRunnable> GenerateRunnables()
        {
            var runnables = new List<IRunnable>();
            List<IRunnable> agenty = new List<IRunnable>();

           for (int i = 0; i < 4; i++)
            {
                agenty.Add(new AddAgent(L1,L2));
            }
            /*
            for (int i = 0; i < 100; i += 3)
            {
                agenty.Add(new ConstantCountingAgent(i, 10));
            }
            for (int i = 100; i < 200; i += 3)
            {
                
              //  agenty.Add(new CountingAgent(i, 1));
            }
            for (int i = 200; i < 300; i += 3)
            {
             
                //agenty.Add(new SineGeneratingAgent(i, 1));
            }
            */
            return agenty;
        }
       
        static List<int> L1;
        static List<int> L2;
        static List<int> L3;

        public static void MakeL1(int howMany)
        {
            Random rnd = new Random(1);
            L1 = new List<int>();
            L2 = new List<int>();
            L3 = new List<int>();
            for (int i = 0; i < howMany; i++)
            {
                L1.Add(rnd.Next(1,100));
            }
        }

        public static void RunThreads()         // http://www.albahari.com/threading/
        {
            List<Thread> startedThread = new List<Thread>();
            List<IRunnable> agenty = new List<IRunnable>(GenerateRunnables());

            foreach (IRunnable agent in agenty)
            {
                Thread thread = new Thread(agent.Run);
                thread.Start();
                thread.Join();
                startedThread.Add(thread);
            }
        }

        public static void ShowList(List<int> L)         
        {
            

            foreach (int element in L)
            {
                Console.WriteLine("{0}", element);
            }
        }


        static void RunFiber()
        {
            List<IRunnable> agenty = new List<IRunnable>(GenerateRunnables());
            var mlist = agenty.Select(p => p.CoroutineUpdate());
            do
            {
                foreach (var a in mlist)
                {
                    a.MoveNext();
                }

            } while (agenty.Where(p => p.HasFinished == false).Any());

        }
        static void Main(string[] args)
        {
            
            //RunFiber();
            MakeL1(10);
            ShowList(L1);
            RunThreads();
            Console.WriteLine("");
            ShowList(L1);

            Console.ReadKey();
        }
    }
}
