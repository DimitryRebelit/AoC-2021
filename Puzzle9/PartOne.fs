module AoC2021.Puzzle9.PartOne

open System.IO

let neighbors r c (A:'a[,]) =
    [if r > 0 then yield A.[r-1,c]
     if r < Array2D.length1 A - 1 then yield A.[r+1,c]
     if c > 0 then yield A.[r,c-1]
     if c < Array2D.length2 A - 1 then yield A.[r,c+1]]

let Start () =
    printfn "== Solution 9 | Part One == "
    printfn "What is the sum of the risk levels of all low points on your heightmap?"
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()
    
    let oceanFloor =
        File.ReadLines("Puzzle9\input.txt")
        |> Seq.map
            (fun s ->
                s.ToCharArray()
                |> Seq.map string
                |> Seq.map int
                |> Seq.toArray)
        |> Seq.toArray
        |> array2D
        
    let lowPoints = new ResizeArray<int>()
    
    let columns = (oceanFloor |> Array2D.length1) - 1
    let rows = (oceanFloor |> Array2D.length2) - 1
    
    for column = 0 to columns do
        for row = 0 to rows do
            let target = oceanFloor[column,row]
            let h = neighbors column row oceanFloor |> Seq.toArray
            let lowPoint = h |> Array.forall(fun x -> x > target)
            
            if lowPoint then
                lowPoints.Add(target)

    printfn $"  Answer:"
    printfn $"       Sum of risk: %A{(lowPoints |> Seq.map(fun x -> 1 + x) |> Seq.sum)}" 
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms"

//    let data = fileLines |> Seq.map int |> Seq.pairwise
