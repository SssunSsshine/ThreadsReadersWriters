using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace L5._221ReadersWriters
{
    class Writer
    {
        const int MAX_COUNT = 1;
        const int INIT_COUNT = 1;
        static Semaphore sem = new Semaphore(INIT_COUNT, MAX_COUNT);
        Thread writerThread;
        static int countRecord = 0;
        public Writer(int numWriter, Queue<Record> queue)
        {
            writerThread = new Thread(Write);
            writerThread.Name = $"Writer {numWriter}";
            writerThread.Start(queue);
        }

        public void Write(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Queue<Record>))
                return;
            Queue < Record > queue = (Queue<Record>)obj;
            while (true)
            {
                sem.WaitOne();
                Console.WriteLine(writerThread.Name + " Begin writing");
                countRecord++;
                Record record = new Record(writerThread.Name, countRecord.ToString());
                queue.Enqueue(record);
                Thread.Sleep(1000);
                Console.WriteLine(writerThread.Name + "End writing");
                sem.Release();
            }
        }
    }
}
