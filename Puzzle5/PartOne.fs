module AoC2021.Puzzle5.PartOne

open System.IO

type Point(x : int, y : int) =
    member val X = x with get, set
    member val Y = y with get,set

type Line(startPoint : Point, endPoint : Point) =
    member this.GetLinePoints() : Point[] =
        let direction = if startPoint.X <> endPoint.X then "horizontal" else "vertical"
        if direction = "horizontal" then
            let dif = startPoint.X - endPoint.X
            let points = Array.zeroCreate ((abs dif + 1))
            let mutable arrayIndex = 0
            
            let startingPoint = min startPoint.X endPoint.X
            let endpoint = max startPoint.X endPoint.X
            
            for i = startingPoint to endpoint do
                points[arrayIndex] <- new Point(i, startPoint.Y)
                arrayIndex <- arrayIndex + 1
            
            points
        else
            let dif = startPoint.Y - endPoint.Y
            let points = Array.zeroCreate ((abs dif + 1))
            let mutable arrayIndex = 0
            
            let startingPoint = min startPoint.Y endPoint.Y
            let endpoint = max startPoint.Y endPoint.Y
            
            for i = startingPoint to endpoint do
                points[arrayIndex] <- new Point(startPoint.X, i)
                arrayIndex <- arrayIndex + 1
            
            points
   
    member val Start = startPoint with get, set
    member val End = endPoint with get, set
   

let Start() =
    printfn "== Solution 5 | Part One == "
    printfn "At how many points do at least two lines overlap?"
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let fileLines = File.ReadLines("Puzzle5\input.txt")
    let co = fileLines |> Seq.mapi (fun i s -> s.Split "->" |> Seq.map (fun p -> p.Trim()))

    let lines = co
                      |> Seq.map (fun coordinates -> coordinates
                                                    |> Seq.map (fun coordinate -> coordinate.Split ",")
                                                    |> Seq.map (fun (coordinate:string[]) -> new Point(coordinate.[0] |> int, coordinate.[1] |> int))
                                                    |> Seq.toArray)
                      |> Seq.filter(fun (pointPair:Point[]) -> (pointPair[0].X <> pointPair[1].X && pointPair[0].Y <> pointPair[1].Y) <> true)
                      |> Seq.map (fun pointPair -> new Line(pointPair[0], pointPair[1]))
                      |> Seq.toArray
          
    let gridSize = (max (lines |> Seq.map(fun line -> max line.Start.Y line.End.Y) |> Seq.max) (lines |> Seq.map(fun line -> max line.Start.X line.End.X) |> Seq.max)) + 1
                     
    let floor : int [,] = Array2D.zeroCreate gridSize gridSize
    
    for line in lines do
        let points = line.GetLinePoints()
        for i = 0 to (points.Length - 1) do
            floor[points[i].Y,points[i].X] <- floor[points[i].Y,points[i].X] + 1
    
    let mutable intersections = 0
    
    for i = 0 to (gridSize - 1) do
        floor[i,*] |> Array.iter(fun (value:int) ->
            if value > 1 then intersections <- intersections + 1)
        
    printfn $"  Answer:"
    printfn $"       Drawn lines: %A{lines.Length}"
    printfn $"       Floor size: %A{gridSize} x %A{gridSize}"
    printfn $"       Intersecting points: %A{intersections}"  
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms"
        