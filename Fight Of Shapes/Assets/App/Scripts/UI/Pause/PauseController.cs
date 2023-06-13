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
      SettingsButtonPressedEvent.Raise(false);
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
    private void BackButtonPressedEventHandler(bool isFromMenu){
      if (!isFromMenu) view.gameObject.SetActive(true);
    }
    private void OnPauseButtonPressedEventHandler(){
      view.gameObject.SetActive(true);
    }
    private void addScoreEventHandler(int score){ view.AddScore(score); }
    #endregion

    #region Unity's Methods
    private void OnEnable(){
      SOG.UI.Settings.BackButtonPressedEvent.OnBackButtonPressedEvent += BackButtonPressedEventHandler;
      PauseButtonPressedEvent.OnPauseButtonPressedEvent += OnPauseButtonPressedEventHandler;
      AddScoreEvent.EventAddScore += addScoreEventHandler;
    }
    private void OnDisable(){
      SOG.UI.Settings.BackButtonPressedEvent.OnBackButtonPressedEvent -= BackButtonPressedEventHandler;
      PauseButtonPressedEvent.OnPauseButtonPressedEvent -= OnPauseButtonPressedEventHandler;
      AddScoreEvent.EventAddScore -= addScoreEventHandler;
    }
    #endregion
  }
}
