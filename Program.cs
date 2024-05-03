using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Sortirovki_C__Valirakhmanov
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] lenghts = { 50, 500 ,5000, 50000, 500000 };
            int i = 4;
            List<List<long>> TimeList = new List<List<long>>();
            for (int j = 0; j < 3; j++)
                TimeList.Add(new List<long>());
            SortirivkiTimeRandom(TimeList, lenghts[i]);
            SortirivkiTimePartiallyOrdered(TimeList, lenghts[i]);
            SortirivkiTimeRepeatedData(TimeList, lenghts[i]);
            for (int j = 0; j < TimeList.Count(); j++)
            {
                Console.WriteLine("Тип под номером: " + j);
                foreach (long time in TimeList[j])
                    Console.WriteLine(time);
            }
        } 
        public static void PrintMasiv<T>(T[] masiv) where T : IComparable
        {
            if (typeof(T) == typeof(Date))
                for (int i = 0; i < masiv.Length; i++)
                {
                    Date.WriteDate((Date)(object)masiv[i]);
                    Console.Write(" ");
                }
            else
                foreach (T i in masiv) { Console.Write(i + " "); }
            Console.WriteLine() ;
        }
        public static void SortirovkiTime(
            int[] masivInt, byte[] masivByte,
            string[] masivString, Date[] masivDate,
            Stopwatch swInt, Stopwatch swByte, Stopwatch swString, Stopwatch swDate)
        {
            //Console.WriteLine("0%");
            swInt.Start();
            masivInt = Sortirovka<int>.Porazridnaya(masivInt);
            //Array.Sort<int>(masivInt);
            swInt.Stop();
            //Console.WriteLine("25%");
            swByte.Start();
            masivByte = Sortirovka<byte>.Porazridnaya(masivByte);
            //Array.Sort<byte>(masivByte
            swByte.Stop();
            //Console.WriteLine("50%");
            swString.Start();
            masivString = Sortirovka<string>.Porazridnaya(masivString);
            //Array.Sort<string>(masivString);
            swString.Stop();
            //Console.WriteLine("75%");
            swDate.Start();
            masivDate = Sortirovka<Date>.Porazridnaya(masivDate);
            //Array.Sort<Date>(masivDate);
            swDate.Stop();
            //Console.WriteLine("100%");
        }
        public static void SortirovkiTimeP(
        int[] masivInt, byte[] masivByte,
        string[] masivString, Date[] masivDate,
        Stopwatch swInt, Stopwatch swByte, Stopwatch swString, Stopwatch swDate)
        {
            Task taskInt = Task.Run(() =>
            {
                swInt.Start();
                masivInt = Sortirovka<int>.Vstavki(masivInt);
                swInt.Stop();
                Console.WriteLine("Int");
            });

            Task taskByte = Task.Run(() =>
            {
                swByte.Start();
                masivByte = Sortirovka<byte>.Vstavki(masivByte);
                swByte.Stop();
                Console.WriteLine("Byte");
            });

            Task taskString = Task.Run(() =>
            {
                swString.Start();
                masivString = Sortirovka<string>.Vstavki(masivString);
                swString.Stop();
                Console.WriteLine("String");
            });

            Task taskDate = Task.Run(() =>
            {
                swDate.Start();
                masivDate = Sortirovka<Date>.Vstavki(masivDate);
                swDate.Stop();
                Console.WriteLine("Date");
            });

            Task.WaitAll(taskInt, taskByte, taskString, taskDate);
        }
        public static void SortirivkiTimeRandom(List<List<long>> TimeList, int size)
        {
            Stopwatch swInt = new Stopwatch();
            Stopwatch swByte = new Stopwatch();
            Stopwatch swString = new Stopwatch();
            Stopwatch swDate = new Stopwatch();
            if (size <= 500)
            {
                for (int i = 0; i < 1000; i++)
                {
                    int[] masivInt = new int[size];
                    masivInt = DataGenerator<int>.RandomData(masivInt.Length, masivInt);
                    byte[] masivByte = new byte[size];
                    masivByte = DataGenerator<byte>.RandomData(masivByte.Length, masivByte);
                    string[] masivString = new string[size];
                    masivString = DataGenerator<string>.RandomData(masivString.Length, masivString);
                    Date[] masivDate = new Date[size];
                    masivDate = DataGenerator<Date>.RandomData(masivDate.Length, masivDate);
                    SortirovkiTime(masivInt, masivByte, masivString, masivDate
                    , swInt, swByte, swString, swDate);
                }
            }
            else if (size <= 5000) 
            {
                for (int i = 0; i < 10; i++)
                {
                    int[] masivInt = new int[size];
                    masivInt = DataGenerator<int>.RandomData(masivInt.Length, masivInt);
                    byte[] masivByte = new byte[size];
                    masivByte = DataGenerator<byte>.RandomData(masivByte.Length, masivByte);
                    string[] masivString = new string[size];
                    masivString = DataGenerator<string>.RandomData(masivString.Length, masivString);
                    Date[] masivDate = new Date[size];
                    masivDate = DataGenerator<Date>.RandomData(masivDate.Length, masivDate);
                    SortirovkiTime(masivInt, masivByte, masivString, masivDate
                    , swInt, swByte, swString, swDate);
                }
            }
            else
            {
                int[] masivInt = new int[size];
                masivInt = DataGenerator<int>.RandomData(masivInt.Length, masivInt);
                byte[] masivByte = new byte[size];
                masivByte = DataGenerator<byte>.RandomData(masivByte.Length, masivByte);
                string[] masivString = new string[size];
                masivString = DataGenerator<string>.RandomData(masivString.Length, masivString);
                Date[] masivDate = new Date[size];
                masivDate = DataGenerator<Date>.RandomData(masivDate.Length, masivDate);
                SortirovkiTime(masivInt, masivByte, masivString, masivDate
                , swInt, swByte, swString, swDate);
            }
            Console.WriteLine("Для длины массива случайных чисел: " + size);
            Console.WriteLine(swInt.ElapsedMilliseconds + " Тип Int");
            Console.WriteLine(swByte.ElapsedMilliseconds + " Тип Byte");
            Console.WriteLine(swString.ElapsedMilliseconds + " Тип String");
            Console.WriteLine(swDate.ElapsedMilliseconds + " Тип Date");
            TimeList[0].Add(swInt.ElapsedMilliseconds);
            TimeList[0].Add(swByte.ElapsedMilliseconds);
            TimeList[0].Add(swString.ElapsedMilliseconds);
            TimeList[0].Add(swDate.ElapsedMilliseconds);
        }
        public static void SortirivkiTimePartiallyOrdered(List<List<long>> TimeList, int size)
        {
            Stopwatch swInt = new Stopwatch();
            Stopwatch swByte = new Stopwatch();
            Stopwatch swString = new Stopwatch();
            Stopwatch swDate = new Stopwatch();
            if (size <= 500)
            {
                for (int i = 0; i < 1000; i++)
                {
                    int[] masivInt = new int[size];
                    masivInt = DataGenerator<int>.PartiallyOrdered(masivInt.Length, masivInt);
                    byte[] masivByte = new byte[size];
                    masivByte = DataGenerator<byte>.PartiallyOrdered(masivByte.Length, masivByte);
                    string[] masivString = new string[size];
                    masivString = DataGenerator<string>.PartiallyOrdered(masivString.Length, masivString);
                    Date[] masivDate = new Date[size];
                    masivDate = DataGenerator<Date>.PartiallyOrdered(masivDate.Length, masivDate);
                    SortirovkiTime(masivInt, masivByte, masivString, masivDate
                    , swInt, swByte, swString, swDate);
                }
            }
            else if (size <= 5000)
            {
                for (int i = 0; i < 10; i++)
                {
                    int[] masivInt = new int[size];
                    masivInt = DataGenerator<int>.PartiallyOrdered(masivInt.Length, masivInt);
                    byte[] masivByte = new byte[size];
                    masivByte = DataGenerator<byte>.PartiallyOrdered(masivByte.Length, masivByte);
                    string[] masivString = new string[size];
                    masivString = DataGenerator<string>.PartiallyOrdered(masivString.Length, masivString);
                    Date[] masivDate = new Date[size];
                    masivDate = DataGenerator<Date>.PartiallyOrdered(masivDate.Length, masivDate);
                    SortirovkiTime(masivInt, masivByte, masivString, masivDate
                    , swInt, swByte, swString, swDate);
                }
            }
            else
            {
                int[] masivInt = new int[size];
                masivInt = DataGenerator<int>.PartiallyOrdered(masivInt.Length, masivInt);
                byte[] masivByte = new byte[size];
                masivByte = DataGenerator<byte>.PartiallyOrdered(masivByte.Length, masivByte);
                string[] masivString = new string[size];
                masivString = DataGenerator<string>.PartiallyOrdered(masivString.Length, masivString);
                Date[] masivDate = new Date[size];
                masivDate = DataGenerator<Date>.PartiallyOrdered(masivDate.Length, masivDate);
                SortirovkiTime(masivInt, masivByte, masivString, masivDate
                , swInt, swByte, swString, swDate);
            }
            Console.WriteLine("Для длины массива частично отсортированного: " + size);
            Console.WriteLine(swInt.ElapsedMilliseconds + " Тип Int");
            Console.WriteLine(swByte.ElapsedMilliseconds + " Тип Byte");
            Console.WriteLine(swString.ElapsedMilliseconds + " Тип String");
            Console.WriteLine(swDate.ElapsedMilliseconds + " Тип Date");
            TimeList[1].Add(swInt.ElapsedMilliseconds);
            TimeList[1].Add(swByte.ElapsedMilliseconds);
            TimeList[1].Add(swString.ElapsedMilliseconds);
            TimeList[1].Add(swDate.ElapsedMilliseconds);
        }
        public static void SortirivkiTimeRepeatedData(List<List<long>> TimeList, int size)
        {
            Stopwatch swInt = new Stopwatch();
            Stopwatch swByte = new Stopwatch();
            Stopwatch swString = new Stopwatch();
            Stopwatch swDate = new Stopwatch();
            if (size <= 500)
            {
                for (int i = 0; i < 1000; i++)
                {
                    int[] masivInt = new int[size];
                    masivInt = DataGenerator<int>.RepeatedData(masivInt.Length, masivInt);
                    byte[] masivByte = new byte[size];
                    masivByte = DataGenerator<byte>.RepeatedData(masivByte.Length, masivByte);
                    string[] masivString = new string[size];
                    masivString = DataGenerator<string>.RepeatedData(masivString.Length, masivString);
                    Date[] masivDate = new Date[size];
                    masivDate = DataGenerator<Date>.RepeatedData(masivDate.Length, masivDate);
                    SortirovkiTime(masivInt, masivByte, masivString, masivDate
                    , swInt, swByte, swString, swDate);
                }
            }
            else if (size <= 5000)
            {
                for (int i = 0; i < 10; i++)
                {
                    int[] masivInt = new int[size];
                    masivInt = DataGenerator<int>.RepeatedData(masivInt.Length, masivInt);
                    byte[] masivByte = new byte[size];
                    masivByte = DataGenerator<byte>.RepeatedData(masivByte.Length, masivByte);
                    string[] masivString = new string[size];
                    masivString = DataGenerator<string>.RepeatedData(masivString.Length, masivString);
                    Date[] masivDate = new Date[size];
                    masivDate = DataGenerator<Date>.RepeatedData(masivDate.Length, masivDate);
                    SortirovkiTime(masivInt, masivByte, masivString, masivDate
                    , swInt, swByte, swString, swDate);
                }
            }
            else
            {
                int[] masivInt = new int[size];
                masivInt = DataGenerator<int>.RepeatedData(masivInt.Length, masivInt);
                byte[] masivByte = new byte[size];
                masivByte = DataGenerator<byte>.RepeatedData(masivByte.Length, masivByte);
                string[] masivString = new string[size];
                masivString = DataGenerator<string>.RepeatedData(masivString.Length, masivString);
                Date[] masivDate = new Date[size];
                masivDate = DataGenerator<Date>.RepeatedData(masivDate.Length, masivDate);
                SortirovkiTime(masivInt, masivByte, masivString, masivDate
                , swInt, swByte, swString, swDate);
            }
            Console.WriteLine("Для длины массива с большим количеством одинаковых элементов: " + size);
            Console.WriteLine(swInt.ElapsedMilliseconds + " Тип Int");
            Console.WriteLine(swByte.ElapsedMilliseconds + " Тип Byte");
            Console.WriteLine(swString.ElapsedMilliseconds + " Тип String");
            Console.WriteLine(swDate.ElapsedMilliseconds + " Тип Date");
            TimeList[2].Add(swInt.ElapsedMilliseconds);
            TimeList[2].Add(swByte.ElapsedMilliseconds);
            TimeList[2].Add(swString.ElapsedMilliseconds);
            TimeList[2].Add(swDate.ElapsedMilliseconds);
        }
    }
}