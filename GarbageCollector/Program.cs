using System;

namespace GarbageCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            int max = 0;
            while(true)
            {
                disposeable dp = new disposeable();
                if (max < dp.get())
                {
                    max = dp.get();
                }
                else
                {
                    break;
                }
                Console.WriteLine(dp.get());
            }
            Console.WriteLine("Max number of instances: {0}", max);
        }
    }
    class disposeable
    {
        static int counter; 
        public disposeable(){
            add();
        }
        void add()
        {
            counter++;
        }
        public int get()
        {
            return counter;
        }
        ~disposeable()
        {
            counter--;
        }
    }
}
