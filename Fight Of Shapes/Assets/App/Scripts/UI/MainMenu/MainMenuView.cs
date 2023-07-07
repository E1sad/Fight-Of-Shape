using SOG.Audio_Manager;
using UnityEngine;

namespace SOG.UI.MainMenu{
  public class MainMenuView : MonoBehaviour{
    [Header("Variables")]
    [SerializeField] private AudioClip _buttonClip;

    [Header("Links")]
    [SerializeField] private MainMenuController controller;

    //Internal varibales

    #region My Methods
    public void OnPlayButtonPressed(){
      controller.OnPlayButtonPressed(); AudioManager.Instance.PlaySoundClip(_buttonClip);
    }
    public void OnShopMenuButtonpressed(){
      controller.OnShopMenuButtonpressed(); AudioManager.Instance.PlaySoundClip(_buttonClip);
    }
    public void OnSettingsButtonPressed(){
      controller.OnSettingsButtonPressed(); AudioManager.Instance.PlaySoundClip(_buttonClip);
    }
    public void OnCreditsButtonPressed(){ 
      controller.OnCreditsButtonPressed(); AudioManager.Instance.PlaySoundClip(_buttonClip);
    }
    #endregion

    #region Unity's Methods

    #endregion
  }
}