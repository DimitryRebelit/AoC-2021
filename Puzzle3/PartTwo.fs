module AoC2021.Puzzle3.PartTwo

open System
open System.IO

let rec ExtractLifeSupportRating (data : seq<char[]>, bitSize : int, column : int, isCo2 : bool) = 
    let mutable oneCount = 0
    let mutable zeroCount = 0
    let mutable mostCommonCharacter = '1'
    
    data
        |> Seq.map (fun x -> x[column])
        |> Seq.iter (fun x -> if x = '1' then oneCount <- oneCount + 1 else zeroCount <- zeroCount + 1)
        
    if isCo2 then
        if oneCount < zeroCount then mostCommonCharacter <- '1' else mostCommonCharacter <- '0'
        if oneCount = zeroCount then mostCommonCharacter <- '0'
    else
        if oneCount > zeroCount then mostCommonCharacter <- '1' else mostCommonCharacter <- '0'
        if oneCount = zeroCount then mostCommonCharacter <- '1'
    
    let filteredData = data |> Seq.filter(fun x -> x[column] = mostCommonCharacter)
    let filteredDataAmount = filteredData |> Seq.length;
    
    match filteredDataAmount with
    | s when s = 1 -> filteredData
    | _ -> ExtractLifeSupportRating(filteredData, bitSize, (column + 1), isCo2)

let Start() =
    printfn "== Solution 3 | Part Two == "
    printfn "What is the life support rating of the submarine?"
    
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let fileLines = File.ReadLines("Puzzle3\input.txt") |> Seq.toArray |> Seq.map (Seq.toArray)
    let bitSize = fileLines |> Seq.head |> Seq.length
    
    let oxygenDataArray = ExtractLifeSupportRating(fileLines,bitSize,0,false) |> Seq.head
    let co2DataArray = ExtractLifeSupportRating(fileLines,bitSize,0,true) |> Seq.head
    
    let oxygenDecimal = Convert.ToInt32(new string(oxygenDataArray), 2)
    let co2Decimal = Convert.ToInt32(new string(co2DataArray), 2)
    
    printfn $"  Answer:"
    printfn $"       Oxygen byte: %A{oxygenDataArray}"
    printfn $"       Co2 byte: %A{co2DataArray}"
    printfn $"       Oxygen decimal: %A{oxygenDecimal}"      
    printfn $"       Co2 decimal: %A{co2Decimal}"
    printfn $"       Oxygen * Co2: %A{oxygenDecimal * co2Decimal}" 
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms"
