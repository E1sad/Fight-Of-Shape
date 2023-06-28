namespace SOG.UI.Settings{
  public static class BackButtonPressedEvent{
    public static event System.Action<bool,bool> OnBackButtonPressedEvent;
    public static void Raise(bool isFromMenu, bool isFromPause) { 
      OnBackButtonPressedEvent?.Invoke(isFromMenu, isFromPause);}
  }
}
