using SOG.UI.MainMenu;
using SOG.UI.Pause;
using UnityEngine;

namespace SOG.UI.GamePlay{
  public class GamePlayController : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private GamePlayView view;

    //Internal varibales

    #region My Methods
    public void PauseButtonPressed(){
      view.gameObject.SetActive(false);
      PauseButtonPressedEvent.Raise();
    }
    private void addScoreEventHandler(int score) { view.AddScore(score);}
    private void onPlayButtonPressedEventHandler() { view.gameObject.SetActive(true);}
    private void damagedPlayerHealthEventHandler(int health) { view.DamagedHealth(health);}
    private void OnRestartButtonPressedHandler(bool isFromMenu){
      view.gameObject.SetActive(true);
      view.SetScore(0);
    }
    #endregion

    #region Unity's Methods
    private void OnEnable(){
      AddScoreEvent.EventAddScore += addScoreEventHandler;
      PlayButtonPressedEvent.OnPlayButtonPressedEvent += onPlayButtonPressedEventHandler;
      RestartButtonPressedEvent.OnRestartButtonPressed += OnRestartButtonPressedHandler;
      //DamagedPlayerHealhtEvent.EventDamagedPlayerHealth += damagedPlayerHealthEventHandler; 
    }
    private void OnDisable(){
      AddScoreEvent.EventAddScore -= addScoreEventHandler;
      PlayButtonPressedEvent.OnPlayButtonPressedEvent -= onPlayButtonPressedEventHandler;
      RestartButtonPressedEvent.OnRestartButtonPressed += OnRestartButtonPressedHandler;
      //DamagedPlayerHealhtEvent.EventDamagedPlayerHealth -= damagedPlayerHealthEventHandler;
    }
    #endregion
  }
}
