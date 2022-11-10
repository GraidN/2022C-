using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace RandomSalary
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateRandomSalary();
            Stopwatch sw = Stopwatch.StartNew();
            var Start = DateTime.Now;
            ReadAndUpdate();
            sw.Stop();
            TimeSpan total = DateTime.Now - Start;
            Console.WriteLine("Binary Update: {0} ms", sw.ElapsedMilliseconds);
            Console.WriteLine("Binary Update: {0} ms (DateTime)", total.TotalMilliseconds);


        }

        private static void ReadAndUpdate()
        {
            List<int> Salaries = new List<int>();
            //Start measuring
            Stopwatch sw = Stopwatch.StartNew();
            var start1 = DateTime.Now;

            var bytes = File.ReadAllBytes("RandomSalary.bin"); //Openingfile and reading bytes
            sw.Stop();
            TimeSpan total1 = DateTime.Now - start1;
            for (int i = 0; i < bytes.Length / 4; i++)//No specific number
            {
                Salaries.Add(BitConverter.ToInt32(bytes, i * 4));//Converting back to Int
            }

            BinaryWriter bw = new BinaryWriter(File.Open("UpdatedRandomSalary.bin", FileMode.Create));//Opening file stream

            //Start measuring
            Stopwatch sw2 = Stopwatch.StartNew();
            var start2 = DateTime.Now;

            foreach (var salary in Salaries)
            {
                bw.Write(BitConverter.GetBytes((int)(salary * 1.2))); //Converting then write
            }

            sw2.Stop();
            TimeSpan total2 = DateTime.Now-start2;

            Console.WriteLine("Binary Read: {0} ms", sw.ElapsedMilliseconds);
            Console.WriteLine("Binary Read: {0} ms (DateTIme)", total1.TotalMilliseconds);
            Console.WriteLine("Binary Write&Covnert: {0} ms", sw2.ElapsedMilliseconds);
            Console.WriteLine("Binary Write&Covnert: {0} ms (DateTime)", total2.TotalMilliseconds);
            Console.WriteLine("Numbers in file: {0}", bytes.Length / 4);

        }

        public static void GenerateRandomSalary()
        {
            List<Byte[]> Salaries = new List<Byte[]>();
            Random rnd = new Random();

            for (int i = 0; i < 1000000; i++)
            {
                Salaries.Add(BitConverter.GetBytes(rnd.Next(200000, 550000)));//Populating list
            }

            BinaryWriter bw = new BinaryWriter(File.Open("RandomSalary.bin", FileMode.Create));//Opening a filestream
            //Start measuring
            Stopwatch sw = Stopwatch.StartNew();
            var Start = DateTime.Now;

            foreach (Byte[] values in Salaries)
            {
                bw.Write(values);//Write to file per salary
            }

            sw.Stop();
            TimeSpan total = DateTime.Now - Start;
            bw.Close();
            Console.WriteLine("Binary Write: {0} ms", sw.ElapsedMilliseconds);
            Console.WriteLine("Binary Write: {0} ms (DateTime)", total.TotalMilliseconds);

        }
    }
}