using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/*Использование семафоров и очереди 
 5.1.	Создать приложение с двумя дополнительными потоками писателей и двумя потоками читателей. 
Писатели в случайные моменты времени помещают записи, содержащие номер потока писателя и номер записи в СХД, 
читатели в случайные моменты времени удаляют записи, делая об этом сообщения. */

namespace L5._221ReadersWriters
{
    class Program
    {
        class Data
        {
            static public Queue<Record> queue = new Queue<Record>();
            public string threadName;
            public Data(string threadName)
            {
                this.threadName = threadName;
            }
            static public int countRecord = 0;
        }

        
        const int MAX_COUNT = 1;
        const int INIT_COUNT = 1;
        static Semaphore sem = new Semaphore(INIT_COUNT, MAX_COUNT);
        static public void Write(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Data))
                return;
            Data data = (Data)obj;
            string threadName = data.threadName;
            while (true)
            {
                sem.WaitOne();
                Console.WriteLine(threadName + " Begin writing");
                Data.countRecord++;
                Record record = new Record(threadName, Data.countRecord.ToString());
                Data.queue.Enqueue(record);
                Thread.Sleep(1000);
                Console.WriteLine(threadName + "End writing");
                sem.Release();
            }
        }
        static public void Read(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Data))
                return;
            Data data = (Data)obj;
            string threadName = data.threadName;
            while (true)
            {
                sem.WaitOne();
                if (Data.queue.Count != 0)
                {
                    Record record = Data.queue.Dequeue();
                    /*foreach(Record record2 in Data.queue)
                    {
                        int n = Int32.Parse(record2.numRecord);
                        n--;
                        record2.numRecord = n.ToString();
                    }
                    Data.countRecord--;*/
                    Console.WriteLine(threadName + " remove " + record);
                }
                sem.Release();
                Thread.Sleep(1000);
            }
        }
        static void Main(string[] args)
        {
            /*Queue<Record> queue = new Queue<Record>();
            Writer writer1 = new Writer(1, queue);
            Writer writer2 = new Writer(2, queue);
            Writer writer3 = new Writer(3, queue);
            Writer writer4 = new Writer(4, queue);
            Reader reader1 = new Reader(1, queue);
            Reader reader2 = new Reader(2, queue);*/
            Data data1 = new Data("Writer 1");
            Data data2 = new Data("Writer 2");
            Data data3 = new Data("Reader 1");
            Data data4 = new Data("Reader 2");
           /* Data data5 = new Data("Writer 3");
            Data data6 = new Data("Writer 4");
            Data data7 = new Data("Reader 3");
            Data data8 = new Data("Reader 4");*/
            Thread writer1 = new Thread(Write);
            Thread writer2 = new Thread(Write);
            
            Thread reader1 = new Thread(Read);
            Thread reader2 = new Thread(Read);
            /*Thread writer3 = new Thread(Write);
            Thread writer4 = new Thread(Write);

            Thread reader3 = new Thread(Read);
            Thread reader4 = new Thread(Read);*/
            writer1.Start(data1);
            writer2.Start(data2);
            reader1.Start(data3);
            reader2.Start(data4);
            /*writer3.Start(data5);
            writer4.Start(data6);
            reader3.Start(data7);
            reader4.Start(data8);*/
            Console.ReadLine();
        }
    }
}
