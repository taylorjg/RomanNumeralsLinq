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
                .OrderByDescending(cv => cv.Item1)
                .Aggregate(
                    Tuple.Create(ImmutableList.Create<Tuple<string, int>>(), n),
                    (acc, tuple) =>
                    {
                        var oldList = acc.Item1;
                        var remaining = acc.Item2;
                        var romanValue = tuple.Item1;
                        var romanString = tuple.Item2;
                        var newList = oldList.Add(Tuple.Create(romanString, remaining/romanValue));
                        return Tuple.Create(newList, remaining%romanValue);
                    },
                    acc => acc.Item1)
                .Aggregate(
                    string.Empty,
                    (acc, tuple) => acc + string.Concat(Enumerable.Repeat(tuple.Item1, tuple.Item2)));
        }

        public int RomanToDecimal(string s)
        {
            return CurrencyValues
                .OrderByDescending(cv => cv.Item2.Length)
                .Aggregate(
                    Tuple.Create(s, 0),
                    (acc, tuple) =>
                    {
                        var sOld = acc.Item1;
                        var nOld = acc.Item2;
                        var romanValue = tuple.Item1;
                        var romanString = tuple.Item2;
                        var numOccurrences = 0;
                        var cursor = 0;
                        for (;;)
                        {
                            var pos = sOld.IndexOf(romanString, cursor, StringComparison.Ordinal);
                            if (pos < 0) break;
                            cursor = pos + 1;
                            numOccurrences++;
                        }
                        var sNew = sOld.Replace(romanString, string.Empty);
                        var nNew = nOld + (romanValue*numOccurrences);
                        return Tuple.Create(sNew, nNew);
                    },
                    acc => acc.Item2);
        }

        private static readonly IImmutableList<Tuple<int, string>> CurrencyValues = ImmutableList.Create(
            Tuple.Create(1, "I"),
            Tuple.Create(4, "IV"),
            Tuple.Create(5, "V"), 
            Tuple.Create(9, "IX"),
            Tuple.Create(10, "X"),
            Tuple.Create(40, "XL"),
            Tuple.Create(50, "L"),
            Tuple.Create(90, "XC"),
            Tuple.Create(100, "C"),
            Tuple.Create(400, "CD"),
            Tuple.Create(500, "D"),
            Tuple.Create(900, "CM"),
            Tuple.Create(1000, "M"));
    }
}
