using SOG.Game_Manager;
using SOG.UI.GameOver;
using SOG.UI.MainMenu;
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
    [SerializeField] private int _hexagonChance;
    [SerializeField] private int _pentagonChance;
    [SerializeField] private int _squareChance;
    [SerializeField] private int _triangleChance;

    [Header("Links")]
    [SerializeField] private GameObject[] _enemyTypes;

    //Internal varibales
    private System.Random _random;
    private List<GameObject> _sendableEnemiesList;
    private List<GameObject> _enemiesList;
    private List<GameObject> _sendedEnemiesList;
    private IEnumerator _instantiateEnemies;
    private Coroutine _routineSendEnemise;
    private int _hardnessCounter;

    #region My Methods
    private IEnumerator instantiateEnemies(){
      for (int type = 0; type < _enemyTypes.Length; type++){
        for (int i = 0; i < 5; i++){
          GameObject enemy = Instantiate(_enemyTypes[type], transform);
          enemy.GetComponent<EnemyStats>().SetHealthRemainder();
          enemy.GetComponent<EnemyMovement>().SetIsGamePlayState(true);
          enemy.GetComponent<EnemyMovement>().SetHardness(0);
          enemy.SetActive(false);
          _sendableEnemiesList.Add(enemy);
          _enemiesList.Add(enemy);
          yield return new WaitForSeconds(0.1f);}}
    }
    private IEnumerator sendEnemies()
    {while (_gameStatePlay){
        yield return new WaitForSeconds(_frequency-(float)((_hardnessCounter/10)*0.1));
        GameObject selectedEnemy = null;
        int selectedCornerNumber = 0;
        int random = _random.Next(0, 100);
/*        Debug.Log(random);
        Debug.Log($"Hexagon chance: {_hexagonChance + (_hardnessCounter / 5)}");
        Debug.Log($"Pentagon chance: {_pentagonChance + (_hardnessCounter / 5)}");
        Debug.Log($"Square chance: {_squareChance + (_hardnessCounter / 5)}");
        Debug.Log($"Triangle chance: {_triangleChance + (_hardnessCounter / 5)}");
        Debug.Log($"Circle chance: {100 - (_triangleChance+ (_hardnessCounter / 5))}");*/
        if (random < _hexagonChance + (_hardnessCounter / 5)) selectedCornerNumber = 6;
        else if (random>=_hexagonChance+(_hardnessCounter/5)&&random<_pentagonChance+(_hardnessCounter/5))
          selectedCornerNumber = 5;
        else if (random>=_pentagonChance+(_hardnessCounter/5)&&random<_squareChance+(_hardnessCounter/5))
          selectedCornerNumber = 4;
        else if (random>=_squareChance+(_hardnessCounter/5)&&random<_triangleChance+(_hardnessCounter/5))
          selectedCornerNumber = 3;
        else selectedCornerNumber = 0;
        for (int i = 0; i < _sendableEnemiesList.Count; i++){
          if (_sendableEnemiesList[i].GetComponent<EnemyStats>().Corner == selectedCornerNumber)
          { selectedEnemy = _sendableEnemiesList[i]; _sendableEnemiesList.RemoveAt(i); break;}}
        if (selectedEnemy == null) continue;
        selectedEnemy.GetComponent<EnemyMovement>().SetHardness(_hardnessCounter / 10);
        selectedEnemy.transform.position = _locations[_random.Next(0, 3)];
        selectedEnemy.SetActive(true);
        _hardnessCounter++;
        _sendedEnemiesList.Add(selectedEnemy);}
    }
    private void destroyed(EnemyStats enemy){
      enemy.gameObject.SetActive(false);
      _sendedEnemiesList.RemoveAll(Enemy => Enemy == enemy.gameObject);
      _sendableEnemiesList.Add(enemy.gameObject);
    }
    private void gameStateHandler(object sender, GameStateChangedEvent eventargs){
      switch (eventargs.CurrentGameState){
        case GameState.IDLE_STATE: restartAndIdleState(); break;
        case GameState.RESTART_STATE: restartAndIdleState(); break;
        case GameState.PAUSE_STATE: pauseState(); break;
        case GameState.PLAY_STATE: gamePlayState(); break;
        default: break;}
    }
    private void gamePlayState() {
      for (int i = 0; i < _sendedEnemiesList.Count; i++){
        _sendedEnemiesList[i].GetComponent<EnemyMovement>().EnemyRb.bodyType = RigidbodyType2D.Dynamic;
        _sendedEnemiesList[i].GetComponent<EnemyMovement>().SetIsGamePlayState(true);}
      _gameStatePlay = true; startSendEnemiesCoroutine();
    }
    private void pauseState(){
      _gameStatePlay = false; stopSendEnemiesCoroutine();
      for (int i = 0; i < _sendedEnemiesList.Count; i++){
        _sendedEnemiesList[i].GetComponent<EnemyMovement>().SetIsGamePlayState(false);
        _sendedEnemiesList[i].GetComponent<EnemyMovement>().EnemyRb.bodyType = RigidbodyType2D.Static;}
    }
    private void restartAndIdleState(){
      _gameStatePlay = false; stopSendEnemiesCoroutine();
      _hardnessCounter = 0;
      for (int i = 0; i < _enemiesList.Count; i++){ 
        _enemiesList[i].GetComponent<EnemyMovement>().EnemyRb.bodyType = RigidbodyType2D.Dynamic;
        _enemiesList[i].GetComponent<EnemyMovement>().SetIsGamePlayState(true);
        _enemiesList[i].GetComponent<EnemyMovement>().SetHardness(0);
        _enemiesList[i].GetComponent<EnemyStats>().RestartState();
        _enemiesList[i].SetActive(false);}
      for (int i = 0; i < _sendedEnemiesList.Count; i++) {
        _sendableEnemiesList.Add(_sendedEnemiesList[0]); _sendedEnemiesList.RemoveAt(0);}
    }
    private void DestroyEnemeyEventHandler(object sender, DestroyEnemyEventArgs eventArgs) {
      destroyed(eventArgs.ThisEnemy);
    }
    private void startSendEnemiesCoroutine() { 
      if (_routineSendEnemise != null) StopCoroutine(_routineSendEnemise);
      _routineSendEnemise = StartCoroutine(sendEnemies());
    }
    private void stopSendEnemiesCoroutine(){
      if (_routineSendEnemise != null) StopCoroutine(_routineSendEnemise);
      _routineSendEnemise = null;
    }
    #endregion

    #region Unity's Methods
    private void Start(){
      _random = new System.Random();
      _sendableEnemiesList = new List<GameObject>();
      _sendedEnemiesList = new List<GameObject>();
      _enemiesList = new List<GameObject>();
      _instantiateEnemies = instantiateEnemies();
      StartCoroutine(_instantiateEnemies);
      _hardnessCounter = 0;
    }
    private void OnEnable(){
      GameStateEvents.OnGameStateChanged += gameStateHandler;
      DestroyEnemyEvent.EventDestroyEnemy += DestroyEnemeyEventHandler;
      //GameOverEvent.EventGameOver += pauseState;
      //PlayButtonPressedEvent.OnPlayButtonPressedEvent += gamePlayState;
    }
    private void OnDisable(){
      GameStateEvents.OnGameStateChanged -= gameStateHandler;
      DestroyEnemyEvent.EventDestroyEnemy -= DestroyEnemeyEventHandler;
      //GameOverEvent.EventGameOver += pauseState;
      //PlayButtonPressedEvent.OnPlayButtonPressedEvent -= gamePlayState;
    }
    #endregion
  }
}