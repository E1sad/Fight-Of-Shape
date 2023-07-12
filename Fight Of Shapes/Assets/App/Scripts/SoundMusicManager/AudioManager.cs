using SOG.SaveManager;
using UnityEngine;

namespace SOG.Audio_Manager
{
  public class AudioManager : MonoBehaviour
  {
    [Header("Variables")]
    public static AudioManager Instance;
    [SerializeField] private AudioClip _musicClip;

    [Header("Links")]
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private AudioSource _musicSource;

    //Internal varibales
    private bool _isMusicOn = true;
    private bool _isSoundOn = true;

    #region My Methods
    public void SetSoundVolume(float volume) { 
      _soundSource.volume = volume;
      if (volume == 0) _isSoundOn = false;
      else _isSoundOn = true;
      SaveMusicSettings.Raise(_isMusicOn,_isSoundOn);
    }
    public void SetMusicVolume(float volume) { 
      _musicSource.volume = volume; 
      if (volume == 0) _isMusicOn = false;
      else _isMusicOn = true;
      SaveMusicSettings.Raise(_isMusicOn, _isSoundOn);
    }
    public void PlaySoundClip(AudioClip clip) { _soundSource.PlayOneShot(clip); }
    public void PlayMusicClip(AudioClip clip) { 
      _musicSource.loop = true; _musicSource.clip = clip; _musicSource.Play(); 
    }
    private void sendDataToObjectsEventHandler(int[] upgradeLevel, int bestScore, int money, int HardnessLevel,
      bool isMusicOn, bool isSoundOn, bool isFirstTime){
      _isSoundOn = isSoundOn; _isMusicOn = isMusicOn;
      if (_isMusicOn) SetMusicVolume(0.5f); else SetMusicVolume(0);
      if (_isSoundOn) SetSoundVolume(1f); else SetSoundVolume(0);
    }
    #endregion

    #region Unity's Methods
    private void Awake(){
      if (Instance == null) Instance = this;
      else Destroy(gameObject);
    }
    private void Start(){
      PlayMusicClip(_musicClip);
    }
    private void OnEnable(){
      SendDataToObjects.SendDataToObjectsEvent += sendDataToObjectsEventHandler;
    }
    private void OnDisable(){
      SendDataToObjects.SendDataToObjectsEvent -= sendDataToObjectsEventHandler;
    }
    #endregion
  }
}
