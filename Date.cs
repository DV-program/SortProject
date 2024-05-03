using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sortirovki_C__Valirakhmanov
{
    public class Date : IComparable
    {
        public int Day { get; }
        public int Month { get; }
        public int Year { get;}

        public Date(int day = 0, int month = 0, int year = 0)
        {
            Day = day;
            Month = month;
            Year = year;
        }
        public static void WriteDate(Date date)
        {
            Console.Write(date.Day + ":" + date.Month + ":" + date.Year);
        }

        public override string ToString()
        {
            return $"{Day}/{Month}/{Year}";
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Date otherDate = obj as Date;
            if (otherDate != null)
            {
                if (Year != otherDate.Year)
                {
                    return Year.CompareTo(otherDate.Year);
                }
                else if (Month != otherDate.Month)
                {
                    return Month.CompareTo(otherDate.Month);
                }
                else
                {
                    return Day.CompareTo(otherDate.Day);
                }
            }
            else
            {
                throw new ArgumentException("Object is not a Date");
            }
        }

        public int CompareTo(Date otherDate)
        {
            if (otherDate == null) return 1;

            if (Year != otherDate.Year)
            {
                return Year.CompareTo(otherDate.Year);
            }
            else if (Month != otherDate.Month)
            {
                return Month.CompareTo(otherDate.Month);
            }
            else
            {
                return Day.CompareTo(otherDate.Day);
            }
        }
    }
}
