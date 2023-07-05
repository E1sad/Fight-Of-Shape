using SOG.UI.Settings;
using SOG.UI.Shop;
using UnityEngine;

namespace SOG.UI.MainMenu{
  public class MainMenuController : MonoBehaviour{
    /*[Header("Variables")]*/
    [Header("Links")]
    [SerializeField] private MainMenuView view;

    //Internal varibales

    #region My Methods
    public void OnPlayButtonPressed(){
      PlayButtonPressedEvent.Raise();
      view.gameObject.SetActive(false);
    }
    public void OnShopMenuButtonpressed() {
      view.gameObject.SetActive(false);
      ShopButtonPressedEvent.Raise("MainMenu");
    }
    public void OnSettingsButtonPressed(){
      SettingsButtonPressedEvent.Raise(true,false);
      view.gameObject.SetActive(false);
    }
    public void OnCreditsButtonPressed(){ 
      CreditsButtonPressedEvent.Raise();
      view.gameObject.SetActive(false);
    }
    private void BackButtonPressedEventHandler(bool isFromMenu, bool isFromPause){
      if(isFromMenu) view.gameObject.SetActive(true);
    }
    private void BackButtonPressedFromCreditsEventHandler(){
      view.gameObject.SetActive(true);
    }
    private void OnMainMenuButtonPressedEventHandler(){
      view.gameObject.SetActive(true);
    }
    private void BackButtonFromShopPressedEventHandler(object sender, BackButtonFromShopEventArguments eventArgs){
      if (eventArgs.PreviousPage == "MainMenu") { view.gameObject.SetActive(true); }
    }
    #endregion

    #region Unity's Methods
    private void OnEnable(){
      SOG.UI.Settings.BackButtonPressedEvent.OnBackButtonPressedEvent += BackButtonPressedEventHandler;
      SOG.UI.Credits.BackButtonPressedEvent.OnBackButtonPressedEvent += BackButtonPressedFromCreditsEventHandler;
      SOG.UI.Pause.MainMenuButtonPressedEvent.OnMainMenuButtonPressedEvent += OnMainMenuButtonPressedEventHandler;
      Shop.BackButtonPressedEvent.ShopMenuBackButtonPressedEvent += BackButtonFromShopPressedEventHandler;
    }
    private void OnDisable(){
      SOG.UI.Settings.BackButtonPressedEvent.OnBackButtonPressedEvent -= BackButtonPressedEventHandler;
      SOG.UI.Credits.BackButtonPressedEvent.OnBackButtonPressedEvent -= BackButtonPressedFromCreditsEventHandler;
      SOG.UI.Pause.MainMenuButtonPressedEvent.OnMainMenuButtonPressedEvent -= OnMainMenuButtonPressedEventHandler;
      Shop.BackButtonPressedEvent.ShopMenuBackButtonPressedEvent -= BackButtonFromShopPressedEventHandler;
    }
    #endregion
  }
}
