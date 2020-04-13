namespace FSharpMonogame
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type Sprite = {
  position: Vector2
  speed: float32
  direction: Vector2
  texture: Texture2D
  size: Point
  offset: Point
}

module Sprite =
  let getRect sprite =
    Rectangle(sprite.position |> Vector2.toPoint, sprite.size)