module Kross.Tests

open Kross
open NUnit.Framework

[<Test>]
let ``Kross is the name`` () =
  Assert.AreEqual("Kross",Library.Name)
