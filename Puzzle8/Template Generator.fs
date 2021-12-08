module AoC2021.Puzzle8.Template_Generator

open AoC2021.Puzzle8.Models.Display

let GenerateDisplayTemplate(input : string[]) : Display = 
        let template = new Display()

        let numberOne =
            input
            |> Seq.filter (fun s -> s.Length = 2)
            |> Seq.head
            |> Seq.toList

        let numberSeven =
            input
            |> Seq.filter (fun s -> s.Length = 3)
            |> Seq.head
            |> Seq.toList

        // After seven we know the top value for our template (result in dd)
        template.Segments.[0].Identifier <-
            numberSeven
            |> Seq.filter (fun c -> List.contains c numberOne <> true)
            |> Seq.head

        // For further reduction we need the number four (because that's unique) (either top left, middle) (eee,fff)
        let numberFour =
            input
            |> Seq.filter (fun s -> s.Length = 4)
            |> Seq.head
            |> Seq.toList
            |> Seq.filter (fun c -> List.contains c numberOne <> true)
            |> Seq.toList

        let mutable check =
            numberFour @ numberSeven
            |> Seq.distinct
            |> Seq.toList

        let numberThree =
            input
            // Filter out the results that aren't 5
            |> Seq.filter (fun s -> s.Length = 5)
            // Filter out everything that doesnt not include (number 4, number 7 or number 1)
            |> Seq.filter
                (fun s ->
                    let values = s |> Seq.map char
                    let mutable pass = 0
                    let mutable two = 0

                    values
                    |> Seq.iter
                        (fun x ->
                            if List.contains x check <> true then
                                pass <- pass + 1

                            if List.contains x numberFour then
                                two <- two + 1)

                    pass = 1 && two = 1

                    )
            |> Seq.head
            |> Seq.map char
            |> Seq.toList

        
            // [0] = top
        // [1] = upper left
        // [2] = upper right
        // [3] = middle
        // [4] = lower left
        // [5] = lower right
        // [6] = bottom
        
        // We can now get the middle and bottom
        template.Segments[3].Identifier <-
            numberThree
            |> Seq.filter (fun x -> List.contains x numberFour)
            |> Seq.head

        template.Segments[6].Identifier <-
            numberThree
            |> Seq.filter (fun x -> List.contains x check <> true)
            |> Seq.head

        check <-
            numberThree @ numberFour
            |> Seq.distinct
            |> Seq.filter (fun c -> List.contains c numberOne <> true)
            |> Seq.toList

        let numberFive =
            input
            // Filter out the results that aren't 5
            |> Seq.filter (fun s -> s.Length = 5)
            // Filter out everything that doesnt not include (number 3, number 4 or either one of number 1)
            |> Seq.filter
                (fun s ->
                    let values = s |> Seq.map char
                    let mutable pass = 0
                    let mutable two = 0

                    values
                    |> Seq.iter
                        (fun x ->
                            if List.contains x check then
                                pass <- pass + 1

                            if List.contains x numberOne then
                                two <- two + 1)

                    pass = 4 && two = 1)
            |> Seq.head
            |> Seq.map char
            |> Seq.toList


        template.Segments[5].Identifier <-
            numberFive
            |> Seq.filter (fun x -> List.contains x numberOne)
            |> Seq.head

        template.Segments[1].Identifier <-
            numberFive
            |> Seq.filter (fun x -> List.contains x numberOne <> true)
            |> Seq.filter (fun x -> List.contains x numberThree <> true)
            |> Seq.head

        template.Segments[2].Identifier <-
            numberOne
            |> Seq.filter (fun x -> List.contains x numberFive <> true)
            |> Seq.head

        check <-
            numberThree @ numberFive
            |> Seq.distinct
            |> Seq.toList

        template.Segments[4].Identifier <-
            input
            |> Seq.filter (fun s -> s.Length = 7)
            |> Seq.head
            |> Seq.map char
            |> Seq.filter (fun c -> List.contains c check <> true)
            |> Seq.head
            
        template