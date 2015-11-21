namespace Kross.App.IOS

open System
open System.Drawing

open Foundation
open UIKit

[<Register ("ViewController")>]
type ViewController (handle:IntPtr) =
    inherit UIViewController (handle)

    override x.DidReceiveMemoryWarning () =
        // Releases the view if it doesn't have a superview.
        base.DidReceiveMemoryWarning ()
        // Release any cached data, images, etc that aren't in use.

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        // Perform any additional setup after loading the view, typically from a nib.
        let l = new UITextView(x.View.Frame)
        l.AutoresizingMask <- UIViewAutoresizing.All
        l.Text <- Kross.Library.Name
        x.View.AddSubview l

        Async.Start <| async {
            let! posts = Kross.FSSnip.LoadPostsAsync 20
            let text = String.concat "\n\n" posts
            do CoreFoundation.DispatchQueue.MainQueue.DispatchAsync(fun () -> l.Text <- text)
            }

    override x.ShouldAutorotateToInterfaceOrientation (toInterfaceOrientation) =
        // Return true for supported orientations
        if UIDevice.CurrentDevice.UserInterfaceIdiom = UIUserInterfaceIdiom.Phone then
           toInterfaceOrientation <> UIInterfaceOrientation.PortraitUpsideDown
        else
           true
