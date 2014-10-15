open FSharpEnumCast
open System.Collections.Generic

type SomeEnum =
| None      = 0
| One       = 1
| Two       = 2
| Three     = 3
| Four      = 4
| Five      = 5
| Six       = 6
| Seven     = 7
| Eight     = 8
| Nine      = 9
| Ten       = 10
| Eleven    = 11
| Twelve    = 12
| Thirteen  = 13
| Fourteen  = 14
| Fifteen   = 15
| Sixteen   = 16
| Seventeen = 17
| Eighteen  = 18
| Nineteen  = 19

let private testSet = [| 
    SomeEnum.One 
    SomeEnum.Three 
    SomeEnum.Five 
    SomeEnum.Eight
    SomeEnum.Ten
    SomeEnum.Eleven
    SomeEnum.Fourteen
    SomeEnum.Sixteen
    SomeEnum.Seventeen
    SomeEnum.Nineteen
    |]

let private rand = System.Random()
let private testInput : SomeEnum[] = Array.init(1000) (fun i -> LanguagePrimitives.EnumOfValue (rand.Next 20))
let private intTestInput = testInput |> Array.map(fun e -> (int) e)

let timeit name mult (action : unit -> unit) =
    action ()
    printfn "Testing %s" name
    let sw = new System.Diagnostics.Stopwatch()
    sw.Start ()
    let tc = 1000*mult
    for x in 0..tc do
        action ()
    sw.Stop ()
    (float sw.ElapsedMilliseconds) / (float mult)

[<EntryPoint>]
let main _ = 
    let hashSet           = HashSet testSet
    let fSharpSet         = set testSet
    let cSharp            = CSharpEnumCast.CSharpDenseIntSet<SomeEnum>(testSet)
    let fSharp            = EnumDenseIntSet<SomeEnum>(testSet)
    let fSharpConvertible = ConvertibleDenseIntSet<SomeEnum>(testSet)
    let fSharpUgly        = UglyDenseIntSet<SomeEnum>(testSet)
    
    let hashSetTime             = timeit "HashSet           "   10  <| fun () -> for x in testInput do ignore   <| hashSet.Contains x
    let fSharpSetTime           = timeit "F# set            "   1   <| fun () -> for x in testInput do ignore   <| fSharpSet.Contains x
    let cSharpTime              = timeit "C#                "   10  <| fun () -> for x in testInput do ignore   <| cSharp.Contains x
    let cSharpTime2             = timeit "C# (int input)    "   100 <| fun () -> for x in intTestInput do ignore<| cSharp.Contains2 x
    let fSharpTime              = timeit "F# (generic enum) "   10  <| fun () -> for x in testInput do ignore   <| fSharp.Contains x
    let fSharpConvertibleTime   = timeit "F# (IConvertible) "   10  <| fun () -> for x in testInput do ignore   <| fSharpConvertible.Contains x
    let fSharpUglyTime          = timeit "F# (int input)    "   100 <| fun () -> for x in intTestInput do ignore<| fSharpUgly.Contains x

    printfn "HashSet:           %g ms" hashSetTime
    printfn "F# set:            %g ms" fSharpSetTime
    printfn "C#:                %g ms" cSharpTime
    printfn "C# (int input):    %g ms" cSharpTime2
    printfn "F# (generic enum): %g ms" fSharpTime
    printfn "F# (IConvertible): %g ms" fSharpConvertibleTime
    printfn "F# (int input):    %g ms" fSharpUglyTime
    printfn ""
    printfn "Press Enter to exit."
    System.Console.ReadLine() |> ignore
    0 // return an integer exit code
