using SOG.Game_Manager;
using SOG.SaveManager;
using SOG.UI.MainMenu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SOG.Player
{
  public class Movement : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Vector3[] locations;

    [Header("Links")]
    [SerializeField] private Rigidbody2D _playerRb;

    //internal variables
    [HideInInspector] public Location _playerLocation { private set; get; }
    private int _indexOfLocations;
    private bool _isMoving;
    private bool _isRightSwipe;
    private bool _isLeftSwipe;
    private Vector2 _touchStartPos;
    private Vector2 _touchEndPos;
    private bool _isGamePlayState;

    #region My Mehtods
    private Location fromIndexToLocation(int index) { 
      switch (index){
        case 1: return Location.CENTER;
        case 0: return Location.LEFT;
        case 2: return Location.RIGHT;
        default: return Location.NULL;}
    }
    private void move(Vector3 target){
      if (Math.Round(target.x,1,MidpointRounding.AwayFromZero) == Math.Round(transform.position.x,1,MidpointRounding.AwayFromZero))
      { _isMoving = false; return; }
      _isMoving = true;
      transform.position = Vector3.MoveTowards(transform.position, target, _movementSpeed * Time.deltaTime);
    }
    private void playerMovement(){
      if (_isMoving) return;
      /*Debug.Log("Waiting....");*/
      if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || _isRightSwipe){
        if(_indexOfLocations < 2) _indexOfLocations += 1;
        _playerLocation = fromIndexToLocation(_indexOfLocations);
        _isRightSwipe = false;}
      if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || _isLeftSwipe) {
        if (_indexOfLocations > 0) _indexOfLocations -= 1;
        _playerLocation = fromIndexToLocation(_indexOfLocations);
        _isLeftSwipe = false;}
    }
    private void Swipe(){
      if (Input.touchCount > 0){
        if(Input.GetTouch(0).phase == TouchPhase.Began) _touchStartPos = Input.GetTouch(0).position;
        if (Input.GetTouch(0).phase == TouchPhase.Ended && _touchStartPos != new Vector2(0,0)) { 
          _touchEndPos = Input.GetTouch(0).position;
          if (_touchStartPos.x - _touchEndPos.x > 50) _isLeftSwipe = true;
          if (_touchStartPos.x - _touchEndPos.x < -50)_isRightSwipe = true;}}
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
      _indexOfLocations = 1;
      _playerLocation = fromIndexToLocation(_indexOfLocations);
      move(locations[_indexOfLocations]);
    }
    private void pauseState() { _isGamePlayState = false; }
    private void gamePlayState() { _isGamePlayState = true; }
    #endregion

    #region Unity Methods
    private void Start(){
      _playerLocation = Location.CENTER;
      _isMoving = false;
      _indexOfLocations = 1;
      _isLeftSwipe = false;
      _isRightSwipe = false;
      _isGamePlayState = false;
    }
    private void Update(){
      Swipe();
      if (!_isGamePlayState) return;
      playerMovement();
      move(locations[_indexOfLocations]);
    }
    private void OnEnable(){
      GameStateEvents.OnGameStateChanged += gameStateHandler;
      PlayButtonPressedEvent.OnPlayButtonPressedEvent += gamePlayState;
    }
    private void OnDisable(){
      GameStateEvents.OnGameStateChanged -= gameStateHandler;
      PlayButtonPressedEvent.OnPlayButtonPressedEvent -= gamePlayState;
    }
    #endregion
  }
}

