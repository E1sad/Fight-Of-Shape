using System;
using TMPro;
using UnityEngine;

namespace SOG.UI.Pause
{
  public class PauseView : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private AudioClip _buttonClip;

    [Header("Links")]
    [SerializeField] private PauseController controller;
    
    //Internal varibales
    private int _score;
    private int _bestScore;

    #region My Methods
    public void OnPlayButtonPressed(){
      controller.PlayButtonPressed(); Audio_Manager.AudioManager.Instance.PlaySoundClip(_buttonClip);
    }
    public void OnSettingsButtonPressed(){
      controller.SettingsButtonPressed(); Audio_Manager.AudioManager.Instance.PlaySoundClip(_buttonClip);
    }
    public void OnRestartButtonPressed(){
      controller.RestartButtonPressed(); Audio_Manager.AudioManager.Instance.PlaySoundClip(_buttonClip);
    }
    public void OnMainMenuButtonPressed(){
      controller.MainMenuButtonPressed(); Audio_Manager.AudioManager.Instance.PlaySoundClip(_buttonClip);
    }
    public void AddScore(int score) {
      if ((_score + score) <= 0) _score = 0;
      else _score += score; 
      _scoreText.text = Convert.ToString(_score); 
    }
    public void SetScore(int score) { _score = score; _scoreText.text = Convert.ToString(_score); }
    public void BestScore(){if(_bestScore<_score)_bestScore=_score; _bestScoreText.text="Best: "+_bestScore;}
    public void SetBestScoer(int score) { _bestScore = score; _bestScoreText.text = "Best: " + _bestScore; }

    #endregion

    #region Unity's Methods

    #endregion
  }
}
