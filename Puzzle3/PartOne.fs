module AoC2021.Puzzle3.PartOne

open System
open System.IO

let Start () =
    
    printfn "== Solution 3 | Part One == "
    printfn "What is the power consumption of the submarine? (Be sure to represent your answer in decimal, not binary.)"
    
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let fileLines = File.ReadLines("Puzzle3\input.txt")
    let bitSize = 11
    let mutable gamma = ""
    let mutable epsilon = ""
    
    for index in 0..bitSize do
        let data  = 
            fileLines
                |> Seq.map (fun x -> x[index])
                |> Seq.countBy id 
                |> Seq.maxBy snd
                |> fst
        gamma <- gamma + string data
        
    for index in 0..bitSize do
        let data  = 
            fileLines
                |> Seq.map (fun x -> x[index])
                |> Seq.countBy id 
                |> Seq.minBy snd
                |> fst
        epsilon <- epsilon + string data

    let gammaDecimal = Convert.ToInt32(gamma, 2)
    let epsilonDecimal = Convert.ToInt32(epsilon, 2)
    
    printfn $"  Answer:"
    printfn $"       Gamma byte: %A{gamma}"
    printfn $"       Epsilon byte: %A{epsilon}"
    printfn $"       Gamma decimal: %A{gammaDecimal}"      
    printfn $"       Epsilon decimal: %A{epsilonDecimal}"
    printfn $"       Epsilon * Gamma: %A{gammaDecimal * epsilonDecimal}" 
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms"    