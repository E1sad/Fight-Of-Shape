using SOG.Player;
using SOG.UI.GameOver;
using SOG.UI.MainMenu;
using SOG.UI.Pause;
using SOG.UI.Shop;
using UnityEngine;

namespace SOG.UI.GamePlay{
  public class GamePlayController : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private GamePlayView view;

    //Internal varibales

    #region My Methods
    public void PauseButtonPressed(){
      view.PauseButtonPressed();
      view.gameObject.SetActive(false);
      PauseButtonPressedEvent.Raise();
    }
    private void addScoreEventHandler(int score) {
      view.ScoreAddedOrSubtractedFeedback(score); view.AddScore(score);}
    private void onPlayButtonPressedEventHandler() { view.gameObject.SetActive(true);}
    private void damagedPlayerHealthEventHandler(int health) { view.DamagedHealth(health);}
    private void OnRestartButtonPressedHandler(bool isFromMenu){
      view.gameObject.SetActive(true); view.SetScore(0);
    }
    private void OnMainMenuButtonPressedEventHandler() { view.gameObject.SetActive(false); view.SetScore(0); }
    private void gameOverEventHandler() { view.gameObject.SetActive(false);}
    private void damagedPlayerHealhtEventHandler(int health) { view.DamagedHealth(health); }
    private void onGameStateChangedEventHandler(object sender, Game_Manager.GameStateChangedEvent eventArgs) {
      if(eventArgs.CurrentGameState == GameState.RESTART_STATE) view.SetScore(0);
    }
    private void PlayerStatsCahngedEventHandler(PlayerScriptableObject playerStats){
      view.ChangeHealthImage(playerStats.PlayerType); ;
    }
    #endregion

    #region Unity's Methods
    private void Start(){
      //Temporary. When save system implemented you should change that.
      view.ChangeHealthImage(PlayerTypeEnum.CIRCLE);
    }
    private void OnEnable(){
      AddScoreEvent.EventAddScore += addScoreEventHandler;
      PlayButtonPressedEvent.OnPlayButtonPressedEvent += onPlayButtonPressedEventHandler;
      RestartButtonPressedEvent.OnRestartButtonPressed += OnRestartButtonPressedHandler;
      MainMenuButtonPressedEvent.OnMainMenuButtonPressedEvent += OnMainMenuButtonPressedEventHandler;
      GameOverEvent.EventGameOver += gameOverEventHandler;
      DamagedPlayerHealhtEvent.EventDamagedPlayerHealth += damagedPlayerHealhtEventHandler;
      Game_Manager.GameStateEvents.OnGameStateChanged += onGameStateChangedEventHandler;
      PlayerStatsChanged.PlayerStatsCahngedEvent += PlayerStatsCahngedEventHandler;
    }
    private void OnDisable(){
      AddScoreEvent.EventAddScore -= addScoreEventHandler;
      PlayButtonPressedEvent.OnPlayButtonPressedEvent -= onPlayButtonPressedEventHandler;
      RestartButtonPressedEvent.OnRestartButtonPressed -= OnRestartButtonPressedHandler;
      MainMenuButtonPressedEvent.OnMainMenuButtonPressedEvent -= OnMainMenuButtonPressedEventHandler;
      GameOverEvent.EventGameOver -= gameOverEventHandler;
      DamagedPlayerHealhtEvent.EventDamagedPlayerHealth -= damagedPlayerHealhtEventHandler;
      Game_Manager.GameStateEvents.OnGameStateChanged -= onGameStateChangedEventHandler;
      PlayerStatsChanged.PlayerStatsCahngedEvent += PlayerStatsCahngedEventHandler;
    }
    #endregion
  }
}
