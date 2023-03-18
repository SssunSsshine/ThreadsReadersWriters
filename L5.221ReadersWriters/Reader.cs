using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace L5._221ReadersWriters
{
    class Reader
    {
        const int MAX_COUNT = 1;
        const int INIT_COUNT = 1;
        static Semaphore sem = new Semaphore(INIT_COUNT, MAX_COUNT);
        Thread readerThread;
        public Reader(int numReader, Queue<Record> queue)
        {
            readerThread = new Thread(Read);
            readerThread.Name = $"Reader {numReader}";
            readerThread.Start(queue);
        }

        public void Read(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Queue<Record>))
                return;
            Queue<Record> queue = (Queue<Record>)obj;
            while (true)
            {
                sem.WaitOne();
                if (queue.Count != 0)
                {
                    Record record = queue.Dequeue();
                    Console.WriteLine(readerThread.Name + " remove " + record);
                }
               sem.Release();
                Thread.Sleep(1000);
            }
        }
    }
}
