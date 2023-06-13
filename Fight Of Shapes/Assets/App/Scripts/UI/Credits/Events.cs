namespace SOG.UI.Credits{
  public static class BackButtonPressedEvent{
    public static event System.Action OnBackButtonPressedEvent;
    public static void Raise() { OnBackButtonPressedEvent?.Invoke(); }
  }
}
