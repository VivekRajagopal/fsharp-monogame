namespace FSharpMonogame

open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open Microsoft.Xna.Framework
open GameUtil
open Microsoft.Xna.Framework.Content
open Microsoft.FSharp.Core.Operators.Unchecked

type Player = Player of Sprite ref
module Player =
  let private sprite = ref defaultof<Sprite>

  let Create () = Player sprite

  let LoadContent (Player sprite) (content: ContentManager) spriteName =
    sprite := {
      position = Vector2.Zero
      speed = 250.f
      texture = content.Load spriteName
      size = Point(64, 64)
      offset = Point(0, 128)
    }

  let Update (Player sprite) frameData =
    let movementVector = Keyboard.GetState() |> Input.movementMapping |> Vector2.safeNormalise
    let pos' = (!sprite).position + movementVector * (!sprite).speed * float32 frameData.GameTime.ElapsedGameTime.TotalSeconds

    let playerSize = (!sprite).size.ToVector2()

    let minClamp = Vector2.Zero - playerSize * 0.5f
    
    let maxClamp = Vector2(float32 frameData.ViewportWidth, float32 frameData.ViewportHeight) - playerSize * 0.5f

    sprite := { !sprite with position = Vector2.Clamp(pos', minClamp, maxClamp) }

  let Draw (Player sprite) (spriteBatch: SpriteBatch) _gameTime =
    Sprite.draw !sprite spriteBatch true