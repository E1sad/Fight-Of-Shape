namespace SOG.UI.Pause{
  public static class RestartButtonPressedEvent{
    public static event System.Action<bool> OnRestartButtonPressed;
    public static void Raise(bool isFromPause) { OnRestartButtonPressed?.Invoke(isFromPause);}
  }
  public static class MainMenuButtonPressedEvent{
    public static event System.Action OnMainMenuButtonPressedEvent;
    public static void Raise() { OnMainMenuButtonPressedEvent?.Invoke();}
  }
}
