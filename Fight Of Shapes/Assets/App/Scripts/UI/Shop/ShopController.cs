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
    public void SetCoin(int coin) { _coin = coin; view.CoinChangesOnUI(_coin); }
    #endregion

    #region Unity's Methods
    private void Start(){
      SetCoin(9999); //Temporary. It should change when save system is implemented.
    }
    private void OnEnable(){
      ShopButtonPressedEvent.EventShopButtonPressed += shopButtonPressedEventHandler;
      AddCoinEvent.EventAddCoin += addCoinEventHandler;
    }
    private void OnDisable(){
      ShopButtonPressedEvent.EventShopButtonPressed -= shopButtonPressedEventHandler;
      AddCoinEvent.EventAddCoin -= addCoinEventHandler;
    }
    #endregion
  }
}
