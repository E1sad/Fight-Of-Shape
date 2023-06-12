using SOG.Game_Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.Enemy
{
  public class EnemySpawner : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private Vector3[] _locations;
    [SerializeField] private bool _gameStatePlay;
    [SerializeField] private float _frequency;

    [Header("Links")]
    [SerializeField] private GameObject[] _enemyTypes;

    //Internal varibales
    private System.Random _random;
    private List<GameObject> _sendableEnemiesList;
    private List<GameObject> _enemiesList;
    private List<GameObject> _sendedEnemiesList;
    private IEnumerator _instantiateEnemies, _sendEnemies;
    private bool _isInvokeCompleted;

    #region My Methods
    private IEnumerator instantiateEnemies(){
      for (int type = 0; type < _enemyTypes.Length; type++){
        for (int i = 0; i < 5; i++){
          GameObject enemy = Instantiate(_enemyTypes[type], transform);
          enemy.SetActive(false);
          _sendableEnemiesList.Add(enemy);
          _enemiesList.Add(enemy);
          yield return new WaitForSeconds(0.1f);}}
    }
    private IEnumerator sendEnemies()
    {while (_gameStatePlay){
        GameObject selectedEnemy = null;
        int random = _random.Next(0, 100);
        if ( random % 6 == 0)
        {for (int i = 0; i < _sendableEnemiesList.Count; i++)
          {if (_sendableEnemiesList[i].GetComponent<EnemyStats>().Corner == 4)
            {selectedEnemy = _sendableEnemiesList[i]; _sendableEnemiesList.RemoveAt(i); break;}}}
        else if(random % 3 == 0)
        {for (int i = 0; i < _sendableEnemiesList.Count; i++)
          {if (_sendableEnemiesList[i].GetComponent<EnemyStats>().Corner == 3)
            { selectedEnemy = _sendableEnemiesList[i]; _sendableEnemiesList.RemoveAt(i); break; }}}
        else
        {for (int i = 0; i < _sendableEnemiesList.Count; i++)
          {if (_sendableEnemiesList[i].GetComponent<EnemyStats>().Corner == 0)
            { selectedEnemy = _sendableEnemiesList[i]; _sendableEnemiesList.RemoveAt(i); break; }}}
        if (selectedEnemy == null) continue;
        selectedEnemy.transform.position = _locations[_random.Next(0, 3)];
        selectedEnemy.SetActive(true);
        _sendedEnemiesList.Add(selectedEnemy);
        yield return new WaitForSeconds(_frequency);}
    }
    private void destroyed(EnemyStats enemy){
      enemy.gameObject.SetActive(false);
      _sendedEnemiesList.RemoveAll(Enemy => Enemy == enemy.gameObject);
      _sendableEnemiesList.Add(enemy.gameObject);
    }
    private void startCoroutineSendEnemies(){StartCoroutine(_sendEnemies);}
    private void stopCoroutineSendEnemies() { StopCoroutine(_sendEnemies);}
    private void gameStateHandler(object sender, GameStateChangedEvent eventargs){
      switch (eventargs.CurrentGameState){
        case GameState.IDLE_STATE: restartAndIdleState(); break;
        case GameState.RESTART_STATE: restartAndIdleState(); break;
        case GameState.PAUSE_STATE: pauseState(); break;
        case GameState.PLAY_STATE: gamePlayState(); break;
        default: break;}
    }
    private void gamePlayState() {
      for (int i = 0; i < _enemiesList.Count; i++){
        _enemiesList[i].GetComponent<EnemyMovement>().EnemyRb.bodyType = RigidbodyType2D.Dynamic;}
      _gameStatePlay = true;
      if (_isInvokeCompleted){
        Invoke("startCoroutineSendEnemies", _frequency); _isInvokeCompleted = false;}
    }
    private void pauseState(){
      _gameStatePlay = false;
      stopCoroutineSendEnemies();
      for (int i = 0; i < _enemiesList.Count; i++){
        _enemiesList[i].GetComponent<EnemyMovement>().EnemyRb.bodyType = RigidbodyType2D.Static;}
    }
    private void restartAndIdleState(){
      _gameStatePlay = false;
      stopCoroutineSendEnemies();
      for (int i = 0; i < _enemiesList.Count; i++){ _enemiesList[i].SetActive(false);}
      for (int i = 0; i < _sendedEnemiesList.Count; i++) {
        _sendableEnemiesList.Add(_sendedEnemiesList[0]); _sendedEnemiesList.RemoveAt(0);}
    }
    private void DestroyEnemeyEventHandler(object sender, DestroyEnemyEventArgs eventArgs) {
      destroyed(eventArgs.ThisEnemy);
    }
    private void alternativeStartCoroutine() { startCoroutineSendEnemies(); _isInvokeCompleted = true; }
    #endregion

    #region Unity's Methods
    private void Start(){
      _random = new System.Random();
      _sendableEnemiesList = new List<GameObject>();
      _sendedEnemiesList = new List<GameObject>();
      _enemiesList = new List<GameObject>();
      _instantiateEnemies = instantiateEnemies();
      _sendEnemies = sendEnemies();
      StartCoroutine(_instantiateEnemies);
      Invoke("startCoroutineSendEnemies", 3f);
      _isInvokeCompleted = true;
    }
    private void OnEnable(){
      GameStateEvents.OnGameStateChanged += gameStateHandler;
      DestroyEnemyEvent.EventDestroyEnemy += DestroyEnemeyEventHandler;
    }
    private void OnDisable(){
      GameStateEvents.OnGameStateChanged -= gameStateHandler;
      DestroyEnemyEvent.EventDestroyEnemy -= DestroyEnemeyEventHandler;
    }
    #endregion
  }
}