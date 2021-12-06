module AoC2021.Puzzle6.PartOne

open System.IO

type LanternFish(birthTimer : int) =
    member val BirthTimer = birthTimer with get, set
    
    member this.GivingBirth() : bool =
        if this.BirthTimer = 0 then
            this.BirthTimer <- 6
            true
        else
            this.BirthTimer <- this.BirthTimer - 1
            false



let Start() =
    printfn "== Solution 6 | Part One == "
    printfn "How many lantern fish would there be after 80 days?"
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let fileLines = File.ReadLines("Puzzle6\input.txt")
    let f = new ResizeArray<LanternFish>()
    
    // Get the initial fish
    fileLines |> Seq.head |> fun s -> s.Split ',' |> Seq.map int |>  Seq.map (fun i -> new LanternFish(i)) |> Seq.iter(fun fish -> f.Add(fish))
    let days = 80
    
    for i = 1 to days do
        for fish in (f |> Seq.toArray) do
            let hasGivenBirth = fish.GivingBirth()
            if hasGivenBirth then
                let fish = new LanternFish(8)
                f.Add(fish)
        
    printfn $"  Answer:"
    printfn $"       Amount of fish: %A{f.Count}" 
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms"