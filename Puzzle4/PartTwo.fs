module AoC2021.Puzzle4.PartTwo
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
    printfn "== Solution 4 | Part Two == "
    printfn "Figure out which board will win last. Once it wins, what would its final score be?"
    
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
    let mutable cardHadBingo = Seq.empty
    let mutable lastCardNumberBingo = 0
    let mutable lastNumber = 0
    while BINGO <> true && oldLadyIndex <> bingoNumbers.Length do
        let bingoNumber = bingoNumbers.[oldLadyIndex]
        let mutable cardNumber = 0
        
        for card in bingoCards do
            if (cardHadBingo |> Seq.contains cardNumber) <> true then
                for row in card do
                    for number in row do
                        if number.Number = bingoNumber then number.Mark <- true
                
                    let bingoOnRow = row |> List.forall(fun num -> num.Mark)
                    if bingoOnRow then
                        lastNumber <- bingoNumber
                        lastCardNumberBingo <- cardNumber
                        cardHadBingo <- Seq.append cardHadBingo [cardNumber]
                
                for column in 0..(bingoCardSize - 1) do
                    let bingoOnColum = card
                                        |> List.map (fun x -> x.[column])
                                        |> List.forall(fun num -> num.Mark)
                    if bingoOnColum then
                        lastNumber <- bingoNumber
                        lastCardNumberBingo <- cardNumber
                        cardHadBingo <- Seq.append cardHadBingo [cardNumber]
            
            cardNumber <- cardNumber + 1
            
        oldLadyIndex <- oldLadyIndex + 1
        
        if lastNumber = bingoCards.Length then
            BINGO <- true
       
    let sumOfUnmarkedNumbers =  bingoCards.[lastCardNumberBingo]
                                |> List.map (fun row -> row |> List.filter(fun n -> n.Mark = false) |> List.map(fun n -> n.Number) |> List.sum)
                                |> List.sum 
    printfn $"  Answer:"
    printfn $"       Last winning card: %A{lastCardNumberBingo}"
    printfn $"       Sum unmarked numbers: %A{sumOfUnmarkedNumbers}"
    printfn $"       Last called number: %A{lastNumber}" 
    printfn $"       Sum * Last: %A{sumOfUnmarkedNumbers * lastNumber}" 
    printfn $"  Duration: %f{stopWatch.Elapsed.TotalMilliseconds} ms"