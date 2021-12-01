module AoC2021.Puzzle1.Solution

open System.IO

let start_solution_1 () =
    let mutable count = 0
    let fileLines = File.ReadLines("Puzzle1\input.txt")
    let data = fileLines |> Seq.map int |> Seq.pairwise

    for first, second in data do
        match (first, second) with
        | (first, second) when first < second -> count <- count + 1
        | _ -> ()

    printfn "%A" count

let start_solution_1_part_two () =
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

    printfn "%A" count
