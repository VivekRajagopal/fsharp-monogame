namespace FSharpMonogame

open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open Microsoft.Xna.Framework
open GameUtil

type Player = Player of Sprite ref
module Player =
  let Update (Player sprite) frameData =
    let movementVector = Keyboard.GetState() |> Input.movementMapping |> Vector2.safeNormalise
    let pos' = (!sprite).position + movementVector * (!sprite).speed * float32 frameData.GameTime.ElapsedGameTime.TotalSeconds

    let playerSize = (!sprite).size.ToVector2()

    let minClamp = Vector2.Zero - playerSize * 0.5f
    
    let maxClamp = Vector2(float32 frameData.ViewportWidth, float32 frameData.ViewportHeight) - playerSize * 0.5f

    sprite := { !sprite with position = Vector2.Clamp(pos', minClamp, maxClamp) }

  let Draw (Player sprite) (spriteBatch: SpriteBatch) _gameTime =
    Sprite.draw !sprite spriteBatch true