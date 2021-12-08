module AoC2021.Puzzle8.PartTwo

open System.IO
open AoC2021.Puzzle8.Models.Display
open AoC2021.Puzzle8.PartOne

let Start () =
    printfn "== Solution 8 | Part Two == "
    printfn "What do you get if you add up all of the output values?"
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
      
        let mutable numberResult = ""
        for number in numbers do
            let display = template.Clone()
            display.Populate(number |> Seq.map char |> Seq.toArray)
            numberResult <- numberResult + $"{display.Value}"
        
        valueResult.Add(numberResult |> int)
        
    printfn "%A" valueResult
    printfn $"  Answer:"
    printfn $"      Count: {valueResult |> Seq.sum}"
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms"
 