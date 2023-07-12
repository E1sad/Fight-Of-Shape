using System;

namespace SOG.SaveManager{
  public static class SendDataToObjects{
    public delegate void SendDataToObjectsDelegates
      (int[] upgradeLevel,int bestScore, int money, int HardnessLevel, 
      bool isMusicOn, bool isSoundOn, bool isFirstTime);
    public static event SendDataToObjectsDelegates SendDataToObjectsEvent;
    public static void Raise(int[] upgradeLevel, int bestScore, int money, int HardnessLevel, 
      bool isMusicOn, bool isSoundOn, bool isFirstTime) {
      SendDataToObjectsEvent?.Invoke
        (upgradeLevel, bestScore, money, HardnessLevel, isMusicOn, isSoundOn, isFirstTime);
    }
  }
  public static class SaveHardnessLevel {
    public static event EventHandler<int> SaveHardnessLevelEvent;
    public static void Raise(object sender,int hardnessLevel){
      SaveHardnessLevelEvent?.Invoke(sender,hardnessLevel);
    }
  }
  public static class SaveMoney {
    public static event Action<int> SaveMoneyEvent;
    public static void Raise(int money) { SaveMoneyEvent?.Invoke(money); }
  }
  public static class SaveBestScore{
    public static event Action<int> SaveBestScoreEvent;
    public static void Raise(int bestScore) { SaveBestScoreEvent?.Invoke(bestScore); }
  }
  public class SaveShopUpgradesEventArgs : EventArgs{
    public int[] IndexOfUpgradeLevels;
    public SaveShopUpgradesEventArgs(int[] upgradeLevels) { IndexOfUpgradeLevels = upgradeLevels; }
  }
  public static class SaveShopUpgrades {
    public delegate void SaveShopUpgradesDelegate(SaveShopUpgradesEventArgs eventArgs);
    public static event SaveShopUpgradesDelegate SaveShopUpgradesEvent;
    public static void Raise(SaveShopUpgradesEventArgs eventArgs) {
      SaveShopUpgradesEvent?.Invoke(eventArgs);
    }
  }
  public static class SaveMusicSettings{
    public static event Action<bool,bool> SaveMusicSettingsEvent;
    public static void Raise(bool isMusicOn, bool isSoundOn) { 
      SaveMusicSettingsEvent?.Invoke(isMusicOn, isSoundOn); 
    }
  }
  public static class IsItFirstTime{
    public static event Action<bool> IsItFirstTimeEvent;
    public static void Raise(bool isFirstTime){
      IsItFirstTimeEvent?.Invoke(isFirstTime);
    }
  }
  public static class DidGoRight{
    public static event Action DidGoRightEvent;
    public static void Raise(){ DidGoRightEvent?.Invoke();}
  }
  public static class DidGoLeft{
    public static event Action DidGoLeftEvent;
    public static void Raise() { DidGoLeftEvent?.Invoke(); }
  }
}
