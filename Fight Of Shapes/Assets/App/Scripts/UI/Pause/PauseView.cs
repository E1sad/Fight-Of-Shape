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
    #endregion

    #region Unity's Methods
    private void Start(){
      _score = 0;
      AddScore(_score);
    }
    #endregion
  }
}
