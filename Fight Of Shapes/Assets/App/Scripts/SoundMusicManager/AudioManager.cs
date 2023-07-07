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

    #region My Methods
    public void SetSoundVolume(float volume) { _soundSource.volume = volume; }
    public void SetMusicVolume(float volume) { _musicSource.volume = volume; }
    public void PlaySoundClip(AudioClip clip) { _soundSource.PlayOneShot(clip); }
    public void PlayMusicClip(AudioClip clip) { 
      _musicSource.loop = true; _musicSource.clip = clip; _musicSource.Play(); 
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
    #endregion
  }
}
