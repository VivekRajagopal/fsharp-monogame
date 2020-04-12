namespace FSharpMonogame
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open System

type Sprite = {
  position: Vector2
  speed: float32
  texture: Texture2D
  size: Point
  offset: Point 
}

module Sprite =
  let draw sprite (spriteBatch: SpriteBatch) _withWireframe =
    let sourceRect = Rectangle(sprite.offset, sprite.size)
    spriteBatch.Draw(sprite.texture, sprite.position, Nullable.op_Implicit sourceRect, Color.White)

    (*
      // Drawing wireframe bounding box
      let state = new RasterizerState();
      state.FillMode <- FillMode.WireFrame;
      spriteBatch.GraphicsDevice.RasterizerState <- state;
      spriteBatch.Draw(sprite.texture, sprite.position, Nullable.op_Implicit sourceRect, Color.White)
    *)