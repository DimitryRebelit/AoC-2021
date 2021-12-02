module AoC2021.Puzzle2.Solution
open System.IO

let start_solution_2 () =
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let mutable horizontal = 0
    let mutable vertical = 0
    
    let fileLines = File.ReadLines("Puzzle2\input.txt")
    let data =
            fileLines
            |> Seq.mapi (fun index x -> x.Split [|' '|])
            |> Seq.mapi (fun index x -> (x[0], x[1] |> int))

    for direction, amount in data do
        match (direction, amount) with
        | (direction, amount) when direction = "forward" -> horizontal <- horizontal + amount
        | (direction, amount) when direction = "up" -> vertical <- vertical - amount
        | (direction, amount) when direction = "down" -> vertical <- vertical + amount
        | _ -> ()
    
    printfn $"  Answer:"
    printfn $"      Horizontal:             %A{horizontal}"
    printfn $"      Vertical:               %A{vertical}"
    printfn $"      Vertical & Horizontal:  %A{horizontal * vertical}"
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms"

let Start () =
    printfn "== Solution 2 == "
    printfn "What do you get if you multiply your final horizontal position by your final depth?"
    start_solution_2()