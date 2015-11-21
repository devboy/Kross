module Kross.Tests

open Kross
open NUnit.Framework
open FSharp.Data

[<Test>]
let ``Kross is the name`` () =
  Assert.AreEqual("Kross",Library.Name)

[<Test>]
let ``PostId should be of length 2`` () =
  Assert.AreEqual(2,FSSnip.ids() |> Seq.head |> String.length)

[<Test>]
let ``PostIds should be kind of random`` () =
  Assert.AreNotEqual(FSSnip.ids() |> Seq.head,FSSnip.ids() |> Seq.head)

[<Test>]
let ``Url should be valid`` () =
  Assert.IsTrue(System.Uri(FSSnip.urls |> Seq.head).IsAbsoluteUri)

//[<Test>]  
//let ``Urls should be plenty`` () =
//  Assert.AreEqual(10,FSSnip.urls |> Seq.take 10 |> Seq.length)
//
//[<Test>]
//let ``Posts should be plenty`` () =
//  Assert.AreEqual(10,FSSnip.posts |> Seq.take 10 |> Seq.length)