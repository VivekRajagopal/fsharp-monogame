namespace FSharpMonogame

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.FSharp.Core.Operators.Unchecked

type FSGame () as this =
    inherit Game()

    let graphics = new GraphicsDeviceManager(this)
    let mutable spriteBatch = defaultof<_>
    let player = Player.Create()

    do
        this.Content.RootDirectory <- "Content"
        this.IsMouseVisible <- true

    override this.Initialize() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)
        base.Initialize()

    override this.LoadContent() =
        Player.LoadContent player this.Content "skeleton"

    override this.Update (gameTime) =
        let frameData = GameUtil.getFrameData this gameTime

        GameExit.Update this frameData
        Player.Update player frameData

        base.Update(gameTime)

    override this.Draw gameTime =
        this.GraphicsDevice.Clear Color.Black

        spriteBatch.Begin()

        Player.Draw player spriteBatch gameTime

        spriteBatch.End()
        base.Draw(gameTime)