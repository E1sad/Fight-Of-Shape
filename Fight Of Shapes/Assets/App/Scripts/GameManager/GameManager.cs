using SOG.UI.GameOver;
using SOG.UI.GamePlay;
using SOG.UI.MainMenu;
using SOG.UI.Pause;
using UnityEngine;

namespace SOG.Game_Manager
{
  public class GameManager : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private GameState _currentGameState;
    /*[Header("Links")]*/
    //Internal varibales
    private bool _isPauesed;

    #region My Methods
    public void GamePlayState(GameState state){
      _currentGameState = state;
      if (_currentGameState == GameState.PAUSE_STATE) _isPauesed = true;
      if (_isPauesed && _currentGameState == GameState.PLAY_STATE){
        GameStateEvents.Raise(this, _currentGameState, true); _isPauesed = false;}
      else { 
        _isPauesed = false; GameStateEvents.Raise(this, _currentGameState, false);}
    }
    private void PausePlayRestartFromKeyboard(){
      if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)){
        if (_currentGameState == GameState.PLAY_STATE){
          _currentGameState = GameState.PAUSE_STATE;
          GameStateEvents.Raise(this, _currentGameState,false);}
        else if (_currentGameState == GameState.PAUSE_STATE){
          _currentGameState = GameState.PLAY_STATE;
          GameStateEvents.Raise(this, _currentGameState,true);}}
      if (Input.GetKeyDown(KeyCode.R)){
        _currentGameState = GameState.RESTART_STATE;
        GameStateEvents.Raise(this, _currentGameState,false);
        _currentGameState = GameState.PLAY_STATE;
        GameStateEvents.Raise(this, _currentGameState,false);}
    }
    private void playButtonPressedEventHandler(){
      _currentGameState = GameState.PLAY_STATE;
      GamePlayState(_currentGameState);
    }
    private void pauseButtonPressedEventHandler(){
      _currentGameState = GameState.PAUSE_STATE;
      GamePlayState(_currentGameState);
    }
    private void OnMainMenuButtonPressedEventHandler(){
      _currentGameState = GameState.RESTART_STATE;
      GamePlayState(_currentGameState);
    }
    private void OnRestartButtonPressedHandler(bool isFromPause){
      _currentGameState = GameState.RESTART_STATE;
      GamePlayState(_currentGameState);
      _currentGameState = GameState.PLAY_STATE;
      GamePlayState(_currentGameState);
    }
    private void EventGameOverHandler(){
      _currentGameState = GameState.PAUSE_STATE;
      GamePlayState(_currentGameState);
    }
    #endregion

    #region Unity's Methods
    private void Update(){
      PausePlayRestartFromKeyboard();
    }
    private void Start(){
      Application.targetFrameRate = 60;
      _currentGameState = GameState.PAUSE_STATE;
      _isPauesed = false;
    }
    private void OnEnable(){
      PlayButtonPressedEvent.OnPlayButtonPressedEvent += playButtonPressedEventHandler;
      PauseButtonPressedEvent.OnPauseButtonPressedEvent += pauseButtonPressedEventHandler;
      MainMenuButtonPressedEvent.OnMainMenuButtonPressedEvent += OnMainMenuButtonPressedEventHandler;
      RestartButtonPressedEvent.OnRestartButtonPressed += OnRestartButtonPressedHandler;
      GameOverEvent.EventGameOver += EventGameOverHandler;
    }
    private void OnDisable(){
      PlayButtonPressedEvent.OnPlayButtonPressedEvent -= playButtonPressedEventHandler;
      PauseButtonPressedEvent.OnPauseButtonPressedEvent -= pauseButtonPressedEventHandler;
      MainMenuButtonPressedEvent.OnMainMenuButtonPressedEvent -= OnMainMenuButtonPressedEventHandler;
      RestartButtonPressedEvent.OnRestartButtonPressed -= OnRestartButtonPressedHandler;
      GameOverEvent.EventGameOver -= EventGameOverHandler;
    }
    #endregion
  }
}