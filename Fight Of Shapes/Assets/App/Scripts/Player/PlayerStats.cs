using SOG.Bullet;
using SOG.Game_Manager;
using SOG.UI.GamePlay;
using SOG.UI.MainMenu;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.Player
{
  public class PlayerStats : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private int _health;
    [Header("Links")]
    [SerializeField] private GameObject[] _shapes;

    //Internal varibales
    private int _healthRemainder;

    #region My Methods
    public void damage(int damageOfHit)
    {
      if ((_health - damageOfHit) <= 0) dead();
      else { _health -= damageOfHit; DamagedPlayerHealhtEvent.Raise(_health);
        Camera.ShakeCameraEvent.Raise(this, new Camera.CameraShakerEventArg(.2f, .1f));}
    }
    private void dead(){ 
      DamagedPlayerHealhtEvent.Raise(0); _health = _healthRemainder; 
      DamagedPlayerHealhtEvent.Raise(_health); }
    private void onPlayButtonPressedEventHandler() { DamagedPlayerHealhtEvent.Raise(_health); }
    private void OnGameStateChangedEventHandler(object sender , GameStateChangedEvent eventArgs) {
      if(eventArgs.CurrentGameState == GameState.PLAY_STATE) DamagedPlayerHealhtEvent.Raise(_health); 
    }
    #endregion

    #region Unity's Methods
    private void Start(){
      _healthRemainder = _health; //Temporary. When save system implemented you should change that.
    }
    private void OnEnable(){
      PlayButtonPressedEvent.OnPlayButtonPressedEvent += onPlayButtonPressedEventHandler;
      GameStateEvents.OnGameStateChanged += OnGameStateChangedEventHandler;
    }
    private void OnDisable(){
      PlayButtonPressedEvent.OnPlayButtonPressedEvent -= onPlayButtonPressedEventHandler;
      GameStateEvents.OnGameStateChanged -= OnGameStateChangedEventHandler;

    }
    #endregion
  }
}
