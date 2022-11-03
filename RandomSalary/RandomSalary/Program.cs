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
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ReadAndUpdate();
            sw.Stop();
            Console.WriteLine("Binary Update: {0} ms", sw.ElapsedMilliseconds);

        }

        private static void ReadAndUpdate()
        {
            List<int> Salaries = new List<int>();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var bytes = File.ReadAllBytes("RandomSalary.bin");
            sw.Stop();
            for (int i = 0; i < bytes.Length / 4; i++)//No specific number
            {
                Salaries.Add(BitConverter.ToInt32(bytes, i*4));
            }
            BinaryWriter bw = new BinaryWriter(File.Open("UpdatedRandomSalary.bin", FileMode.Create));
            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            foreach (var salary in Salaries)
            {
                bw.Write(BitConverter.GetBytes((int)(salary * 1.2)));
            }
            sw2.Stop();

            Console.WriteLine("Binary Read: {0} ms", sw.ElapsedMilliseconds);
            Console.WriteLine("Binary Write&Covnert: {0} ms", sw2.ElapsedMilliseconds);
            Console.WriteLine("Numbers in file: {0}", bytes.Length/4);

        }

        public static void GenerateRandomSalary()
        {
            List<Byte[]> Salaries = new List<Byte[]>();
            Random rnd = new Random();
            for (int i = 0; i < 1000000; i++)
            {
                Salaries.Add(BitConverter.GetBytes(rnd.Next(200000, 550000)));
            }


            BinaryWriter bw = new BinaryWriter(File.Open("RandomSalary.bin", FileMode.Create));
            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach (Byte[] values in Salaries)
            {
                bw.Write(values);
            }
            sw.Stop();
            bw.Close();

            Console.WriteLine("Binary Write: {0} ms", sw.ElapsedMilliseconds);
        }
    }
}
