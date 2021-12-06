module AoC2021.Puzzle6.PartTwo

open System.IO

let countFishByDay (dw: int64[]) (f:int) =
    dw[f] <- dw[f] + 1L
    dw

let Start() =
    printfn "== Solution 6 | Part Two == "
    printfn "How many lantern fish would there be after 256 days?"
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let line = File.ReadLines("Puzzle6\input.txt") |> Seq.head
    let days = 256
    
    let conveyor = line
                |> fun s -> s.Split ','
                |> Array.map int
                |> Array.fold countFishByDay (Array.create 9 0L)
   
    // Move the amount of fish around to track how many there are in which state
    for i in 1 .. days do
        let adultsReady = conveyor[0]
        conveyor[0] <- conveyor[1]
        conveyor[1] <- conveyor[2]
        conveyor[2] <- conveyor[3]
        conveyor[3] <- conveyor[4]
        conveyor[4] <- conveyor[5]
        conveyor[5] <- conveyor[6]
        conveyor[6] <- conveyor[7] + adultsReady
        conveyor[7] <- conveyor[8]
        conveyor[8] <- adultsReady
    
    
    printfn $"  Answer:"
    printfn $"       Amount of fish: {Array.sum conveyor}" 
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms"