namespace FSharpMonogame
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Content
open Microsoft.Xna.Framework

module Scoring =
  let private spriteFont = ref Unchecked.defaultof<SpriteFont>;
  let private gameWonTexture = ref Unchecked.defaultof<Texture2D>;

  let LoadContent (content: ContentManager) =
    spriteFont := content.Load "default"
    gameWonTexture := content.Load "game-won"

  let handleBallState appState =
    match appState.BallState with
    | ScoreToEnemy -> { appState with BallState = Respawn; EnemyScore = appState.EnemyScore + 1 }
    | ScoreToPlayer -> { appState with BallState = Respawn; PlayerScore = appState.PlayerScore + 1 }
    | _ -> appState

  let handleScore appState =
    match appState with
    | x when x.PlayerScore > 10 -> { appState with GameState = GameWon }
    | _ -> appState

  let update appState =
    appState
    |> handleBallState
    |> handleScore

  let drawScore (spriteBatch: SpriteBatch) appState =
    spriteBatch.DrawString(!spriteFont, sprintf "Player: %i" appState.PlayerScore, Vector2.Zero, Color.Orange)

  let drawGameWon (spriteBatch: SpriteBatch) _appState =
    spriteBatch.Draw(!gameWonTexture, Vector2.Zero, Color.White)

  let draw (spriteBatch: SpriteBatch) appState =
    match appState.GameState with
    | Playing -> drawScore spriteBatch appState
    | GameWon -> drawGameWon spriteBatch appState