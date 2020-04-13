namespace FSharpMonogame
open Microsoft.Xna.Framework

module Vector2 =
  let safeNormalise vec =
    if vec <> Vector2.Zero then vec.Normalize()
    vec

  let fromCoordinate x y = Vector2(float32 x, float32 y)

  let fromPoint (point: Point) = Vector2(float32 point.X, float32 point.Y)
  let toPoint (vector: Vector2) = Point(int vector.X, int vector.Y)

  let flipX (vector: Vector2) = Vector2(-vector.X, vector.Y)
  let flipY (vector: Vector2) = Vector2(vector.X, -vector.Y)