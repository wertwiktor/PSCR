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

        static int randFunctionGeneratorVarible = 1;
        static Bank bank;
        static int suma = 0;
        static int iterations = 100;

        static List<IRunnable> GenerateRunnables()
        {
            List<IRunnable> agenty = new List<IRunnable>();

            bank =  new Bank(0);
            Random rnd = new Random(randFunctionGeneratorVarible);

            for (int i = 0; i < iterations; i++)
            {
                int temp = rnd.Next(-100, 100);
                agenty.Add(new AddAgent(bank, temp));
                suma += temp;
            }
            return agenty;
        }


        public static void RunThreads()
        {
            List<Thread> startedThread = new List<Thread>();
            List<IRunnable> agenty = new List<IRunnable>(GenerateRunnables());

            foreach (IRunnable agent in agenty)
            {
                Thread thread = new Thread(agent.Run);
                thread.Start();
                startedThread.Add(thread);
            }

            foreach (Thread started in startedThread)
                started.Join();



        }
        static void Main(string[] args)
        {
            RunThreads();
            Console.WriteLine("Suma: {0} Suma:{1}", suma,bank.GetValue());
            Console.ReadKey();
        }
    }
}
