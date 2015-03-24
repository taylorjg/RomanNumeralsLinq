using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    public class RomanNumerals
    {
        public string DecimalToRoman(int n)
        {
            var map = CurrencyValues.Aggregate(
                Tuple.Create(new Dictionary<string, int>(), n),
                (acc, tuple) =>
                {
                    var m = acc.Item1;
                    var r = acc.Item2;
                    var v = tuple.Item1;
                    var s = tuple.Item2;
                    m.Add(s, r/v);
                    return Tuple.Create(m, r%v);
                },
                acc => acc.Item1);

            return map.Aggregate(
                string.Empty,
                (acc, kvp) => acc + string.Concat(Enumerable.Repeat(kvp.Key, kvp.Value)));
        }

        public int RomanToDecimal(string s)
        {
            var n = 0;

            CurrencyValues
                .Where(tuple => tuple.Item2.Length > 1)
                .ToList()
                .ForEach(tuple =>
                {
                    if (s.IndexOf(tuple.Item2, StringComparison.Ordinal) >= 0)
                    {
                        n += tuple.Item1;
                        s = s.Replace(tuple.Item2, string.Empty);
                    }
                });

            var remainingTotal =
                (from romanChar in s
                    join tuple in CurrencyValues on romanChar.ToString() equals tuple.Item2
                    select tuple.Item1).Sum();

            return n + remainingTotal;
        }

        private static readonly List<Tuple<int, string>> CurrencyValues = new List<Tuple<int, string>>
        {
            Tuple.Create(1000, "M"),
            Tuple.Create(900, "CM"),
            Tuple.Create(500, "D"),
            Tuple.Create(400, "CD"),
            Tuple.Create(100, "C"),
            Tuple.Create(90, "XC"),
            Tuple.Create(50, "L"),
            Tuple.Create(40, "XL"),
            Tuple.Create(10, "X"),
            Tuple.Create(9, "IX"),
            Tuple.Create(5, "V"),
            Tuple.Create(4, "IV"),
            Tuple.Create(1, "I")
        };
    }
}
