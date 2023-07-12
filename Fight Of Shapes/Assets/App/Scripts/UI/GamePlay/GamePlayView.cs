using SOG.Audio_Manager;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace SOG.UI.GamePlay
{
  public class GamePlayView : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private float _duration;
    [SerializeField] private AudioClip _buttonClip;

    [Header("Links")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _addedScoreText;
    [SerializeField] private TMP_Text _subtractedScoreText;
    [SerializeField] private GamePlayController _controller;
    [SerializeField] private GameObject[] _circleHealthObjects;
    [SerializeField] private GameObject[] _triangleHealthObjects;
    [SerializeField] private GameObject[] _squareHealthObjects;
    [SerializeField] private GameObject[] _pentagonHealthObjects;
    [SerializeField] private GameObject[] _hexagonHealthObjects;
    

    //Internal Variables
    private int _score;
    private int _localTimeScale;
    private Coroutine _addRoutine;
    private Coroutine _subtractRoutine;
    private GameObject[] _healthImagesReference = new GameObject[3];

    #region My Methods
    public void OnPauseButtonPressed() { _controller.PauseButtonPressed();}
    public void AddScore(int score) {
      if ((_score + score) <= 0) { SetScore(0); _score = 0; }
      else _score += score; 
      _scoreText.text = Convert.ToString(_score); 
    }
    public void ScoreAddedOrSubtractedFeedback(int score) {
      if (score > 0)  startAddCoroutine(score);
      if (score < 0) startSubtractCoroutine(score);
    }
    private IEnumerator FadeOutAddedScore(int score){
      TMP_Text textComponent = _addedScoreText.GetComponent<TextMeshProUGUI>();
      textComponent.text = "+" + score;
      float elapsed = 0f;
      while (elapsed < _duration) {
        textComponent.alpha = (1-(elapsed / _duration));
        elapsed += Time.deltaTime * _localTimeScale;
        yield return null;}
    }
    private IEnumerator FadeOutSubtractedScore(int score){
      TMP_Text textComponent = _subtractedScoreText.GetComponent<TextMeshProUGUI>();
      textComponent.text = "" + score;
      float elapsed = 0f;
      while (elapsed < _duration){
        textComponent.alpha = (1 - (elapsed / _duration));
        elapsed += Time.deltaTime * _localTimeScale;
        yield return null;}
    }
    public void SetScore(int score) { _score = score; _scoreText.text = Convert.ToString(_score); }
    public void DamagedHealth(int health) { 
      for(int i = health; i < 3; i++){ _healthImagesReference[i].SetActive(false);}
      for(int i = 0; i < health; i++) { _healthImagesReference[i].SetActive(true);}
     }
    public void ChangeHealthImage(PlayerTypeEnum playerType) {
      if (_healthImagesReference[0] != null){
        for (int i = 0; i < _healthImagesReference.Length; i++){_healthImagesReference[i].SetActive(false);}}
      switch (playerType){
        case PlayerTypeEnum.CIRCLE: _healthImagesReference = _circleHealthObjects; break;
        case PlayerTypeEnum.HEXAGON: _healthImagesReference = _hexagonHealthObjects; break;
        case PlayerTypeEnum.PENTAGON: _healthImagesReference = _pentagonHealthObjects; break;
        case PlayerTypeEnum.TRIANGLE: _healthImagesReference = _triangleHealthObjects; break;
        case PlayerTypeEnum.SQUARE: _healthImagesReference = _squareHealthObjects; break;
        default: break;}
    }
    private void startAddCoroutine(int score) { 
      if (_addRoutine != null) StopCoroutine(_addRoutine); _addRoutine = StartCoroutine(FadeOutAddedScore(score));
    }
    private void startSubtractCoroutine(int score) {
      if (_subtractRoutine != null) StopCoroutine(_subtractRoutine);
      _subtractRoutine = StartCoroutine(FadeOutSubtractedScore(score));
    }
    private void stopAddCoroutine() {
      if (_addRoutine != null) StopCoroutine(_addRoutine); _addRoutine = null;
    }
    private void stopSubtractCoroutine() {
      if (_subtractRoutine != null) StopCoroutine(_subtractRoutine); _subtractRoutine = null;
    }
    public void PauseButtonPressed() { 
      stopAddCoroutine(); stopSubtractCoroutine();
      _addedScoreText.GetComponent<TextMeshProUGUI>().alpha = 0;
      _subtractedScoreText.GetComponent<TextMeshProUGUI>().alpha = 0;
      AudioManager.Instance.PlaySoundClip(_buttonClip);
    }
    #endregion

    #region Unity's Methods
    private void Start(){
      _localTimeScale = 1;
      _score = 0;
      AddScore(_score);
    }
    #endregion
  }
}
