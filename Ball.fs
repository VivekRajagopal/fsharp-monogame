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

  let LoadContent (Ball sprite) (content: ContentManager) =
    sprite := {
      position = Vector2.Zero
      direction = Vector2(1.f, -1.f) |> Vector2.safeNormalise
      speed = 320.f
      texture = content.Load "ball"
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
    | HitLeft | HitRight -> Vector2(-dir.X, dir.Y), InPlay
    | HitTop -> Vector2(dir.X, -dir.Y), ScoreToEnemy
    | HitBottom -> Vector2(dir.X, -dir.Y), ScoreToPlayer
    | _ -> dir, InPlay

  let spawnSprite appState sprite =
    { appState with BallState = InPlay },
    { sprite with
        position = Vector2.fromCoordinate (appState.FrameData.ViewportWidth / 2) (appState.FrameData.ViewportHeight / 2) }

  let moveSprite appState sprite =
    let pos = sprite.position + sprite.direction * sprite.speed * float32 appState.FrameData.GameTime.ElapsedGameTime.TotalSeconds
    let ballSize = sprite.size.ToVector2()

    let minClamp = Vector2.Zero
    let maxClamp = Vector2(float32 appState.FrameData.ViewportWidth, float32 appState.FrameData.ViewportHeight) - ballSize

    let dir', newBallState = calculateCollision minClamp maxClamp pos sprite.direction

    { appState with BallState = newBallState },
    { sprite with
        position = Vector2.Clamp(pos, minClamp, maxClamp) 
        direction =  dir' }

  let Update (Ball sprite) (appState: AppState ref) =
    let appState', sprite' =
      match (!appState).BallState with
      | Respawn -> spawnSprite !appState !sprite
      | _ -> moveSprite !appState !sprite

    appState := appState'
    sprite := sprite'

  let Draw (Ball sprite) (spriteBatch: SpriteBatch) _gameTime =
    Rendering.Draw spriteBatch !sprite