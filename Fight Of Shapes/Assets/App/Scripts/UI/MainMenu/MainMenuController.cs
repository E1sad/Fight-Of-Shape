using SOG.UI.Settings;
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
    #endregion

    #region Unity's Methods
    private void OnEnable(){
      SOG.UI.Settings.BackButtonPressedEvent.OnBackButtonPressedEvent += BackButtonPressedEventHandler;
      SOG.UI.Credits.BackButtonPressedEvent.OnBackButtonPressedEvent += BackButtonPressedFromCreditsEventHandler;
      SOG.UI.Pause.MainMenuButtonPressedEvent.OnMainMenuButtonPressedEvent += OnMainMenuButtonPressedEventHandler;
    }
    private void OnDisable(){
      SOG.UI.Settings.BackButtonPressedEvent.OnBackButtonPressedEvent -= BackButtonPressedEventHandler;
      SOG.UI.Credits.BackButtonPressedEvent.OnBackButtonPressedEvent -= BackButtonPressedFromCreditsEventHandler;
      SOG.UI.Pause.MainMenuButtonPressedEvent.OnMainMenuButtonPressedEvent -= OnMainMenuButtonPressedEventHandler;
    }
    #endregion
  }
}
