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
let private testInput : SomeEnum[] = Array.init(1000000) (fun i -> LanguagePrimitives.EnumOfValue (i % 20))
let private intTestInput = testInput |> Array.map(fun e -> (int) e)

[<EntryPoint>]
let main _ = 
    let sw = new System.Diagnostics.Stopwatch()
    let hashSet           = HashSet testSet
    let fSharpSet         = set testSet
    let cSharp            = CSharpEnumCast.CSharpDenseIntSet<SomeEnum>(testSet)
    let fSharp            = EnumDenseIntSet<SomeEnum>(testSet)
    let fSharpConvertible = ConvertibleDenseIntSet<SomeEnum>(testSet)
    let fSharpUgly        = UglyDenseIntSet<SomeEnum>(testSet)
    printfn "Testing HashSet..."
    sw.Start()
    let hashSetOutput = testInput |> Array.filter (fun input -> hashSet.Contains input)
    sw.Stop()
    let hashSetTime = sw.Elapsed.Milliseconds
    sw.Reset()
    printfn "Testing Set..."
    sw.Start()
    let fSharpSetOutput = testInput |> Array.filter (fun input -> Set.contains input fSharpSet)
    sw.Stop()
    let fSharpSetTime = sw.Elapsed.Milliseconds
    sw.Reset()
    printfn "Testing C#..."
    sw.Start()
    let cSharpOutput = testInput |> Array.filter (fun input -> cSharp.Contains input)
    sw.Stop()
    let cSharpTime = sw.Elapsed.Milliseconds
    sw.Reset()
    printfn "Testing F#..."
    sw.Start()
    let fSharpOutput = testInput |> Array.filter (fun input -> fSharp.Contains input)
    sw.Stop()
    let fSharpTime = sw.Elapsed.Milliseconds
    sw.Reset()
    printfn "Testing F# (IConvertible)..."
    sw.Start()
    let fSharpConvertibleOutput = testInput |> Array.filter (fun input -> fSharpConvertible.Contains input)
    sw.Stop()
    let fSharpConvertibleTime = sw.Elapsed.Milliseconds
    sw.Reset()
    printfn "Testing F# (int input)..."
    sw.Start()
    let fSharpUglyOutput = intTestInput |> Array.filter (fun input -> fSharpUgly.Contains input)
    sw.Stop()
    let fSharpUglyTime = sw.Elapsed.Milliseconds
    printfn "F# set:            %d ms" fSharpSetTime
    printfn "HashSet:           %d ms" hashSetTime
    printfn "C#:                %d ms" cSharpTime
    printfn "F# (generic enum): %d ms" fSharpTime
    printfn "F# (IConvertible): %d ms" fSharpConvertibleTime
    printfn "F# (int input):    %d ms" fSharpUglyTime
    printfn ""
    printfn "Press Enter to exit."
    System.Console.ReadLine() |> ignore
    0 // return an integer exit code
