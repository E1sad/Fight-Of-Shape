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

    [Header("Links")]
    [SerializeField] private PauseController controller;
    
    //Internal varibales
    private int _score;
    private int _bestScore;

    #region My Methods
    public void OnPlayButtonPressed(){
      controller.PlayButtonPressed();
    }
    public void OnSettingsButtonPressed(){
      controller.SettingsButtonPressed();
    }
    public void OnRestartButtonPressed(){
      controller.RestartButtonPressed();
    }
    public void OnMainMenuButtonPressed(){
      controller.MainMenuButtonPressed();
    }
    public void AddScore(int score) { _score += score; _scoreText.text = Convert.ToString(_score); }
    public void SetScore(int score) { _score = score; _scoreText.text = Convert.ToString(_score); }
    public void BestScore(){if(_bestScore<_score)_bestScore=_score; _bestScoreText.text="Best: "+_bestScore;}
    public void SetBestScoer(int score) { _bestScore = score; _bestScoreText.text = "Best: " + _bestScore; }

    #endregion

    #region Unity's Methods

    #endregion
  }
}
