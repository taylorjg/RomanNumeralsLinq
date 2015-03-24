using Code;
using FsCheck;
using FsCheck.Fluent;
using FsCheckUtils;
using NUnit.Framework;

namespace PropertyTestsCs
{
    [TestFixture]
    public class RomanNumeralsPropertyTestsCs
    {
        private static readonly Configuration Configuration = Config.VerboseThrowOnFailure.ToConfiguration();

        [Test]
        public void RoundTripProperty()
        {
            var rn = new RomanNumerals();

            Spec
                .For(Any.IntBetween(1, 3000), n => rn.RomanToDecimal(rn.DecimalToRoman(n)) == n)
                .Check(Configuration);
        }
    }
}
