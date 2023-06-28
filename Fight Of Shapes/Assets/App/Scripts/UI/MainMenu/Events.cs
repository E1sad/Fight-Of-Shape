using System;

namespace SOG.UI.MainMenu{
  public static class PlayButtonPressedEvent{
    public static event Action OnPlayButtonPressedEvent;
    public static void Raise(){OnPlayButtonPressedEvent?.Invoke();}
  }
  public static class SettingsButtonPressedEvent{
    public static event Action<bool,bool> OnSettingsButtonPressedEvent;
    public static void Raise(bool isFromMenu, bool isFromPause) { 
      OnSettingsButtonPressedEvent?.Invoke(isFromMenu,isFromPause); }
  }
  public static class CreditsButtonPressedEvent {
    public static event Action OnCreditsButtonPressed;
    public static void Raise(){ OnCreditsButtonPressed?.Invoke();}
  }
}
