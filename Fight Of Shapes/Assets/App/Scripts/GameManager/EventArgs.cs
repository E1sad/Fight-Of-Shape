using System;

namespace SOG.Game_Manager{
  public class GameStateChangedEvent : EventArgs
  {
    //Internal varibales
    public GameState CurrentGameState{ get; private set; }
    public bool IsStatePauseBeforePlay { get; private set; }

    public GameStateChangedEvent(GameState gameState, bool isStatePauseBeforePlay){
      CurrentGameState = gameState;
      IsStatePauseBeforePlay = isStatePauseBeforePlay;
    }
  }
}
