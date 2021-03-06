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

  let LoadContent (Player sprite) (content: ContentManager) =
    sprite := {
      position = Vector2(150.f, 0.f)
      direction = Vector2.UnitY
      speed = 400.f
      texture = content.Load "paddle"
      size = Point(64, 16)
      offset = Point(0, 0) }

  let Update (Player sprite) frameData =
    let sprite' = !sprite

    let movementVector = Keyboard.GetState() |> Input.movementMapping |> Vector2.safeNormalise

    let pos' = sprite'.position + movementVector * sprite'.speed * float32 frameData.GameTime.ElapsedGameTime.TotalSeconds

    let playerSize = sprite'.size.ToVector2()

    let minClamp = Vector2.Zero
    
    let maxClamp = Vector2.fromCoordinate frameData.ViewportWidth frameData.ViewportHeight - playerSize

    sprite := { sprite' with position = Vector2.Clamp(pos', minClamp, maxClamp) }

  let Draw (Player sprite) (spriteBatch: SpriteBatch) _gameTime =
    Rendering.Draw spriteBatch !sprite