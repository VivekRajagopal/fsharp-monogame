namespace FSharpMonogame

open Microsoft.Xna.Framework.Graphics
open System
open Microsoft.Xna.Framework

module Rendering =
  let mutable private texture = Unchecked.defaultof<Texture2D>
  let getTexture (spriteBatch: SpriteBatch) =
    if (isNull texture) then
      texture <- new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color)
      texture.SetData [|Color.White|]
    texture

  let Draw (spriteBatch: SpriteBatch) (sprite: Sprite) =
    spriteBatch.Draw(
      sprite.texture,
      sprite.position,
      Rectangle(sprite.offset, sprite.size) |> Nullable.op_Implicit,
      Color.White)
