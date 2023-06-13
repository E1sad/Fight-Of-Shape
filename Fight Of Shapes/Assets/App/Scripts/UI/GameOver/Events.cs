using System;

namespace SOG.UI.GameOver{
  public static class GameOverEvent{
    public static event Action EventGameOver;
    public static void Raise() { EventGameOver?.Invoke(); }
  }
}
