using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Sortirovki_C__Valirakhmanov
{
    internal class DataGenerator<T> where T : IComparable
    {
        public static string[] RandomStrings(int size)
        {
            string[] randomStrings = new string[size];
            Random random = new Random();

            for (int i = 0; i < size; i++)
            {
                int length = random.Next(5, 10);
                randomStrings[i] = GenerateRandomString(length);
            }

            return randomStrings;
        }

        static string GenerateRandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            Random random = new Random();
            char[] randomString = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomString[i] = chars[random.Next(chars.Length)];
            }

            return new string(randomString);
        }
        public static Date GenerateRandomDate()
        {
            Date date = new(
                Random.Shared.Next(1, 29),
                Random.Shared.Next(1, 12),
                Random.Shared.Next(0, 10));
            return date;
        }
        public static T[] PartiallyOrdered(int size, T[] masiv)
        {
            for (int i = 0; i < size; i++)
                masiv[i] = RandomDataElement(masiv[i]);
            Array.Sort(masiv);
            int NumberRandomInserts = size / 10 + 1;
            List<int> ArrayRandomIndex = new List<int>();
            for (int i = 0; i < NumberRandomInserts; i++)
            {
                int randomIndex = Random.Shared.Next(0, size - 1);
                while(ArrayRandomIndex.Contains(randomIndex))
                    randomIndex = Random.Shared.Next(0,size - 1);
                ArrayRandomIndex.Add(randomIndex);
                masiv[randomIndex] = RandomDataElement(masiv[randomIndex]);
            }
            return masiv;
        }
        public static T[] RepeatedData(int size, T[] masiv)
        {
            T repeatedElement = default;
            repeatedElement = RandomDataElement(repeatedElement);
            for (int i = 0; i < size; i++)
                masiv[i] = repeatedElement;
            int NumberRandomInserts = size / 10;
            List<int> ArrayRandomIndex = new List<int>();
            for (int i = 0; i < NumberRandomInserts; i++)
            {
                int randomIndex = Random.Shared.Next(0, size - 1);
                while (ArrayRandomIndex.Contains(randomIndex))
                    randomIndex = Random.Shared.Next(0, size - 1);
                ArrayRandomIndex.Add(randomIndex);
                masiv[randomIndex] = RandomDataElement(masiv[randomIndex]);
            }
            return masiv;
        }
        public static T[] RandomData(int size, T[] masiv)
        {
            for (int i = 0; i < size; i++)
                masiv[i] = RandomDataElement(masiv[i]);
            return masiv;
        }
        private static T RandomDataElement(T date)
        {
            if (typeof(T) == typeof(int))
            {
                date = (T)Convert.ChangeType(Random.Shared.Next(100), typeof(T));
            }
            else if (typeof(T) == typeof(string))
            {
                int length = Random.Shared.Next(5, 10);
                date = (T)Convert.ChangeType(GenerateRandomString(length), typeof(T));
            }
            else if (typeof(T) == typeof(byte))
            {
                date = (T)Convert.ChangeType(GenerateRandomByte(), typeof(T));
            }
            else if (typeof(T) == typeof(Date))
            {
                date = (T)Convert.ChangeType(GenerateRandomDate(), typeof(T));
            }

            return date;
        }
        public static byte GenerateRandomByte()
        {
            byte[] buffer = new byte[1];
            Random.Shared.NextBytes(buffer);
            return (byte)(buffer[0] % 10);
        }
    }
}
