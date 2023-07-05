using SOG.UI.GamePlay;
using SOG.UI.MainMenu;
using SOG.UI.Pause;
using SOG.UI.Shop;
using UnityEngine;

namespace SOG.UI.GameOver{
  public class GameOverController : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private GameOverView view;

    #region My Methods
    public void SettingsButtonPressed(){
      view.gameObject.SetActive(false); SettingsButtonPressedEvent.Raise(false,false);
    }
    public void RestartButtonPressed(){
      view.gameObject.SetActive(false); RestartButtonPressedEvent.Raise(true); view.SetScore(0);
    }
    public void OnShopMenuButtonPressed() {
      view.gameObject.SetActive(false); ShopButtonPressedEvent.Raise("GameOver");
    }
    public void MainMenuButtonPressed(){
      view.gameObject.SetActive(false); MainMenuButtonPressedEvent.Raise(); view.SetScore(0);
    }
    private void BackButtonPressedEventHandler(bool isFromMenu, bool isFromPause){
      if (!isFromMenu && !isFromPause) view.gameObject.SetActive(true);
    }
    private void DamagedPlayerHealhtEventHandler(int health){
      if (health > 0) return; view.BestScore(); view.gameObject.SetActive(true);
      GameOverEvent.Raise(); view.AddCoin();
    }
    private void addScoreEventHandler(int score){ view.AddScore(score); }
    private void BackButtonFromShopPressedEventHandler(object sender, BackButtonFromShopEventArguments eventArgs) { 
      if(eventArgs.PreviousPage == "GameOver") {view.gameObject.SetActive(true);}
    }
    public void SendCoinEvent(int coin) { AddCoinEvent.Raise(coin); }
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
      BackButtonPressedEvent.ShopMenuBackButtonPressedEvent += BackButtonFromShopPressedEventHandler;
    }
    private void OnDisable(){
      SOG.UI.Settings.BackButtonPressedEvent.OnBackButtonPressedEvent -= BackButtonPressedEventHandler;
      DamagedPlayerHealhtEvent.EventDamagedPlayerHealth -= DamagedPlayerHealhtEventHandler;
      AddScoreEvent.EventAddScore -= addScoreEventHandler;
      BackButtonPressedEvent.ShopMenuBackButtonPressedEvent -= BackButtonFromShopPressedEventHandler;
    }
    #endregion
  }
}
