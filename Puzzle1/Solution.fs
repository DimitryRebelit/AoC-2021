module AoC2021.Puzzle1.Solution
open System.IO

let start_solution_1 () =
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let mutable count = 0
    let fileLines = File.ReadLines("Puzzle1\input.txt")
    let data = fileLines |> Seq.map int |> Seq.pairwise

    for first, second in data do
        match (first, second) with
        | (first, second) when first < second -> count <- count + 1
        | _ -> ()

    printfn $"  Answer: %A{count}"
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms"

let start_solution_1_part_two () =
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let mutable count = 0
    let fileLines = File.ReadLines("Puzzle1\input.txt")

    let windowedData =
        fileLines
        |> Seq.map int
        |> Seq.windowed 3
        |> Seq.map (fun window -> window |> Array.sum)
        |> Seq.pairwise


    for first, second in windowedData do
        match (first, second) with
        | (first, second) when first < second -> count <- count + 1
        | _ -> ()

    printfn $"  Answer: %A{count}"
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms"

let Start () =
    printfn "== Solution 1 == "
    printfn "How many measurements are larger than the previous measurement?"
    start_solution_1()
    printfn "Consider sums of a three-measurement sliding window. How many sums are larger than the previous sum?"
    start_solution_1_part_two()