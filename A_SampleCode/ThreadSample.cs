using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading; 

namespace ThreadTest
{
    class ThreadSample
    {
        public class Worker
        {
            // This method will be called when the thread is started.
            // 스레드가 실행될 때 호출되는 DoWork 메서드
            // 0부터 9까지의 값을 반복하여 출력, 각 출력은 [ThreadX] i 형식 
            public void DoWork()
            {
                for (int i=0; i<10; i++)
                {
                    Console.WriteLine("[Thread"+num+"] "+i);

                    Thread.Sleep(1);
                }
            }
            public int num;
        }

        static void Main(string[] args)
        {
            // Create the thread object. This does not start the thread. 
            Worker workerObject1 = new Worker();
            workerObject1.num = 1;
            Thread workerThread1 = new Thread(workerObject1.DoWork);

            Worker workerObject2 = new Worker();
            workerObject2.num = 2;
            Thread workerThread2 = new Thread(workerObject2.DoWork);


            // Start the worker thread. 
            workerThread1.Start();
            workerThread2.Start();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("[Main] "+i);
                // Put the main thread to sleep for 1 millisecond to 
                // allow the worker thread to do some work: 
                Thread.Sleep(1);
            }

            // Use the Join method to block the current thread  
            // until the object's thread terminates. 
            workerThread1.Join();
            workerThread2.Join();
        }
    }
}
