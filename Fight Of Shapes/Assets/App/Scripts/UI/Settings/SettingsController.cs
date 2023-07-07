using UnityEngine;
using SOG.UI.MainMenu;

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
    #endregion

    #region Unity's Methods
    private void OnEnable(){
      SettingsButtonPressedEvent.OnSettingsButtonPressedEvent += SettingsButtonPressedEventHandler;
    }
    private void OnDisable(){
      SettingsButtonPressedEvent.OnSettingsButtonPressedEvent -= SettingsButtonPressedEventHandler;
    }
    private void Start(){ view.SetIsSoundOn(true); view.SetIsMusicOn(true);}
    #endregion
  }
}
