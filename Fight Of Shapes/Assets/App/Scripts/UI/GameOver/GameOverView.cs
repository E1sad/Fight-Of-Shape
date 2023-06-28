using System;
using TMPro;
using UnityEngine;

namespace SOG.UI.GameOver
{
  public class GameOverView : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _bestScoreText;

    [Header("Links")]
    [SerializeField] private GameOverController controller;
    
    //Internal varibales
    private int _score;
    private int _bestScore;

    #region My Methods
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
    public void BestScore(){if(_bestScore<_score)_bestScore = _score; _bestScoreText.text="Best: "+_bestScore;}
    public void SetBestScoer(int score) { _bestScore = score; _bestScoreText.text = "Best: " + _bestScore;}
    #endregion

    #region Unity's Methods

    #endregion
  }
}
