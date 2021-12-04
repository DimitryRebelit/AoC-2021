module AoC2021.Puzzle4.PartOne

open System
open System.IO

let Start () =
    
    printfn "== Solution 4 | Part One == "
    printfn "What is the power consumption of the submarine? (Be sure to represent your answer in decimal, not binary.)"
    
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let fileLines = File.ReadLines("Puzzle4\input.txt")
    
    
    
    
    printfn $"  Answer:"