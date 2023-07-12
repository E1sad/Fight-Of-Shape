using SOG.Audio_Manager;
using UnityEngine;
using UnityEngine.UI;

namespace SOG.UI.Settings
{
  public class SettingsView : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private AudioClip _buttonClip;

    [Header("Links")]
    [SerializeField] private SettingsController controller;
    [SerializeField] private Image _soundOff;
    [SerializeField] private Image _musicOff;

    //Internal varibales
    private bool _isSoundOn;
    private bool _isMusicOn;

    #region My Methods
    public void SetIsSoundOn (bool isOn) { 
      _isSoundOn = isOn; _soundOff.gameObject.SetActive(!_isSoundOn);
    }
    public void SetIsMusicOn (bool isOn) { 
      _isMusicOn = isOn; _musicOff.gameObject.SetActive(!_isMusicOn);
    }
    public void OnBackButtonPressedEvent() { 
      controller.BackButtonPressed(); AudioManager.Instance.PlaySoundClip(_buttonClip); 
    }
    public void OnSoundButtonClicked() {
      if (_isSoundOn) {
        _soundOff.gameObject.SetActive(true); AudioManager.Instance.SetSoundVolume(0); _isSoundOn = false;}
      else {
        _soundOff.gameObject.SetActive(false); AudioManager.Instance.SetSoundVolume(1); _isSoundOn = true;}
    }
    public void OnMusicButtonClicked() {
      if (_isMusicOn){
        _musicOff.gameObject.SetActive(true); AudioManager.Instance.SetMusicVolume(0); _isMusicOn = false;}
      else {
        _musicOff.gameObject.SetActive(false); AudioManager.Instance.SetMusicVolume(0.5f); _isMusicOn = true;}
    }
    #endregion

    #region Unity's Methods

    #endregion
  }
}
