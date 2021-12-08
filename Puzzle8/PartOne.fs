module AoC2021.Puzzle8.PartOne

open System.IO
let Start () =
    printfn "== Solution 8 | Part One == "
    printfn "In the output values, how many times do digits 1, 4, 7, or 8 appear?"
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()
    
    let valueResult = new ResizeArray<int>();
    let fileLines = File.ReadLines("Puzzle8\input.txt")
    
    
    for fileLine in fileLines do
        let signals =
            fileLine
            |> fun s -> s.Split '|' |> Seq.head |> fun s -> s.Split ' '
    
        let numbers =
            fileLine
            |> fun s -> s.Split '|' |> Seq.last |> fun s -> s.Split ' '
            |> Seq.filter(fun s -> s |> Seq.map char |> Seq.length > 1)
            |> Seq.toArray
        
        let template = Template_Generator.GenerateDisplayTemplate(signals);

        let mutable i = 0
        numbers
        |> Seq.filter(fun s -> s |> Seq.map char |> Seq.length > 1)
        |> Seq.iter(fun s ->
            s |> Seq.map char |> Seq.toArray |> fun chars ->
                let display = template.Clone()
                display.Segments |> Seq.iter(fun x -> x.On <- false)
                display.Populate(chars)
                if display.Value = 1 || display.Value = 4 || display.Value = 7 || display.Value = 8 then
                    valueResult.Add(display.Value)
                    
                i <- i + 1
            )
        
    printfn $"  Answer:"
    printfn $"      Count: {valueResult |> Seq.length}"
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms"
 

