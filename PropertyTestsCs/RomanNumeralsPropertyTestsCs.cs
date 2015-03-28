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
        public void RoundTripPropertyTest()
        {
            var rnl = new RomanNumeralsLinq();

            Spec
                .For(Any.IntBetween(1, 3000), n => rnl.RomanToDecimal(rnl.DecimalToRoman(n)) == n)
                .Check(Configuration);
        }
    }
}
