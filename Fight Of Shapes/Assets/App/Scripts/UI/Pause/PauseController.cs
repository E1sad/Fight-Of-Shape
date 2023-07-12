using SOG.SaveManager;
using SOG.UI.GameOver;
using SOG.UI.GamePlay;
using SOG.UI.MainMenu;
using UnityEngine;

namespace SOG.UI.Pause{
  public class PauseController : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private PauseView view;

    #region My Methods
    public void PlayButtonPressed(){
      view.gameObject.SetActive(false);
      PlayButtonPressedEvent.Raise();
    }
    public void SettingsButtonPressed(){
      view.gameObject.SetActive(false);
      SettingsButtonPressedEvent.Raise(false,true);
    }
    public void RestartButtonPressed(){
      view.gameObject.SetActive(false);
      RestartButtonPressedEvent.Raise(true);
      view.SetScore(0);
    }
    public void MainMenuButtonPressed(){
      view.gameObject.SetActive(false);
      MainMenuButtonPressedEvent.Raise();
      view.SetScore(0);
    }
    private void BackButtonPressedEventHandler(bool isFromMenu, bool isFromPause){
      if (!isFromMenu && isFromPause) view.gameObject.SetActive(true);
    }
    private void OnPauseButtonPressedEventHandler(){
      view.gameObject.SetActive(true);
    }
    private void addScoreEventHandler(int score){ view.AddScore(score); }
    private void gameOverEventHandler() { view.BestScore(); view.SetScore(0); }
    private void sendDataToObjectsEventHandler(int[] upgradeLevel, int bestScore, int money, int HardnessLevel,
      bool isMusicOn, bool isSoundOn, bool isFirstTime){
      view.SetBestScoer(bestScore);
    }
    #endregion

    #region Unity's Methods
    private void Start(){view.SetScore(0);}
    private void OnEnable(){
      SOG.UI.Settings.BackButtonPressedEvent.OnBackButtonPressedEvent += BackButtonPressedEventHandler;
      PauseButtonPressedEvent.OnPauseButtonPressedEvent += OnPauseButtonPressedEventHandler;
      AddScoreEvent.EventAddScore += addScoreEventHandler;
      GameOverEvent.EventGameOver += gameOverEventHandler;
      SendDataToObjects.SendDataToObjectsEvent += sendDataToObjectsEventHandler;
    }
    private void OnDisable(){
      SOG.UI.Settings.BackButtonPressedEvent.OnBackButtonPressedEvent -= BackButtonPressedEventHandler;
      PauseButtonPressedEvent.OnPauseButtonPressedEvent -= OnPauseButtonPressedEventHandler;
      AddScoreEvent.EventAddScore -= addScoreEventHandler;
      GameOverEvent.EventGameOver -= gameOverEventHandler;
      SendDataToObjects.SendDataToObjectsEvent += sendDataToObjectsEventHandler;
    }
    #endregion
  }
}
