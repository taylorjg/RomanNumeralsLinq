using System;
using System.Collections.Immutable;
using System.Linq;

namespace Code
{
    public class RomanNumeralsLinq
    {
        public string DecimalToRoman(int n)
        {
            return CurrencyValues
                .OrderByDescending(cv => cv.Item1)
                .Aggregate(
                    string.Empty,
                    (acc, tuple) =>
                    {
                        var romanValue = tuple.Item1;
                        var romanString = tuple.Item2;
                        var newString = acc + string.Concat(Enumerable.Repeat(romanString, n/romanValue));
                        n = n%romanValue;
                        return newString;
                    });
        }

        public int RomanToDecimal(string s)
        {
            Func<string, string, string[]> removeOccurrences = (s1, sep) =>
                s1.Split(new[] {sep}, StringSplitOptions.None);

            return CurrencyValues
                .OrderByDescending(cv => cv.Item2.Length)
                .Aggregate(
                    0,
                    (acc, tuple) =>
                    {
                        var romanValue = tuple.Item1;
                        var romanString = tuple.Item2;
                        var remainingBits = removeOccurrences(s, romanString);
                        var numOccurrences = remainingBits.Length - 1;
                        s = string.Concat(remainingBits);
                        return acc + (romanValue*numOccurrences);
                    });
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
