namespace FSharpMonogame

module Program = 

    [<EntryPoint>]
    let main _argv =
        
        use game = new FSGame()
        game.Run()

        0
