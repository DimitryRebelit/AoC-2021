module AoC2021.Puzzle8.Models.Segment

type DisplaySegment() =
    member val Identifier: char = ' ' with get, set
    member val On = false with get, set
    member this.Print = if this.On then this.Identifier else '.'