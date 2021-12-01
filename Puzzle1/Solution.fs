module AoC2021.Puzzle1.Solution
open System.IO
    
let start_solution_1() =
    
    let fileLines = File.ReadLines("Puzzle1\input.txt")
    let data = fileLines |> Seq.map int |> Seq.pairwise
    
    let mutable ascending = 0
    
    for first, second in data do
        match (first,second) with
            | (first, second) when first > second -> ascending <- ascending + 1
            | _ -> ()
            
    printfn "%A" ascending 