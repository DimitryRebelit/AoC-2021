module AoC2021.Puzzle8.Models.Display

open AoC2021.Puzzle8.Models.Segment

type Display() =
    member val Segments : DisplaySegment[] = [| for i in 0 .. 6 -> new DisplaySegment() |]
    member this.Value : int =
        if this.Segments[0].On &&
           this.Segments[1].On &&
           this.Segments[2].On &&
           this.Segments[3].On <> true
           && this.Segments[4].On
           && this.Segments[5].On
           && this.Segments[6].On then
            0
        else if this.Segments[0].On <> true &&
                this.Segments[1].On <> true &&
                this.Segments[2].On &&
                this.Segments[3].On <> true &&
                this.Segments[4].On <> true &&
                this.Segments[5].On &&
                this.Segments[6].On <> true
                then
            1
        else if this.Segments[0].On &&
                this.Segments[1].On <> true &&
                this.Segments[2].On &&
                this.Segments[3].On &&
                this.Segments[4].On &&
                this.Segments[5].On <> true &&
                this.Segments[6].On
                then
            2
        else if this.Segments[0].On &&
                this.Segments[1].On <> true &&
                this.Segments[2].On &&
                this.Segments[3].On &&
                this.Segments[4].On <> true &&
                this.Segments[5].On &&
                this.Segments[6].On then
            3
        else if this.Segments[0].On <> true &&
                this.Segments[1].On &&
                this.Segments[2].On &&
                this.Segments[3].On &&
                this.Segments[4].On <> true &&
                this.Segments[5].On &&
                this.Segments[6].On <> true
                then
            4
        else if this.Segments[0].On &&
                this.Segments[1].On &&
                this.Segments[2].On <> true &&
                this.Segments[3].On &&
                this.Segments[4].On <> true &&
                this.Segments[5].On &&
                this.Segments[6].On
                then
            5
        else if this.Segments[0].On &&
                this.Segments[1].On &&
                this.Segments[2].On <> true &&
                this.Segments[3].On &&
                this.Segments[4].On &&
                this.Segments[5].On &&
                this.Segments[6].On then
            6
        else if this.Segments[0].On  &&
                this.Segments[1].On <> true &&
                this.Segments[2].On &&
                this.Segments[3].On <> true &&
                this.Segments[4].On <> true &&
                this.Segments[5].On &&
                this.Segments[6].On <> true
                then
            7
        else if this.Segments[0].On &&
                this.Segments[1].On &&
                this.Segments[2].On &&
                this.Segments[3].On &&
                this.Segments[4].On <> true &&
                this.Segments[5].On &&
                this.Segments[6].On then
            9
        else
            8

    member this.Populate(values : char[]) : unit =
        this.Segments |> Array.iter(fun seg -> seg.On <- false)
        values |> Array.iter(fun c -> this.Segments |> Seq.filter(fun s -> s.Identifier = c) |> Seq.head |> fun segment -> segment.On <- true)
            
    member this.Clone() = this.MemberwiseClone() :?> Display