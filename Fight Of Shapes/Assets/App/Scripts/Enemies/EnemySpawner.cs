using SOG.Bullet;
using SOG.Game_Manager;
using SOG.Player;
using SOG.SaveManager;
using SOG.UI.GameOver;
using SOG.UI.MainMenu;
using SOG.UI.Shop;
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
    private int _temporaryHardnessCounter;
    private int _permanentHardnessCounter;

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
        yield return new WaitForSeconds(_frequency-(float)((_temporaryHardnessCounter/10)*0.1)-(float)(_permanentHardnessCounter*0.03));
        GameObject selectedEnemy = null;
        int selectedCornerNumber = 0;
        int random = _random.Next(0, 100);
        if (random < _hexagonChance + (_temporaryHardnessCounter / 5) + _permanentHardnessCounter) 
          selectedCornerNumber = 6;
        else if (random>=_hexagonChance+(_temporaryHardnessCounter/5) + _permanentHardnessCounter && 
          random<_pentagonChance+(_temporaryHardnessCounter/5) + _permanentHardnessCounter)
          selectedCornerNumber = 5;
        else if (random>=_pentagonChance+(_temporaryHardnessCounter/5) + _permanentHardnessCounter &&
          random<_squareChance+(_temporaryHardnessCounter/5) + _permanentHardnessCounter)
          selectedCornerNumber = 4;
        else if (random>=_squareChance+(_temporaryHardnessCounter/5) + _permanentHardnessCounter &&
          random<_triangleChance+(_temporaryHardnessCounter/5) + _permanentHardnessCounter)
          selectedCornerNumber = 3;
        else selectedCornerNumber = 0;
        for (int i = 0; i < _sendableEnemiesList.Count; i++){
          if (_sendableEnemiesList[i].GetComponent<EnemyStats>().Corner == selectedCornerNumber)
          { selectedEnemy = _sendableEnemiesList[i]; _sendableEnemiesList.RemoveAt(i); break;}}
        if (selectedEnemy == null) continue;
        selectedEnemy.GetComponent<EnemyMovement>().SetHardness(_temporaryHardnessCounter / 10);
        selectedEnemy.transform.position = _locations[_random.Next(0, 3)];
        selectedEnemy.SetActive(true);
        _temporaryHardnessCounter++;
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
      _temporaryHardnessCounter = 0;
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
    private void playerStatsChangedEventHandler(PlayerScriptableObject playerStats) {
      _permanentHardnessCounter += 3; SaveHardnessLevel.Raise(this,_permanentHardnessCounter);
    }
    private void bulletShapeChangedEventHandler(BulletScriptableObject newShape){
      _permanentHardnessCounter += 3; SaveHardnessLevel.Raise(this, _permanentHardnessCounter);
    }
    private void criticalBulletChanceEventHandler(int chance){
      _permanentHardnessCounter += 3; SaveHardnessLevel.Raise(this, _permanentHardnessCounter);
    }
    private void sendDataToObjectsEventHandler(int[] upgradeLevel, int bestScore, int money, int HardnessLevel,
      bool isMusicOn, bool isSoundOn, bool isFirstTime)
    {
      _permanentHardnessCounter = HardnessLevel;
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
      _temporaryHardnessCounter = 0;
      _permanentHardnessCounter = 0; //Temporary. Should change when save system is implemented
    }
    private void OnEnable(){
      GameStateEvents.OnGameStateChanged += gameStateHandler;
      DestroyEnemyEvent.EventDestroyEnemy += DestroyEnemeyEventHandler;
      PlayerStatsChanged.PlayerStatsCahngedEvent += playerStatsChangedEventHandler;
      BulletShapeChanged.BulletShapeChangedEvent += bulletShapeChangedEventHandler;
      CriticalBulletChance.CriticalBulletChanceEvent += criticalBulletChanceEventHandler;
      SendDataToObjects.SendDataToObjectsEvent += sendDataToObjectsEventHandler;
    }
    private void OnDisable(){
      GameStateEvents.OnGameStateChanged -= gameStateHandler;
      DestroyEnemyEvent.EventDestroyEnemy -= DestroyEnemeyEventHandler;
      PlayerStatsChanged.PlayerStatsCahngedEvent -= playerStatsChangedEventHandler;
      BulletShapeChanged.BulletShapeChangedEvent -= bulletShapeChangedEventHandler;
      CriticalBulletChance.CriticalBulletChanceEvent -= criticalBulletChanceEventHandler;
      SendDataToObjects.SendDataToObjectsEvent -= sendDataToObjectsEventHandler;
    }
    #endregion
  }
}