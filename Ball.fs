namespace FSharpMonogame
open Microsoft.FSharp.Core.Operators.Unchecked
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Content
open Microsoft.Xna.Framework.Graphics
open GameUtil

type Ball = Ball of Sprite ref

module Ball =
  let private sprite = ref defaultof<Sprite>

  let Create () = Ball sprite

  let LoadContent (Ball sprite) (content: ContentManager) spriteName =
    sprite := {
      position = Vector2.Zero
      direction = Vector2(1.f, -1.f) |> Vector2.safeNormalise
      speed = 350.f
      texture = content.Load spriteName
      size = Point(32, 32)
      offset = Point(0, 0) }

  let private (|HitLeft|HitRight|HitTop|HitBottom|NoHit|) (minClamp: Vector2, maxClamp: Vector2, pos: Vector2) =
    match pos with
    | _ when pos.X <= minClamp.X -> HitLeft
    | _ when pos.X >= maxClamp.X -> HitRight
    | _ when pos.Y <= minClamp.Y -> HitTop
    | _ when pos.Y >= maxClamp.Y -> HitBottom
    | _ -> NoHit

  let private calculateCollision (minClamp: Vector2) (maxClamp: Vector2) (pos: Vector2) (dir: Vector2) =
    match (minClamp, maxClamp, pos) with
    | HitLeft | HitRight -> Vector2(-dir.X, dir.Y)
    | HitTop | HitBottom -> Vector2(dir.X, -dir.Y)
    | _ -> dir

  let Update (Ball sprite) frameData =
    let sprite' = !sprite

    let pos' = sprite'.position + sprite'.direction * sprite'.speed * float32 frameData.GameTime.ElapsedGameTime.TotalSeconds

    let ballSize = sprite'.size.ToVector2()

    let minClamp = Vector2.Zero
    let maxClamp = Vector2(float32 frameData.ViewportWidth, float32 frameData.ViewportHeight) - ballSize

    let dir' = calculateCollision minClamp maxClamp pos' sprite'.direction

    sprite := {
      sprite' with
        position = Vector2.Clamp(pos', minClamp, maxClamp) 
        direction =  dir' }

  let Draw (Ball sprite) (spriteBatch: SpriteBatch) _gameTime =
    Rendering.Draw spriteBatch !sprite