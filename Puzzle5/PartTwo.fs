module AoC2021.Puzzle5.PartTwo

open System.IO

type Point(x : int, y : int) =
    member val X = x with get, set
    member val Y = y with get,set

type Line(startPoint : Point, endPoint : Point) =
    member this.GetLinePoints() : Point[] =
        if this.Direction = "horizontal" then
            this.DrawDirectionX <- if startPoint.X < endPoint.X then "LeftToRight" else "RightToLeft"
            
            let amountOfPointsInLine = ((abs (startPoint.X - endPoint.X)))
            let points = Array.zeroCreate (amountOfPointsInLine + 1)
            
            for i = 0 to amountOfPointsInLine do
                let pointX = if this.DrawDirectionX = "LeftToRight" then startPoint.X + i else startPoint.X - i
                let pointY = startPoint.Y
                points[i] <- new Point(pointX, pointY)
            
            points
        else if this.Direction = "vertical" then
            this.DrawDirectionY <- if startPoint.Y < endPoint.Y then "TopToBottom" else "BottomToTop" 
            
            let amountOfPointsInLine = ((abs (startPoint.Y - endPoint.Y)))
            let points = Array.zeroCreate (amountOfPointsInLine + 1)
            
            for i = 0 to amountOfPointsInLine do
                let pointX = startPoint.X
                let pointY = if this.DrawDirectionY = "TopToBottom" then startPoint.Y + i else startPoint.Y - i
                points[i] <- new Point(pointX, pointY)
            
            points
        else
            // Get direction
            this.DrawDirectionX <- if startPoint.X < endPoint.X then "LeftToRight" else "RightToLeft"
            this.DrawDirectionY <- if startPoint.Y < endPoint.Y then "TopToBottom" else "BottomToTop" 
            
            let amountOfPointsInLine = ((abs (startPoint.Y - endPoint.Y)))
            let points = Array.zeroCreate (amountOfPointsInLine + 1)
            
            for i = 0 to amountOfPointsInLine do
                let pointX = if this.DrawDirectionX = "LeftToRight" then startPoint.X + i else startPoint.X - i
                let pointY = if this.DrawDirectionY = "TopToBottom" then startPoint.Y + i else startPoint.Y - i
                points[i] <- new Point(pointX, pointY)
            
            points
   
    member val DrawDirectionX = "" with get, set
    member val DrawDirectionY = "" with get, set
    member val Start = startPoint with get, set
    member val End = endPoint with get, set
    member val Direction =  if startPoint.X <> endPoint.X && startPoint.Y <> endPoint.Y then "diagonal" else
                            if startPoint.X <> endPoint.X then "horizontal" else "vertical"

let Start() =
    printfn "== Solution 5 | Part Two == "
    printfn "At how many points do at least two lines overlap including diagonal?"
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let fileLines = File.ReadLines("Puzzle5\input.txt")

    //   Get lines from input
    let lines = fileLines
                      |> Seq.mapi (fun i s -> s.Split "->" |> Seq.map (fun p -> p.Trim()))
                      |> Seq.map (fun coordinates -> coordinates
                                                    |> Seq.map (fun coordinate -> coordinate.Split ",")
                                                    |> Seq.map (fun (coordinate:string[]) -> new Point(coordinate.[0] |> int, coordinate.[1] |> int))
                                                    |> Seq.toArray)
                      |> Seq.map (fun pointPair -> new Line(pointPair[0], pointPair[1]))
                      |> Seq.toArray
      
    // Setup grid size    
    let gridSize = (max (lines |> Seq.map(fun line -> max line.Start.Y line.End.Y) |> Seq.max) (lines |> Seq.map(fun line -> max line.Start.X line.End.X) |> Seq.max)) + 1 
    let floor : int [,] = Array2D.zeroCreate gridSize gridSize
    
    // Fill the sea floor with those hydrothermal vents
    for line in lines do
        let points = line.GetLinePoints()
        for i = 0 to (points.Length - 1) do
            floor[points[i].Y,points[i].X] <- floor[points[i].Y,points[i].X] + 1
    
    // Check for vents
    let mutable intersections = 0
    for i = 0 to (gridSize - 1) do
        floor[i,*] |> Array.iter(fun (value:int) ->
            if value > 1 then intersections <- intersections + 1)

    printfn $"  Answer:"
    printfn $"       Drawn lines: %A{lines.Length}"
    printfn $"       Floor size: %A{gridSize} x %A{gridSize}"
    printfn $"       Intersecting points: %A{intersections}"  
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms"
        