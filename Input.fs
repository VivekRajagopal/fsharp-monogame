namespace FSharpMonogame
open Microsoft.Xna.Framework.Input
open Microsoft.Xna.Framework

module Input =
  let (|KeyDown|_|) k (state: KeyboardState) =
    if state.IsKeyDown k then Some() else None

  let movementMapping =
    function
    // | KeyDown Keys.W & KeyDown Keys.A -> Vector2(-1.f, -1.f)
    // | KeyDown Keys.W & KeyDown Keys.D -> Vector2(1.f, -1.f)
    // | KeyDown Keys.S & KeyDown Keys.A -> Vector2(-1.f, 1.f)
    // | KeyDown Keys.S & KeyDown Keys.D -> Vector2(1.f, 1.f)
    // | KeyDown Keys.W -> Vector2(0.f, -1.f)
    // | KeyDown Keys.S -> Vector2(0.f, 1.f)
    | KeyDown Keys.A -> Vector2(-1.f, 0.f)
    | KeyDown Keys.D -> Vector2(1.f, -0.f)
    | _ -> Vector2(0.f, 0.f)