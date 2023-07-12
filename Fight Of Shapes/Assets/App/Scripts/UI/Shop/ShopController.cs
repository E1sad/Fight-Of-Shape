using SOG.SaveManager;
using UnityEngine;

namespace SOG.UI.Shop{
  public class ShopController : MonoBehaviour{
    //[Header("Variables")]

    [Header("Links")]
    [SerializeField] private ShopView view;

    //Internal varibales
    private string _previousPage;
    private int _coin;

    #region My Methods
    public void OnBackButtonPressed(){
      BackButtonPressedEvent.Raise(this, new BackButtonFromShopEventArguments(_previousPage));
      view.gameObject.SetActive(false);
    }
    private void shopButtonPressedEventHandler(string fromWhere) {
      _previousPage = fromWhere; view.gameObject.SetActive(true); view.OnSecondUpgradeButtonPressed();
    }
    private void addCoinEventHandler(int addedCoinAmount) { 
      _coin += addedCoinAmount; view.CoinChangesOnUI(_coin); 
    }
    public int GetCoin() { return _coin; }
    public void SetCoin(int coin) { _coin = coin; view.CoinChangesOnUI(_coin); sendMoney(); }
    private void sendDataToObjectsEventHandler(int[] upgradeLevel, int bestScore, int money, int HardnessLevel,
      bool isMusicOn, bool isSoundOn, bool isFirstTime) {
      view.SetIndexOfUpgradeLevel(upgradeLevel); SetCoin(money);
    }
    public void SendUpgradeEvents(int index, int[] indexOfUpgradeLevel){
      switch (index){
        case 0: CriticalBulletChance.Raise(view.BulletCriticalChanceUpgrade[indexOfUpgradeLevel[0]]); break;
        case 1: PlayerStatsChanged.Raise(view.PlayerStats[indexOfUpgradeLevel[1]]); break;
        case 2: BulletShapeChanged.Raise(view.BulletStats[indexOfUpgradeLevel[2]]); break;
        default: break;}
      SaveShopUpgrades.Raise(new SaveShopUpgradesEventArgs(indexOfUpgradeLevel));
    }
    private void sendMoney(){SaveMoney.Raise(_coin);}
    #endregion

    #region Unity's Methods
    private void OnEnable(){
      ShopButtonPressedEvent.EventShopButtonPressed += shopButtonPressedEventHandler;
      AddCoinEvent.EventAddCoin += addCoinEventHandler;
      SendDataToObjects.SendDataToObjectsEvent += sendDataToObjectsEventHandler;
    }
    private void OnDisable(){
      ShopButtonPressedEvent.EventShopButtonPressed -= shopButtonPressedEventHandler;
      AddCoinEvent.EventAddCoin -= addCoinEventHandler;
      SendDataToObjects.SendDataToObjectsEvent -= sendDataToObjectsEventHandler;
    }
    #endregion
  }
}
