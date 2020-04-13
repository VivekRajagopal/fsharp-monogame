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
        Player.LoadContent player this.Content "paddle"
        Ball.LoadContent ball this.Content "ball"

    override this.Update (gameTime) =
        let frameData = GameUtil.getFrameData this gameTime

        GameExit.Update this frameData

        Player.Update player frameData
        Ball.Update ball frameData

        Collision.handlePlayerWithBall player ball

        base.Update(gameTime)

    override this.Draw gameTime =
        this.GraphicsDevice.Clear Color.Black

        spriteBatch.Begin()

        Player.Draw player spriteBatch gameTime
        Ball.Draw ball spriteBatch gameTime

        spriteBatch.End()
        base.Draw(gameTime)