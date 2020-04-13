namespace FSharpMonogame
open Microsoft.Xna.Framework

module Collision =
  let private (|HitLeft|HitRight|HitTop|HitBottom|NoHit|) (minClamp: Vector2, maxClamp: Vector2, pos: Vector2) =
    match pos with
    | _ when pos.X <= minClamp.X -> HitLeft
    | _ when pos.X >= maxClamp.X -> HitRight
    | _ when pos.Y <= minClamp.Y -> HitTop
    | _ when pos.Y >= maxClamp.Y -> HitBottom
    | _ -> NoHit

  let calculateCollision (minClamp: Vector2, maxClamp: Vector2) (pos: Vector2) (dir: Vector2) =
    match (minClamp, maxClamp, pos) with
    | HitLeft | HitRight -> Vector2.flipX dir
    | HitTop | HitBottom -> Vector2.flipY dir
    | _ -> dir

  let areSpritesColliding sprite1 sprite2 =
    let rect1 = Sprite.getRect sprite1
    let rect2 = Sprite.getRect sprite2
    rect1.Intersects rect2

  let handlePlayerWithBall (Player player) (Ball ball) =
    let playerSprite = !player
    let ballSprite = !ball

    let dir', pos' =
      match areSpritesColliding playerSprite ballSprite with
      | true ->
        Vector2.flipY ballSprite.direction,
        Vector2(ballSprite.position.X, playerSprite.size.Y |> float32)
      | false -> ballSprite.direction, ballSprite.position

    ball := {
      ballSprite with
        direction = dir'
        position = pos' }