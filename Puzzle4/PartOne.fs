module AoC2021.Puzzle4.PartOne

open System
open System.IO

type BingoNumber (number : int, mark : bool) =
    member val Mark = mark with get, set
    member val Number = number with get, set
    
let splitStringInBingoValues(content : string, delimiter : char) : BingoNumber list =
    content
        |> fun s -> s.Split delimiter |> Array.toList
        |> List.map (fun s -> s.TrimStart())
        |> List.filter (fun s -> s <> "")
        |> List.map int
        |> List.map (fun x -> new BingoNumber(x, false))
        
let splitStringInNumbers(content : string, delimiter : char) : int[] =
    content
        |> fun s -> s.Split delimiter
        |> Seq.map (fun s -> s.TrimStart())
        |> Seq.filter (fun s -> s <> "")
        |> Seq.map int
        |> Seq.toArray

let Start () =
    printfn "== Solution 4 | Part One == "
    printfn "To guarantee victory against the giant squid, figure out which board will win first. What will your final score be if you choose that board?"
    
    let mutable stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let fileLines = File.ReadLines("Puzzle4\input.txt")
    let bingoCardSize = 5
    
    let bingoNumbers = fileLines
                            |> Seq.head
                            |> fun x -> splitStringInNumbers(x, ',')
                           
    let mutable bingoCards = fileLines
                            |> Seq.toList
                            |> List.skip 1
                            |> List.chunkBySize (bingoCardSize + 1)
                            |> List.mapi (fun index x -> x
                                                        |> List.filter(fun y -> y <> "")
                                                        |> List.map (fun y -> splitStringInBingoValues(y, ' ')))
                            
    let mutable BINGO = false
    let mutable oldLadyIndex = 0
    let mutable cardNumberBingo = 0
    let mutable lastNumber = 0

    while BINGO <> true && oldLadyIndex <> bingoNumbers.Length do
        let bingoNumber = bingoNumbers.[oldLadyIndex]
        let mutable cardNumber = 0;
        for card in bingoCards do
            // Set the number marked
            for row in card do
                for number in row do
                    if number.Number = bingoNumber then number.Mark <- true
            
                let bingoOnRow = row |> List.forall(fun num -> num.Mark)
                if bingoOnRow then
                    lastNumber <- bingoNumber
                    cardNumberBingo <- cardNumber
                    BINGO <- true
            
            for column in 0..(bingoCardSize - 1) do
                let bingoOnColum = card
                                    |> List.map (fun x -> x.[column])
                                    |> List.forall(fun num -> num.Mark)
                if bingoOnColum then
                    lastNumber <- bingoNumber
                    cardNumberBingo <- cardNumber
                    BINGO <- true
            
            cardNumber <- cardNumber + 1
        oldLadyIndex <- oldLadyIndex + 1
        
       
    let sumOfUnmarkedNumbers =  bingoCards.[cardNumberBingo]
                                |> List.map (fun row -> row |> List.filter(fun n -> n.Mark = false) |> List.map(fun n -> n.Number) |> List.sum)
                                |> List.sum 

    printfn $"  Answer:"
    printfn $"       Winning card: %A{cardNumberBingo}"
    printfn $"       Sum unmarked numbers: %A{sumOfUnmarkedNumbers}"
    printfn $"       Last called number: %A{lastNumber}" 
    printfn $"       Sum * Last: %A{sumOfUnmarkedNumbers * lastNumber}" 
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms" 