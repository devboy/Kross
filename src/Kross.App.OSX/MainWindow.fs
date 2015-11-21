namespace Kross.App.OSX

open System
open Foundation
open AppKit

[<Register ("MainWindow")>]
type MainWindow =
    inherit NSWindow

    new () = { inherit NSWindow () }
    new (handle : IntPtr) = { inherit NSWindow (handle) }

    [<Export ("initWithCoder:")>]
    new (coder : NSCoder) = { inherit NSWindow (coder) }

    override x.AwakeFromNib () =
        base.AwakeFromNib ()
        x.Title <- Kross.Library.Name

        let l = new NSTextView(x.ContentView.Frame)
        new NSAttributedString(Kross.Library.Name) 
        |> l.TextStorage.SetString 
        x.ContentView.AddSubview l

        Async.Start <| async { 
            let! posts = Kross.FSSnip.LoadPostsAsync 20
            let text = new NSAttributedString(String.concat "\n\n" posts)
            do CoreFoundation.DispatchQueue.MainQueue.DispatchAsync(fun () -> text |> l.TextStorage.SetString)
            }