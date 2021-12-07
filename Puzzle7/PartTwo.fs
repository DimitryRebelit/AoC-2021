module AoC2021.Puzzle7.PartTwo

open System
open System.IO



let Start () =
    printfn "== Solution 7 | Part Two == "
    printfn "How much fuel must they spend to align to that position"
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()

    let lowestCost =
        File.ReadLines("Puzzle7\input.txt")
        |> Seq.head
        |> fun s ->
            s.Split ','
            |> Array.map int
            |> fun values ->
                [| (values |> Seq.min) .. (values |> Seq.max) |]
                |> Array.map
                    (fun angle ->
                        values
                        |> Array.map
                            (fun v ->
                                (v - angle)
                                |> abs
                                |> fun distance -> (distance * (distance + 1) / 2))
                        |> Array.sum
                        |> fun fuel -> (angle, fuel))
                |> Array.minBy snd

    printfn $"  Answer:"
    printfn $"      Angle: {lowestCost |> fst}"
    printfn $"      Fuel cost: {lowestCost |> snd}"
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms"
