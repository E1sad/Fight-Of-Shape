using System;
using TMPro;
using UnityEngine;

namespace SOG.UI.GamePlay
{
  public class GamePlayView : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _HealhtPlaceHolder;

    [Header("Links")]
    [SerializeField] private GamePlayController controller;

    //Internal Variables
    private int _score;
    private int _health;

    #region My Methods
    public void OnPauseButtonPressed() { controller.PauseButtonPressed();}
    public void AddScore(int score) {
      if ((_score + score) <= 0) { SetScore(0); _score = 0; }
      else _score += score; 
      _scoreText.text = Convert.ToString(_score); 
    }
    public void SetScore(int score) { _score = score; _scoreText.text = Convert.ToString(_score); }
    public void DamagedHealth(int health) { _health = health; _HealhtPlaceHolder.text = "" + _health; }
    #endregion

    #region Unity's Methods
    private void Start(){
      _score = 0;
      AddScore(_score);
    }
    #endregion
  }
}
