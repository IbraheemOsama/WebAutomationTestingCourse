using System.Collections.Generic;

namespace Tax.Core
{
    public class YearService : IYearService
    {
        private static readonly Dictionary<int, double> YearPercentage = new Dictionary<int, double>
        {
            { 2000, 1.5/100 },
            { 2016, 2.5/100 },
            { 2017, 3.5/100 },
            { 2018, 4.5/100 },
            { 2019, 5.5/100 }
        };

        public decimal GetYearPercentage(int year)
        {
            return (decimal)(YearPercentage.ContainsKey(year) ? YearPercentage[year] : 6.0/100);
        }
    }
}
