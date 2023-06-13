namespace SOG.UI.Settings{
  public static class BackButtonPressedEvent{
    public static event System.Action<bool> OnBackButtonPressedEvent;
    public static void Raise(bool isFromMenu) { OnBackButtonPressedEvent?.Invoke(isFromMenu);}
  }
}
