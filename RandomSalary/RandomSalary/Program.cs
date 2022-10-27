using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace RandomSalary
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateRandomSalary();
           // ReadAndUpdate();
        }

        private static void ReadAndUpdate()
        {
            List<int> Salaries = new List<int>();
           var bytes = File.ReadAllBytes("RandomSalary.bin");
            for (int i = 0; i < 100000; i++)
            {
                //Salaries.Add(BinaryReader.ToInt32(bytes,0));
            }
            Console.WriteLine(Salaries[0]);
        }

        public static void GenerateRandomSalary()
        {
            List<int> Salaries = new List<int>();
            Random rnd = new Random();
            for (int i = 0; i < 100000; i++)
            {
                Salaries.Add(rnd.Next(200000, 5500000));
                using (var stream = File.Open("RandomSalary.bin", FileMode.Create))
                {
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                    {
                        writer.Write(Salaries[i]);
                    }
                }
            }


            DateTime Startime = DateTime.Now;
            Console.WriteLine(Salaries[0]);
            Console.WriteLine("Binary Write: {0} ms", DateTime.Now.Millisecond - Startime.Millisecond);
        }
    }
}
