namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Kross")>]
[<assembly: AssemblyProductAttribute("Kross")>]
[<assembly: AssemblyDescriptionAttribute("Crossplatform FSharp Application")>]
[<assembly: AssemblyVersionAttribute("1.0")>]
[<assembly: AssemblyFileVersionAttribute("1.0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "1.0"
