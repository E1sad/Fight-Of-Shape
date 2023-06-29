using SOG.Game_Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SOG.Bullet{
  public class DamageFeedback : MonoBehaviour{
    [Header("Variables")]
    [SerializeField] private float _duration;
    [SerializeField] private float _getAwayMultiplier;

    [Header("Links")]
    [SerializeField] private GameObject _textPrefab;
    [SerializeField] private ParticleSystem _particleSystem;

    //Internal varibales
    private List<GameObject> _allTextGameObjects;
    private List<GameObject> _sendableTextGameObjects;
    private List<GameObject> _sendedTextGameObjects;
    private int _localTimeScale;

    #region My Methods
    private IEnumerator instantiateTextObjects(){ 
      for(int i = 0; i < 20; i++) {
        GameObject textObject = Instantiate(_textPrefab, transform);
        textObject.SetActive(false);
        _allTextGameObjects.Add(textObject);
        _sendableTextGameObjects.Add(textObject);
        yield return new WaitForSeconds(0.05f);}
    }
    private IEnumerator damageFeedback(Vector3 position,int damage) {
      float elapsed = 0f;
      GameObject textObject = send();
/*      _particleSystem.transform.position = position;
      _particleSystem.Play();*/
      textObject.transform.position = position;
      position += new Vector3(.5f, 1, 0);
      TMP_Text textFeatures = textObject.GetComponent<TextMeshProUGUI>();
      textFeatures.text = System.Convert.ToString(damage);
      while (elapsed < _duration) {
        textFeatures.alpha = (1-(elapsed/_duration))*_localTimeScale;
        position += new Vector3(1*_getAwayMultiplier*_localTimeScale,2 * _getAwayMultiplier * _localTimeScale, 0);
        textObject.transform.position = position;
        elapsed += Time.deltaTime * _localTimeScale;
        yield return null;
      }
      destroy(textObject);
    }

    private GameObject send(){
      GameObject textObject = _sendableTextGameObjects[0];
      _sendedTextGameObjects.Add(textObject);
      _sendableTextGameObjects.RemoveAt(0);
      textObject.SetActive(true);
      return textObject;
    }
    private void destroy(GameObject textObject) {
      textObject.SetActive(false);
      _sendedTextGameObjects.RemoveAll(tObject => tObject == textObject);
      _sendableTextGameObjects.Add(textObject);
    }
    private void SpawnDamageFeedbackEventHandler(object sender, SpawnDamageFeedbackEventArgs eventArgs) {
      StartCoroutine(damageFeedback(eventArgs.Position, eventArgs.Damage));
    }
    private void gameStateHandler(object sender, GameStateChangedEvent eventargs){
      switch (eventargs.CurrentGameState){
        case GameState.IDLE_STATE: restartState(); break;
        case GameState.RESTART_STATE: restartState(); break;
        case GameState.PAUSE_STATE: _localTimeScale = 0; break;
        case GameState.PLAY_STATE: _localTimeScale = 1; break;
        default: break;}
    }
    private void restartState(){
      StopAllCoroutines();
      for (int i = 0; i < _sendedTextGameObjects.Count; i++){
        _sendedTextGameObjects[i].SetActive(false);
        _sendableTextGameObjects.Add(_sendedTextGameObjects[i]);
        _sendedTextGameObjects.RemoveAt(0);
      }
    }
    #endregion

    #region Unity's Methods
    private void Start(){
      _allTextGameObjects = new List<GameObject>();
      _sendableTextGameObjects = new List<GameObject>();
      _sendedTextGameObjects = new List<GameObject>();
      StartCoroutine(instantiateTextObjects());
      _localTimeScale = 1;
    }
    private void OnEnable(){
      SpawnDamageFeedbackEvent.EventSpawnDamageFeedback += SpawnDamageFeedbackEventHandler;
      GameStateEvents.OnGameStateChanged += gameStateHandler;
    }
    private void OnDisable(){
      SpawnDamageFeedbackEvent.EventSpawnDamageFeedback -= SpawnDamageFeedbackEventHandler;
      GameStateEvents.OnGameStateChanged -= gameStateHandler;
    }
    #endregion
  }
}
