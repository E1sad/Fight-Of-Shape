namespace SOG.SaveManager{
  [System.Serializable]
  public class Data{
    public int[] UpgradeLevels;
    public int BestScore;
    public int Money;
    public int HardnessLevel;
    public bool IsMusicOn;
    public bool IsSoundOn;
    public bool IsFirstTime;

    public Data(int[] upgradeLevels, int bestScore, int money, int hardnessLevel, 
      bool isMusicOn,bool isSoundOn, bool isFirstTime) {
      UpgradeLevels = upgradeLevels; BestScore = bestScore; Money = money; HardnessLevel = hardnessLevel;
      IsMusicOn = isMusicOn; IsSoundOn = isSoundOn; IsFirstTime = isFirstTime;
    }
  }
}
