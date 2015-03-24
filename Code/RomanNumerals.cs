using System.Collections.Generic;
using System.Linq;

namespace Code
{
    public class RomanNumerals
    {
        public string DecimalToRoman(int n)
        {
            var map = new Dictionary<string, int>();

            CurrencyValues.OrderBy(kvp => kvp.Key)
                .Reverse()
                .ToList()
                .ForEach(kvp =>
                {
                    map.Add(kvp.Value, n/kvp.Key);
                    n = n%kvp.Key;
                });

            return map.Aggregate(
                string.Empty,
                (acc, kvp) => acc + string.Concat(Enumerable.Repeat(kvp.Key, kvp.Value)));
        }

        public int RomanToDecimal(string s)
        {
            var n = 0;

            CurrencyValues
                .Where(c => c.Value.Length > 1)
                .ToList()
                .ForEach(c =>
                {
                    if (s.IndexOf(c.Value) >= 0)
                    {
                        n += c.Key;
                        s = s.Replace(c.Value, string.Empty);
                    }
                });

            var remainingTotal =
                (from romanChar in s
                    join cv in CurrencyValues.ToList() on romanChar.ToString() equals cv.Value
                    select cv.Key).Sum();

            return n + remainingTotal;
        }

        private static readonly SortedList<int, string> CurrencyValues = new SortedList<int, string>
        {
            {1000, "M"},
            {900, "CM"},
            {500, "D"},
            {400, "CD"},
            {100, "C"},
            {90, "XC"},
            {50, "L"},
            {40, "XL"},
            {10, "X"},
            {9, "IX"},
            {5, "V"},
            {4, "IV"},
            {1, "I"}
        };
    }
}
