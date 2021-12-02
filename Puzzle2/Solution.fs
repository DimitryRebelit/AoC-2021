module AoC2021.Puzzle2.Solution

open System.IO

let start_solution_2 () =
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let mutable horizontal = 0
    let mutable vertical = 0

    let fileLines = File.ReadLines("Puzzle2\input.txt")

    let data =
        fileLines
        |> Seq.mapi (fun index x -> x.Split [| ' ' |])
        |> Seq.mapi (fun index x -> (x [ 0 ], x [ 1 ] |> int))

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


let start_solution_2_part_two () =
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let mutable horizontal = 0
    let mutable depth = 0
    let mutable aim = 0

    let fileLines = File.ReadLines("Puzzle2\input.txt")

    let data =
        fileLines
        |> Seq.mapi (fun index x -> x.Split [| ' ' |])
        |> Seq.mapi (fun index x -> (x[0], x[1] |> int))

    for direction, amount in data do
        match (direction, amount) with
        | (direction, amount) when direction = "forward" ->
            horizontal <- horizontal + amount
            depth <- depth + (amount * aim)
        | (direction, amount) when direction = "up" -> aim <- aim - amount
        | (direction, amount) when direction = "down" -> aim <- aim + amount
        | _ -> ()

    printfn $"  Answer:"
    printfn $"      Horizontal:          %A{horizontal}"
    printfn $"      Depth:               %A{depth}"
    printfn $"      Depth & Horizontal:  %A{horizontal * depth}"
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms"

let Start () =
    printfn "== Solution 2 == "
    printfn "What do you get if you multiply your final horizontal position by your final depth?"
    start_solution_2 ()
    printfn
        "Using this new interpretation of the commands, calculate the horizontal position and depth you would have after following the planned course. What do you get if you multiply your final horizontal position by your final depth?"
    start_solution_2_part_two ()
