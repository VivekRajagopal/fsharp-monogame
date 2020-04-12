namespace FSharpMonogame
open Microsoft.Xna.Framework

module Vector2 =
  let safeNormalise vec =
    if vec <> Vector2.Zero then vec.Normalize()
    vec