using System;

namespace SOG.Game_Manager
{
  public static class GameStateEvents
  {
    public static event EventHandler<GameStateChangedEvent> OnGameStateChanged;

    public static void Raise(object sender, GameState gameState, bool isStatePauseBeforePlay)
    {
      OnGameStateChanged?.Invoke(sender, new GameStateChangedEvent(gameState, isStatePauseBeforePlay));
    }
  }
}
