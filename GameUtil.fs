namespace FSharpMonogame
open Microsoft.Xna.Framework

module GameUtil =
  type FrameData = {
    ViewportWidth: int
    ViewportHeight: int
    GameTime: GameTime
  }

  let getFrameData (game: Game) gameTime =
    { ViewportWidth = game.GraphicsDevice.Viewport.Width
      ViewportHeight = game.GraphicsDevice.Viewport.Height
      GameTime = gameTime }