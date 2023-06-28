using SOG.UI.GamePlay;
using SOG.UI.MainMenu;
using SOG.UI.Pause;
using UnityEngine;

namespace SOG.UI.GameOver{
  public class GameOverController : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private GameOverView view;

    #region My Methods
    public void SettingsButtonPressed(){
      view.gameObject.SetActive(false);
      SettingsButtonPressedEvent.Raise(false,false);
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
      if (!isFromMenu && !isFromPause) view.gameObject.SetActive(true);
    }
    private void DamagedPlayerHealhtEventHandler(int health){
      if (health > 0) return;
      view.BestScore();
      view.gameObject.SetActive(true);
      GameOverEvent.Raise();
    }
    private void addScoreEventHandler(int score){ view.AddScore(score); }
    #endregion

    #region Unity's Methods
    private void Start()
    {
      view.SetScore(0);
      view.SetBestScoer(0); //Temporary. When save system implemented, you should change that;

    }
    private void OnEnable(){
      SOG.UI.Settings.BackButtonPressedEvent.OnBackButtonPressedEvent += BackButtonPressedEventHandler;
      DamagedPlayerHealhtEvent.EventDamagedPlayerHealth += DamagedPlayerHealhtEventHandler;
      AddScoreEvent.EventAddScore += addScoreEventHandler;
    }
    private void OnDisable(){
      SOG.UI.Settings.BackButtonPressedEvent.OnBackButtonPressedEvent -= BackButtonPressedEventHandler;
      DamagedPlayerHealhtEvent.EventDamagedPlayerHealth -= DamagedPlayerHealhtEventHandler;
      AddScoreEvent.EventAddScore -= addScoreEventHandler;

    }
    #endregion
  }
}
