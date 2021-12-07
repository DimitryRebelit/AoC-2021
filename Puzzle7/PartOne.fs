module AoC2021.Puzzle7.PartOne

open System.IO



let Start() =
    printfn "== Solution 7 | Part One == "
    printfn "How much fuel must they spend to align to that position?"
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let values = File.ReadLines("Puzzle7\input.txt") |> Seq.head |> fun s -> s.Split ',' |> Array.map int
    let maxValue = values |> Seq.max
    
    let mutable totalcost = 0
    let mutable angle = 0
    
    for i = 1 to maxValue do
        let attemptCost = values |> Array.map(fun v -> (v - i) |> abs) |> Array.sum
        if i = 1 then
            totalcost <- attemptCost
        else if totalcost > attemptCost then
            totalcost <- attemptCost
            angle <- i
    
    printfn $"  Answer:"
    printfn $"      Angle: {angle}"
    printfn $"      Fuel cost: {totalcost}"
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms"
