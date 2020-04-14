namespace FSharpMonogame
open Microsoft.Xna.Framework
open GameUtil

type AppState = {
  GameState: GameState
  PlayerScore: int
  EnemyScore: int
  BallState: BallState
  FrameData: FrameData
}

and GameState =
  | Playing
  | GameWon

and BallState =
  | Respawn
  | InPlay
  | ScoreToPlayer
  | ScoreToEnemy

module AppState =
  let create = {
    GameState = Playing
    PlayerScore = 0
    EnemyScore = 0
    BallState = InPlay
    FrameData = Unchecked.defaultof<FrameData>
  }

  let scorePlayer appState = {
    appState with
      BallState = Respawn
      PlayerScore = appState.PlayerScore + 1 }

  let scoreEnemy appState = {
    appState with
      BallState = Respawn
      EnemyScore = appState.EnemyScore + 1 }