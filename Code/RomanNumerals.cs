using System;
using System.Collections.Immutable;
using System.Linq;

namespace Code
{
    public class RomanNumerals
    {
        public string DecimalToRoman(int n)
        {
            return CurrencyValues
                .Aggregate(
                    Tuple.Create(ImmutableList.Create<Tuple<string, int>>(), n),
                    (acc, tuple) =>
                    {
                        var oldList = acc.Item1;
                        var remaining = acc.Item2;
                        var romanValue = tuple.Item1;
                        var romanString = tuple.Item2;
                        var newList = oldList.Add(Tuple.Create(romanString, remaining/romanValue));
                        return Tuple.Create(newList, remaining % romanValue);
                    },
                    acc => acc.Item1)
                .Aggregate(
                    string.Empty,
                    (acc, tuple) => acc + string.Concat(Enumerable.Repeat(tuple.Item1, tuple.Item2)));
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

        private static readonly IImmutableList<Tuple<int, string>> CurrencyValues = ImmutableList.Create(
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
            Tuple.Create(1, "I"));
    }
}
