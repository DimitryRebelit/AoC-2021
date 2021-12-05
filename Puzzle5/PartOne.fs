module AoC2021.Puzzle5.PartOne

open System.IO

let start() =
    printfn "== Solution 5 | Part One == "
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let fileLines = File.ReadLines("Puzzle5\input.txt")
    
    let co = fileLines |> List.mapi (fun i s -> s.Split "->" |> List.map (fun p -> p.Trim()))


//            // Map them to coordinates
//            |> List.map (fun positions -> positions
//                    |> List.map (fun pos ->
//                        let coordinatesText = pos.Split ','
//                        coordinatesText
//                        |> List.map(fun coordinates ->
//                            let x = coordinates[0] |> int
//                            let y = coordinates[1] |> int
//                            
//                            (x,y))
//                    )) 
//                )
    
    let linesPoints = co |> List.map (fun coordinates ->
                                coordinates
                                |> List.map (fun coordinate -> coordinate.Split "," |> Array.toList)
                                |> List.map (fun coordinate ->
                                    let x = coordinate.[0] |> int
                                    let y = coordinate.[1] |> int
                                    (x,y)
                                ))
            
    let lines = linesPoints |> List.filter(fun points -> points.[0][0] <> points.[1][0]
            
        
        )
    
    printfn "%A" lines
    
    printfn $"  Answer:"
        