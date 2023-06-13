using SOG.Bullet;
using SOG.Game_Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.Player
{
  public class Shooting : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private float _ShootingFrequency;

    [Header("Links")]
    [SerializeField] private Movement _playerMovement;

    //Internal varibales
    private IEnumerator _shootCoroutine;
    private bool _isGamePlayState;
    private bool _isInvokeCompleted;

    #region My Methods
    private Vector3 fromLocationToVector(Location loc){
      switch (loc){
        case Location.LEFT: return new Vector3(-1.5f, -4, 0);
        case Location.CENTER: return new Vector3(0, -4, 0);
        case Location.RIGHT: return new Vector3(1.5f, -4, 0);
        default: return new Vector3(10,10,0);}
    }
    private IEnumerator shootingCoroutine(){
      while (_isGamePlayState){
        yield return new WaitForSecondsRealtime(_ShootingFrequency);
        SpawnBulletEvent.Raise(this, 
          new SpawnBulletEventArgs(fromLocationToVector(_playerMovement._playerLocation), Quaternion.identity));}
    }
    private void gameStateHandler(object sender, GameStateChangedEvent eventargs){
      switch (eventargs.CurrentGameState){
        case GameState.IDLE_STATE: restartAndIdleState(); break;
        case GameState.RESTART_STATE: restartAndIdleState(); break;
        case GameState.PAUSE_STATE: pauseState(); break;
        case GameState.PLAY_STATE: gamePlayState(); break;
        default: break;}
    }
    private void restartAndIdleState() { 
      _isGamePlayState = false;
      StopCoroutine(_shootCoroutine);
    }
    private void pauseState() { 
      _isGamePlayState = false;
      StopCoroutine(_shootCoroutine); 
    }
    private void gamePlayState() { 
      _isGamePlayState = true;
      StopCoroutine(_shootCoroutine);
      if (_isInvokeCompleted){
        _isInvokeCompleted = false; Invoke("alternativeStartCoroutine", _ShootingFrequency);}
    }
    private void alternativeStartCoroutine(){ StartCoroutine(_shootCoroutine); _isInvokeCompleted = true; }
    #endregion

    #region Unity's Methods
    private void Start(){
      //_isGamePlayState = false; // when Menu impliment you should change that
      _shootCoroutine = shootingCoroutine();
      //StartCoroutine(_shootCoroutine);
      _isInvokeCompleted = true;
    }
    private void OnEnable(){
      GameStateEvents.OnGameStateChanged += gameStateHandler;
    }
    private void OnDisable(){
      GameStateEvents.OnGameStateChanged -= gameStateHandler;
    }
    #endregion
  }
}