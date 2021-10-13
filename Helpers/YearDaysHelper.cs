using System;

namespace CentWorkTimeTracker.Helpers
{
    public static class YearDaysHelper
    {
        /// <summary>
        /// Количество выходных дней в году
        /// </summary>
        /// <param name="year">Год</param>
        /// <returns>Количество выходных дней в году</returns>
        public static int GetHolidaysCount(int year)
        {
            return GetHolidaysCount(new DateTime(year, 01, 01), new DateTime(year, 12, 31));
        }

        /// <summary>
        /// Количество выходных дней в промежутке
        /// </summary>
        /// <param name="from">Начало</param>
        /// <param name="to">Конец</param>
        /// <returns>Количество выходных дней в промежутке</returns>
        public static int GetHolidaysCount(DateTime from, DateTime to)
        {
            if (to > from)
            {
                return 0;
            }
            return GetDaysCount(to, from) - GetWorkDaysCount(to, from);
        }

        /// <summary>
        /// Количество дней в промежутке
        /// </summary>
        /// <param name="from">Начало</param>
        /// <param name="to">Конец</param>
        /// <returns>Общее количество дней в промежутке</returns>
        public static int GetDaysCount(DateTime from, DateTime to)
        {
            return from > to ? 0 : (to - from).Days + 1;
        }

        /// <summary>
        /// Количество дней в году
        /// </summary>
        /// <param name="year">Год</param>
        /// <returns>Общее количество дней в году</returns>
        public static int GetDaysCount(int year)
        {
            return GetDaysCount(new DateTime(year, 01, 01), new DateTime(year, 12, 31));
        }

        /// <summary>
        /// Количество рабочих дней в году
        /// </summary>
        /// <param name="year">Год</param>
        /// <returns>Количество рабочих дней в году</returns>
        public static int GetWorkDaysCount(int year)
        {
            return GetWorkDaysCount(new DateTime(year, 01, 01), new DateTime(year, 12, 31));
        }

        /// <summary>
        /// Количество рабочих дней в промежутке
        /// </summary>
        /// <param name="from">Начало</param>
        /// <param name="to">Конец</param>
        /// <returns>Количество рабочих дней в году</returns>
        public static int GetWorkDaysCount(DateTime from, DateTime to)
        {
            if (to.Date < from.Date)
            {
                return 0;
            }
            int daysCount = 0;
            for (DateTime i = from; i <= to; i = i.AddDays(1))
            {
                Console.WriteLine(i.ToShortDateString());
                if (i.DayOfWeek != DayOfWeek.Sunday && i.DayOfWeek != DayOfWeek.Saturday)
                {
                    daysCount++;
                }
            }
            return daysCount;
        }
    }
}