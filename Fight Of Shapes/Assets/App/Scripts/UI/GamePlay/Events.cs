namespace SOG.UI.GamePlay{
  public static class PauseButtonPressedEvent{
    public static event System.Action OnPauseButtonPressedEvent;
    public static void Raise() { OnPauseButtonPressedEvent?.Invoke();}
  }
  public static class AddScoreEvent{
    public static event System.Action<int> EventAddScore;
    public static void Raise(int score) { EventAddScore?.Invoke(score);}
  }
  public static class DamagedPlayerHealhtEvent{
    public static event System.Action<int> EventDamagedPlayerHealth;
    public static void Raise(int damage) { EventDamagedPlayerHealth?.Invoke(damage);}
  }
}
