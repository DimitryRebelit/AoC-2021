module AoC2021.Puzzle9.PartTwo

open System.IO

type Point(r: int, c: int, value: int) =
    member val r = r with get, set
    member val c = c with get, set
    member val value = value with get, set

let neighbors r c (A: 'a [,]) =
    [ if r > 0 then yield A.[r - 1, c]
      if r < Array2D.length1 A - 1 then
          yield A.[r + 1, c]
      if c > 0 then yield A.[r, c - 1]
      if c < Array2D.length2 A - 1 then
          yield A.[r, c + 1] ]

let rec searchBasin
    (
        r: int,
        c: int,
        field: int [,],
        direction: string,
        visited: ResizeArray<Point>
    ) : ResizeArray<int> =

    let result = new ResizeArray<int>()
    let point = new Point(r, c, field.[r, c])


    if (visited
        |> Seq.exists (fun x -> x.c = point.c && x.r = point.r)) then
        result
    else
        visited.Add(point)
        result.Add(point.value)

        if (r > 0 && direction <> "down") then
            let neighbour = field.[r - 1, c]

            if neighbour < 9 then
                result.AddRange(searchBasin (r - 1, c, field, "up", visited))

        if r < Array2D.length1 field - 1 && direction <> "up" then
            let neighbour = field.[r + 1, c]

            if neighbour < 9 then
                result.AddRange(searchBasin (r + 1, c, field, "down", visited))

        if c > 0 && direction <> "right" then
            let neighbour = field.[r, c - 1]

            if neighbour < 9 then
                result.AddRange(searchBasin (r, c - 1, field, "left", visited))

        if c < Array2D.length2 field - 1
           && direction <> "left" then
            let neighbour = field.[r, c + 1]

            if neighbour < 9 then
                result.AddRange(searchBasin (r, c + 1, field, "right", visited))

        result


let Start () =
    printfn "== Solution 9 | Part Two == "
    printfn "What do you get if you multiply together the sizes of the three largest basins?"
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

    let lowPoints = new ResizeArray<Point>()

    let columns = (oceanFloor |> Array2D.length1) - 1
    let rows = (oceanFloor |> Array2D.length2) - 1

    for column = 0 to columns do
        for row = 0 to rows do
            let target = oceanFloor[column,row]

            let h =
                neighbors column row oceanFloor |> Seq.toArray

            let lowPoint = h |> Array.forall (fun x -> x > target)

            if lowPoint then
                let lw = new Point(column, row, target)
                lowPoints.Add(lw)
    let basins =
        lowPoints
        |> Seq.map
            (fun lowpoint ->
                searchBasin (lowpoint.r, lowpoint.c, oceanFloor, "", ResizeArray<Point>())
                |> Seq.length)
        |> Seq.sortDescending
        |> Seq.take 3
        |> Seq.toArray
        |> fun c -> c[0] * c[1] * c[2]

    printfn $"  Answer:"
    printfn $"       Multiply of 3 biggest basins : %A{basins}"
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms"
