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

    #region My Methods
    public void BackButtonPressed(){
      view.gameObject.SetActive(false);
      BackButtonPressedEvent.Raise(_isFromMenu);
    }
    private void SettingsButtonPressedEventHandler(bool isFromMenu){
      view.gameObject.SetActive(true); _isFromMenu = isFromMenu;
    }
    #endregion

    #region Unity's Methods
    private void OnEnable(){
      SettingsButtonPressedEvent.OnSettingsButtonPressedEvent += SettingsButtonPressedEventHandler;
    }
    private void OnDisable(){
      SettingsButtonPressedEvent.OnSettingsButtonPressedEvent -= SettingsButtonPressedEventHandler;
    }
    #endregion
  }
}
