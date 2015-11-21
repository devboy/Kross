namespace Kross

module Library = 

    let Name = "Kross"

module FSSnip =

    open System
    open FSharp.Data

    let private rand = Random()
    let rec private chars () = seq {
        let d = rand.NextDouble()
        if d < 0.2 then yield rand.Next(48,57)
        if d > 0.4 then yield rand.Next(65,90)
        if d < 0.8 then yield rand.Next(97,122)
        yield! chars ()
        }

    let rec ids () = seq {
        let x,y = chars() |> Seq.map char |> Seq.pairwise |> Seq.head
        yield sprintf "%c%c" x y
        yield! ids ()
        }

    let urls = ids() |> Seq.map (sprintf "http://fssnip.net/%s")

    let htmls = 
        Seq.map (fun url -> async{ return url,Http.RequestString url }) urls
        |> Seq.map (Async.Catch >> Async.RunSynchronously)
        |> Seq.choose (function Choice1Of2 x -> Some x | _ -> None)

    let title = 
        HtmlDocument.Parse 
        >> fun doc -> 
            doc.Descendants["h2"] 
            |> Seq.tryHead
            |> Option.map (fun h -> h.InnerText())
            |> fun o -> (defaultArg o "[MissingTitle]")

    let posts = Seq.map (fun (url,html) -> url,title html) htmls

    let LoadPostsAsync c = async {
            return 
                posts 
                |> Seq.map (fun (url,title) -> sprintf "%s (%s)" title url)
                |> Seq.take c
            }