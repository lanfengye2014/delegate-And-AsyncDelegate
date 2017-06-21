using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;

namespace 委托和异步方法
{
    public delegate int AddDelegate(int x ,int y);
    public class Program
    {
        //执行回调方法的线程并非客户端线程Main Thread
        public static void onAddCom(IAsyncResult asyncResult) {
            AsyncResult result = (AsyncResult)asyncResult;
            AddDelegate del= (AddDelegate)result.AsyncDelegate;
            string data = (string)asyncResult.AsyncState;
            int rtn =del.EndInvoke(asyncResult);
            Console.WriteLine("{0}: Result, {1}; Data: {2} ",
            Thread.CurrentThread.Name, rtn, data);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Client application started! ");
            Thread.CurrentThread.Name = "Main Thread";

            Calculator cal = new Calculator();

            AddDelegate del = new AddDelegate(cal.add);


            //同步调用
            int result= (int)del.DynamicInvoke(new object[] { 1, 2 });

            Console.WriteLine("DynamicInvoke Result: {0}", result);

            AsyncCallback callback = new AsyncCallback(onAddCom);

            //异步调用
            IAsyncResult asyncResult = del.BeginInvoke(1, 2, callback, "This is a data string");



            for (int i = 1; i <= 3; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(i));
                Console.WriteLine("{0}: Client executed {1} second(s).",
                    Thread.CurrentThread.Name, i);
            }

            Console.WriteLine(" Press any key to exit...");
            Console.ReadKey();

        }
    }
}
