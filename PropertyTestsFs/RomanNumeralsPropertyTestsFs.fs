module PropertyTestsFs.RomanNumeralsPropertyTestsFs

open NUnit.Framework
open Code
open FsCheck
open Gen
open Arb
open Prop

[<Test>]
let ``round trip property test``() =
    let rn = new RomanNumerals()
    let arb = fromGen(choose (1, 3000))
    let property = forAll arb (fun n -> rn.RomanToDecimal(rn.DecimalToRoman(n)) = n)
    Check.One (Config.VerboseThrowOnFailure, property)
