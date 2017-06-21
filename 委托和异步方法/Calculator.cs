using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace 委托和异步方法
{
    public class Calculator
    {
        public int add(int x, int y)
        {
            if (Thread.CurrentThread.IsThreadPoolThread)
            {
                Thread.CurrentThread.Name = "PoolThread";
            }

            Console.WriteLine("Method invoked!");

            for (int i = 1; i <= 2; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(i));
                Console.WriteLine("{0}: Add executed {1} second(s).",
                    Thread.CurrentThread.Name, i);
            }
            Console.WriteLine("Method complete!");

            return x + y;
        }
    }
}
