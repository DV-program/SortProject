using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Sortirovki_C__Valirakhmanov
{
    internal static class Sortirovka<T> where T : IComparable
    {
        // Вспомогательные функции
        static void swap(ref T a, ref T b)
        {
            T c;
            c = a;
            a = b;
            b = c;
        }
        static T[] Merge(T[] masiv1, T[] masiv2)
        {
            T[] masiv = new T[masiv1.Length + masiv2.Length];
            int current = 0;
            int i = 0;
            int j = 0;
            while (i < masiv1.Length && j < masiv2.Length)
            {
                if (masiv1[i].CompareTo(masiv2[j]) < 0)
                {
                    masiv[current] = masiv1[i];
                    i++;
                    current++;
                }
                else
                {
                    masiv[current] = masiv2[j];
                    j++;
                    current++;
                }
            }
            while (i < masiv1.Length)
            {
                masiv[current] = masiv1[i];
                i++;
                current++;
            }
            while (j < masiv2.Length)
            {
                masiv[current] = masiv2[j];
                j++;
                current++;
            }
            return masiv;
        }
        static void Merge(T[] masiv, int lowIndex, int middleIndex, int highIndex)
        {
            int left = lowIndex;
            int right = middleIndex + 1;
            T[] tempMasiv = new T[highIndex - lowIndex + 1];
            int index = 0;

            while ((left <= middleIndex) && (right <= highIndex))
            {
                if (masiv[left].CompareTo(masiv[right]) < 0)
                {
                    tempMasiv[index] = masiv[left];
                    left++;
                }
                else
                {
                    tempMasiv[index] = masiv[right];
                    right++;
                }

                index++;
            }

            for (int i = left; i <= middleIndex; i++)
            {
                tempMasiv[index] = masiv[i];
                index++;
            }

            for (int i = right; i <= highIndex; i++)
            {
                tempMasiv[index] = masiv[i];
                index++;
            }

            for (int i = 0; i < tempMasiv.Length; i++)
            {
                masiv[lowIndex + i] = tempMasiv[i];
            }
        }
        static int Min(T[] masiv)
        {
            T min = masiv[0];
            int j = 0;
            for (int i = 0; i < masiv.Length; i++)
            {
                if (min.CompareTo(masiv[i]) > 0)
                {
                    min = masiv[i];
                    j = i;
                }
            }
            return j;
        }
        static int Max(T[] masiv)
        {
            T max = masiv[0];
            int j = 0;
            for (int i = 0; i < masiv.Length; i++)
            {
                if (max.CompareTo(masiv[i]) < 0)
                {
                    max = masiv[i];
                    j = i;
                }
            }
            return j;
        }
        static int Max(T[] masiv, int left, int right)
        {
            T max = masiv[left];
            int j = left;
            for (int i = left; i <= right; i++)
            {
                if (max.CompareTo(masiv[i]) < 0)
                {
                    max = masiv[i];
                    j = i;
                }
            }
            return j;
        }
        static T Max(T a, T b)
        {
            return a.CompareTo(b) > 0 ? a : b;
        }
        static T Min(T a, T b)
        {
            return a.CompareTo(b) < 0 ? a : b;
        }
        // Простые сортировки   
        public static T[] Puzirek(T[] masiv)
        {
            for (int i = 0; i < masiv.Length - 1; i++)
            {
                for (int j = 0; j < masiv.Length - 1; j++)
                {
                    if (masiv[j].CompareTo(masiv[j + 1]) > 0) { swap(ref masiv[j], ref masiv[j + 1]); }
                }
            }
            return masiv;
        }
        public static T[] Vstavki(T[] masiv)
        {
            for (int i = 1; i < masiv.Length; i++)
            {
                for (int j = i; j >= 1; j--)
                {
                    if (masiv[j].CompareTo((masiv[j - 1])) < 0) { swap(ref masiv[j], ref masiv[j - 1]); }
                }
            }
            return masiv;
        }
        public static T[] Viborom(T[] masiv)
        {
            for (int i = 0; i < masiv.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < masiv.Length; j++)
                {
                    if (masiv[j].CompareTo(masiv[min]) < 0)
                    {
                        min = j;
                    }
                }
                swap(ref masiv[i], ref masiv[min]);

            }
            return masiv;
        }
        // Сортировки Шелла с различным шагом
        public static T[] Shell_d2(T[] masiv)
        {
            int d = masiv.Length / 2;
            while (d > 0)
            {
                for (int i = d; i < masiv.Length; i++)
                {
                    int j = i;
                    while (j >= d && masiv[j].CompareTo(masiv[j - d]) < 0)
                    {
                        swap(ref masiv[j], ref masiv[j - d]);
                        j -= d;
                    }
                }
                d /= 2;
            }
            return masiv;
        }
        public static T[] ShellMarcinCiura(T[] masiv)
        {
            int[] d = new int[] { 1, 4, 10, 23, 57, 132, 301, 701, 1750 };
            int current = d.Length - 1;
            while (current >= 0)
            {
                for (int i = d[current]; i < masiv.Length; i++)
                {
                    int j = i;
                    while (j >= d[current] && masiv[j - d[current]].CompareTo(masiv[j]) > 0)
                    {
                        swap(ref masiv[j], ref masiv[j - d[current]]);
                        j -= d[current];
                    }
                }
                current -= 1;
            }
            return masiv;
        }

        // Сортировка Слиянием
        public static T[] Sliyaniem(T[] masiv)
        {
            return Sliyaniem(masiv, 0, masiv.Length - 1);
        }
        static T[] Sliyaniem(T[] masiv, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                int middleIndex = (lowIndex + highIndex) / 2;
                Sliyaniem(masiv, lowIndex, middleIndex);
                Sliyaniem(masiv, middleIndex + 1, highIndex);
                Merge(masiv, lowIndex, middleIndex, highIndex);
            }

            return masiv;
        }
        // Быстрая сортировка рекусрией
        public static T[] Bistraya(T[] masiv)
        {
            if (masiv == null || masiv.Length == 0)
                return masiv;
            Bistraya(masiv, 0, masiv.Length - 1);
            return masiv;
        }
        static void Bistraya(T[] masiv, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = FindPivot(masiv, low, high);
                T temp = masiv[pivotIndex];
                masiv[pivotIndex] = masiv[high];
                masiv[high] = temp;
                int pivot = Partition(masiv, low, high);
                Bistraya(masiv, low, pivot - 1);
                Bistraya(masiv, pivot + 1, high);
            }
        }

        static int Partition(T[] masiv, int low, int high)
        {
            T pivot = masiv[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (masiv[j].CompareTo(pivot) <= 0)
                {
                    i++;
                    T temp = masiv[i];
                    masiv[i] = masiv[j];
                    masiv[j] = temp;
                }
            }

            T temp2 = masiv[i + 1];
            masiv[i + 1] = masiv[high];
            masiv[high] = temp2;

            return i + 1;
        }
        static int FindPivot(T[] masiv, int low, int high)
        {
            int mid = (low + high) / 2;
            if (masiv[low].CompareTo(masiv[mid]) <= 0 && masiv[mid].CompareTo(masiv[high]) <= 0)
                return mid;
            else if (masiv[low].CompareTo(masiv[high]) <= 0 && masiv[high].CompareTo(masiv[mid]) <= 0)
                return high;
            else
                return low;
        }
        // Быстрая сортировка без рекурсии
        public static T[] BistrayaStack(T[] array)
        {
            if (array == null || array.Length == 0)
                return array;

            Stack<int> stack = new Stack<int>();
            stack.Push(0);
            stack.Push(array.Length - 1);

            while (stack.Count > 0)
            {
                int end = stack.Pop();
                int start = stack.Pop();

                if (start >= end)
                    continue;

                int pivotIndex = PartitionStack(array, start, end);

                if (pivotIndex - 1 > start)
                {
                    stack.Push(start);
                    stack.Push(pivotIndex - 1);
                }

                if (pivotIndex + 1 < end)
                {
                    stack.Push(pivotIndex + 1);
                    stack.Push(end);
                }
            }
            return array;
        }
        private static int PartitionStack(T[] array, int start, int end)
        {
            T pivotValue = array[end];
            int i = start - 1;

            for (int j = start; j < end; j++)
            {
                if (array[j].CompareTo(pivotValue) <= 0)
                {
                    i++;
                    swap(ref array[i], ref array[j]);
                }
            }

            swap(ref array[i+1], ref array[end]);
            return i + 1;
        }
        // Пирамидальная сортировка
        public static T[] Kuchey(T[] masiv)
        {
            if (masiv == null || masiv.Length == 0)
                return masiv;
            PostroitDerevo(masiv);
            for (int i = masiv.Length - 1; i > 0; i--)
            {
                swap(ref masiv[0], ref masiv[i]);
                PereuporidochitDerevo(masiv, 0, i - 1);
            }
            return masiv;
        }
        static void PostroitDerevo(T[] masiv)
        {
            for (int i = masiv.Length / 2 - 1; i >= 0; i--)
                PereuporidochitDerevo(masiv, i, masiv.Length - 1);
            return;
        }
        static void PereuporidochitDerevo(T[] masiv, int i, int last)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left <= last && masiv[i].CompareTo(masiv[left]) < 0)
            {
                largest = left;
            }

            if (right <= last && masiv[largest].CompareTo(masiv[right]) < 0)
            {
                largest = right;
            }

            if (largest != i)
            {
                swap(ref masiv[i], ref masiv[largest]);
                PereuporidochitDerevo(masiv, largest, last);
            }
        }
        // Поразрядная сортировка
        // Для типа byte
        static void CountingSort(byte[] masiv, int exp)
        {
            int n = masiv.Length;
            byte[] output = new byte[n];
            int[] count = new int[10];

            for (int i = 0; i < n; i++)
            {
                count[(masiv[i] / exp) % 10]++;
            }

            for (int i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }

            for (int i = n - 1; i >= 0; i--)
            {
                output[count[(masiv[i] / exp) % 10] - 1] = masiv[i];
                count[(masiv[i] / exp) % 10]--;
            }

            for (int i = 0; i < n; i++)
            {
                masiv[i] = output[i];
            }
        }

        public static byte[] Porazridnaya(byte[] masiv)
        {
            byte max = masiv[0];
            for (int i = 1; i < masiv.Length; i++)
            {
                if (masiv[i] > max)
                {
                    max = masiv[i];
                }
            }

            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                CountingSort(masiv, exp);
            }
            return masiv;
        }
        // Для типа int
        static void CountingSort(int[] masiv, int exp)
        {
            int n = masiv.Length;
            int[] output = new int[n];
            int[] count = new int[10];

            for (int i = 0; i < n; i++)
            {
                count[(masiv[i] / exp) % 10]++;
            }

            for (int i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }

            for (int i = n - 1; i >= 0; i--)
            {
                output[count[(masiv[i] / exp) % 10] - 1] = masiv[i];
                count[(masiv[i] / exp) % 10]--;
            }

            for (int i = 0; i < n; i++)
            {
                masiv[i] = output[i];
            }
        }

        public static int[] Porazridnaya(int[] masiv)
        {
            int max = masiv[0];
            for (int i = 1; i < masiv.Length; i++)
            {
                if (masiv[i] > max)
                {
                    max = masiv[i];
                }
            }

            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                CountingSort(masiv, exp);
            }
            return masiv;
        }
        // Для типа string
        static void CountingSort(string[] masiv, int exp)
        {
            int n = masiv.Length;
            string[] output = new string[n];
            int[] count = new int[256]; 

            for (int i = 0; i < n; i++)
            {
                int index = masiv[i].Length < exp ? 0 : masiv[i][masiv[i].Length - exp];
                count[index]++;
            }

            for (int i = 1; i < 256; i++)
            {
                count[i] += count[i - 1];
            }

            for (int i = n - 1; i >= 0; i--)
            {
                int index = masiv[i].Length < exp ? 0 : masiv[i][masiv[i].Length - exp];
                output[count[index] - 1] = masiv[i];
                count[index]--;
            }

            for (int i = 0; i < n; i++)
            {
                masiv[i] = output[i];
            }
        }

        public static string[] Porazridnaya(string[] masiv)
        {
            int maxLen = masiv.Max(s => s.Length);

            for (int exp = 1; exp <= maxLen; exp++)
            {
                CountingSort(masiv, exp);
            }

            return masiv;
        }
        // Для типа Date
        public static Date[] Porazridnaya(Date[] masiv)
        {
            if (masiv == null || masiv.Length == 0)
            {
                return masiv;
            }
            int maxDay = 31;
            int maxMonth = 12;
            int max = GetMaxYear(masiv);
            int masivLenght = masiv.Length;
            Date[] output = new Date[masiv.Length];
            for (int exp = 1; maxDay / exp > 0; exp *= 10)
            {
                CountSortDay(masiv, output,exp, masivLenght);
            }
            for (int exp = 1; maxMonth / exp > 0; exp *= 10)
            {
                CountSortMonth(masiv,output, exp, masivLenght);
            }
            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                CountSortYear(masiv,output , exp);
            }
            return masiv;
        }

        private static int GetMaxYear(Date[] masiv)
        {
            int max = masiv[0].Year;
            for (int i = 1; i < masiv.Length; i++)
            {
                if (masiv[i].Year > max)
                {
                    max = masiv[i].Year;
                }
            }
            return max;
        }

        private static void CountSortYear(Date[] masiv, Date[] output,int exp)
        {
            int[] count = new int[10];
            for (int i = 0; i < masiv.Length; i++)
            {
                count[(masiv[i].Year / exp) % 10]++;
            }

            for (int i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }

            for (int i = masiv.Length - 1; i >= 0; i--)
            {
                output[count[(masiv[i].Year / exp) % 10] - 1] = masiv[i];
                count[(masiv[i].Year / exp) % 10]--;
            }
            for (int i = 0; i < masiv.Length; i++)
            {
                masiv[i] = output[i];
            }
        }
        static void CountSortDay(Date[] masiv, Date[] output, int exp, int n)
        {
            int[] count = new int[10];

            for (int i = 0; i < n; i++)
            {
                count[(masiv[i].Day / exp) % 10]++;
            }

            for (int i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }

            for (int i = n - 1; i >= 0; i--)
            {
                output[count[(masiv[i].Day / exp) % 10] - 1] = masiv[i];
                count[(masiv[i].Day / exp) % 10]--;
            }

            for (int i = 0; i < n; i++)
            {
                masiv[i] = output[i];
            }
        }
        static void CountSortMonth(Date[] masiv, Date[] output, int exp, int n)
        {
            int[] count = new int[10];

            for (int i = 0; i < n; i++)
            {
                count[(masiv[i].Month / exp) % 10]++;
            }

            for (int i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }

            for (int i = n - 1; i >= 0; i--)
            {
                output[count[(masiv[i].Month / exp) % 10] - 1] = masiv[i];
                count[(masiv[i].Month / exp) % 10]--;
            }

            for (int i = 0; i < n; i++)
            {
                masiv[i] = output[i];
            }
        }
    }
}
