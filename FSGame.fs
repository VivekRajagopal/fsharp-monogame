namespace FSharpMonogame

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open Microsoft.FSharp.Core.Operators.Unchecked

type FSGame () as this =
    inherit Game()

    let graphics = new GraphicsDeviceManager(this)
    let mutable spriteBatch = defaultof<_>
    let mutable playerSpriteSheet = defaultof<Texture2D>
    let playerSprite = ref defaultof<Sprite> 
    let player = Player playerSprite

    do
        this.Content.RootDirectory <- "Content"
        this.IsMouseVisible <- true

    override this.Initialize() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)
        do base.Initialize()

    override this.LoadContent() =
        playerSpriteSheet <- this.Content.Load<Texture2D>("skeleton")
        playerSprite := {
            position = Vector2.Zero
            speed = 250.f
            texture = playerSpriteSheet
            size = Point(64, 64)
            offset = Point(0, 128) }

    override this.Update (gameTime) =
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        then this.Exit()

        let frameData = GameUtil.getFrameData this gameTime

        Player.Update player frameData

        base.Update(gameTime)

    override this.Draw gameTime =
        this.GraphicsDevice.Clear Color.Black

        spriteBatch.Begin()

        Player.Draw player spriteBatch gameTime

        spriteBatch.End()
        base.Draw(gameTime)