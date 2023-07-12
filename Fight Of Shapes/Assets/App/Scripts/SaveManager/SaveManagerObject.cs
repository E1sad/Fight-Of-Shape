using UnityEngine;

namespace SOG.SaveManager{
  public class SaveManagerObject : MonoBehaviour{
    /*[Header("Variables")]*/

    //[Header("Links")]

    //Internal varibales
    private Data _data;
    private int[] _indexOfUpgradeLevel = { 0, 0, 0 };
    private int _money = 0;
    private int _bestScore = 0;
    private int _hardnessLevel = 0;
    private bool _isMusicOn = true;
    private bool _isSoundOn = true;
    private bool _isFirstTime = false;

    #region My Methods
    private void Save() {
      Data data = new Data(_indexOfUpgradeLevel, _bestScore, _money, _hardnessLevel, _isMusicOn, _isSoundOn, _isFirstTime);
      SaveSytem.SaveData(data, Application.persistentDataPath + "/alma.armud"); }
    private void Load(){
      _data = SaveSytem.LoadData(Application.persistentDataPath + "/alma.armud");
      if (_data == null) { _data = new Data(new int[]{ 0, 0, 0 }, 0, 0, 0,true,true,true); }
      SendDataToObjects.Raise(_data.UpgradeLevels, _data.BestScore, _data.Money, _data.HardnessLevel,
        _data.IsMusicOn,_data.IsSoundOn,_data.IsFirstTime);
    }
    private void saveHardnessLevelEventHandler(object sender, int hardnessLevel) {
      /*_data.HardnessLevel*/
      _hardnessLevel = hardnessLevel; Save();
    }
    private void saveMoneyEventHandler(int money) { /*_data.Money */_money = money; Save(); }
    private void saveBestScoreEventHandler(int bestScore) { /*_data.BestScore*/_bestScore = bestScore; Save(); }
    private void saveShopUpgradesEventHandler(SaveShopUpgradesEventArgs eventArgs) {
      /*_data.UpgradeLevels*/
      _indexOfUpgradeLevel = eventArgs.IndexOfUpgradeLevels; Save();
    }
    private void saveMusicSettingsEventHandler(bool isMusicOn, bool isSoundOn) {
      _isMusicOn = isMusicOn; _isSoundOn = isSoundOn; Save();
    }
    #endregion

    #region Unity's Methods
    private void Start(){Load();}
    private void OnEnable(){
      SaveHardnessLevel.SaveHardnessLevelEvent += saveHardnessLevelEventHandler;
      SaveMoney.SaveMoneyEvent += saveMoneyEventHandler;
      SaveBestScore.SaveBestScoreEvent += saveBestScoreEventHandler;
      SaveShopUpgrades.SaveShopUpgradesEvent += saveShopUpgradesEventHandler;
      SaveMusicSettings.SaveMusicSettingsEvent += saveMusicSettingsEventHandler;
    }
    private void OnDisable(){
      SaveHardnessLevel.SaveHardnessLevelEvent -= saveHardnessLevelEventHandler;
      SaveMoney.SaveMoneyEvent -= saveMoneyEventHandler;
      SaveBestScore.SaveBestScoreEvent -= saveBestScoreEventHandler;
      SaveShopUpgrades.SaveShopUpgradesEvent -= saveShopUpgradesEventHandler;
      SaveMusicSettings.SaveMusicSettingsEvent -= saveMusicSettingsEventHandler;
    }
    #endregion
  }
}
