namespace FSharpMonogame

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.FSharp.Core.Operators.Unchecked

type FSGame () as this =
    inherit Game()

    [<Literal>]
    let GameWidth = 500

    [<Literal>]
    let GameHeight = 400

    let graphics = new GraphicsDeviceManager(this)
    let mutable spriteBatch = defaultof<SpriteBatch>
    let player = Player.Create()
    let ball = Ball.Create()

    let appState = ref AppState.create

    override this.Initialize() =
        do
            this.Content.RootDirectory <- "Content"
            this.IsMouseVisible <- true
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)

        graphics.PreferredBackBufferWidth <- GameWidth
        graphics.PreferredBackBufferHeight <- GameHeight
        graphics.ApplyChanges()

        base.Initialize()

    override this.LoadContent() =
        Player.LoadContent player this.Content
        Ball.LoadContent ball this.Content
        Scoring.LoadContent this.Content

    override this.Update (gameTime) =
        let frameData = GameUtil.getFrameData this gameTime
        appState := { !appState with FrameData = frameData }

        GameExit.Update this frameData

        Player.Update player frameData
        Ball.Update ball appState

        Collision.handlePlayerWithBall player ball

        appState := Scoring.update !appState

        base.Update(gameTime)

    override this.Draw gameTime =
        this.GraphicsDevice.Clear Color.Black

        spriteBatch.Begin()

        Player.Draw player spriteBatch gameTime
        Ball.Draw ball spriteBatch gameTime
        Scoring.draw spriteBatch !appState

        spriteBatch.End()
        base.Draw(gameTime)