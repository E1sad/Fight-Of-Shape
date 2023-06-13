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
    #endregion

    #region Unity's Methods
    private void Start(){
      _score = 0;
      AddScore(_score);
    }
    #endregion
  }
}
