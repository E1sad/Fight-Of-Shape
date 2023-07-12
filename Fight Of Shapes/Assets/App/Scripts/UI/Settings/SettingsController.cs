using UnityEngine;
using SOG.UI.MainMenu;
using SOG.SaveManager;

namespace SOG.UI.Settings
{
  public class SettingsController : MonoBehaviour
  {
    //[Header("Variables")]

    [Header("Links")]
    [SerializeField] private SettingsView view;
    //Internal varibales
    private bool _isFromMenu;
    private bool _isFromPause;

    #region My Methods
    public void BackButtonPressed(){
      view.gameObject.SetActive(false);
      BackButtonPressedEvent.Raise(_isFromMenu, _isFromPause);
    }
    private void SettingsButtonPressedEventHandler(bool isFromMenu,bool isFromPause){
      view.gameObject.SetActive(true); _isFromMenu = isFromMenu; _isFromPause = isFromPause;
    }
    private void sendDataToObjectsEventHandler(int[] upgradeLevel, int bestScore, int money, int HardnessLevel,
      bool isMusicOn, bool isSoundOn, bool isFirstTime){
      view.SetIsSoundOn(isSoundOn); view.SetIsMusicOn(isMusicOn);
    }
    #endregion

    #region Unity's Methods
    private void OnEnable(){
      SettingsButtonPressedEvent.OnSettingsButtonPressedEvent += SettingsButtonPressedEventHandler;
      SendDataToObjects.SendDataToObjectsEvent += sendDataToObjectsEventHandler;
    }
    private void OnDisable(){
      SettingsButtonPressedEvent.OnSettingsButtonPressedEvent -= SettingsButtonPressedEventHandler;
      SendDataToObjects.SendDataToObjectsEvent -= sendDataToObjectsEventHandler;
    }
    #endregion
  }
}
