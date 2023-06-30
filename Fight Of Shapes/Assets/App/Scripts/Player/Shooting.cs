using SOG.Bullet;
using SOG.Game_Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SOG.Player
{
  public class Shooting : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private float _ShootingFrequency;

    [Header("Links")]
    [SerializeField] private Movement _playerMovement;
    [SerializeField] private Image _reloadImage;

    //Internal varibales
    private Coroutine _shootingRoutine;
    private Coroutine _reloadRoutine;
    private bool _isGamePlayState;

    #region My Methods
    private Vector3 fromLocationToVector(Location loc){
      switch (loc){
        case Location.LEFT: return new Vector3(-1.5f, -3.34f, 0);
        case Location.CENTER: return new Vector3(0, -3.34f, 0);
        case Location.RIGHT: return new Vector3(1.5f, -3.34f, 0);
        default: return new Vector3(10,10,0);}
    }
    private IEnumerator shootingCoroutine(){
      while (_isGamePlayState){
        startReloadRoutine();
        yield return new WaitForSeconds(_ShootingFrequency);
        SpawnBulletEvent.Raise(this,
          new SpawnBulletEventArgs(fromLocationToVector(_playerMovement._playerLocation), Quaternion.identity));
        stopReloadRoutine();}
    }
    private IEnumerator reloadRoutine() {
      float elapsed = 0f;
      _reloadImage.fillAmount = 0;
      while(elapsed < _ShootingFrequency) {
        _reloadImage.fillAmount = elapsed / _ShootingFrequency;
        elapsed += Time.deltaTime;
        yield return null;}
    }
    private void gameStateHandler(object sender, GameStateChangedEvent eventargs){
      switch (eventargs.CurrentGameState){
        case GameState.IDLE_STATE: restartAndIdleState(); break;
        case GameState.RESTART_STATE: restartAndIdleState(); break;
        case GameState.PAUSE_STATE: pauseState(); break;
        case GameState.PLAY_STATE: gamePlayState(); break;
        default: break;}
    }
    private void restartAndIdleState() {_isGamePlayState = true; stopShootingCoroutine();}
    private void pauseState() { _isGamePlayState = false; stopShootingCoroutine();}
    private void gamePlayState() {_isGamePlayState = true; startShootingCoroutine();}
    private void startShootingCoroutine() { 
      if (_shootingRoutine != null) StopCoroutine(_shootingRoutine); 
      _shootingRoutine = StartCoroutine(shootingCoroutine());
    }
    private void stopShootingCoroutine() { 
      if(_shootingRoutine != null) StopCoroutine(_shootingRoutine); _shootingRoutine = null; 
    }
    private void startReloadRoutine() { 
      if (_reloadRoutine != null) StopCoroutine(_reloadRoutine); 
      _reloadRoutine = StartCoroutine(reloadRoutine());
    }
    private void stopReloadRoutine() { 
      if (_reloadRoutine != null) StopCoroutine(_reloadRoutine); _reloadRoutine = null;
    }
    #endregion

    #region Unity's Methods
    private void OnEnable(){
      GameStateEvents.OnGameStateChanged += gameStateHandler;
    }
    private void OnDisable(){
      GameStateEvents.OnGameStateChanged -= gameStateHandler;
    }
    #endregion
  }
}