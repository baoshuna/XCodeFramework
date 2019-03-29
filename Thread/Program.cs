using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Thread1
{
    class Program
    {
        static int counter = 10;

        static object CounterLockHelper = new object();

        static void Main(string[] args)
        {
            for (int i = 0; i < 20; i++)
            {
                ThreadPool.QueueUserWorkItem(Getmoney);

            }
            //ThreadPool.QueueUserWorkItem(Getmoney);
            //ThreadPool.QueueUserWorkItem(Getmoney);

            Console.ReadKey();
        }

        static void Transfer(object state)
        {
            counter++;

            Thread.Sleep(TimeSpan.FromSeconds(5));
        }

        static void Getmoney(object state)
        {
            lock (typeof(object))
            {
                if (counter >= 1)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    counter--;
                }


                Console.WriteLine(counter);
            }
        }
    }
}
