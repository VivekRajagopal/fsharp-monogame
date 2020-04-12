namespace FSharpMonogame
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input

module GameExit =
  let Update (game: Game) _frameData =
    if (GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
    then game.Exit()