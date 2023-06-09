using SOG.Bullet;
using SOG.Game_Manager;
using SOG.UI.GamePlay;
using SOG.UI.MainMenu;
using SOG.UI.Shop;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.Player
{
  public class PlayerStats : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private int _health;
    [SerializeField] private AudioClip _damageClip;
    [SerializeField] private AudioClip _deathClip;

    [Header("Links")]
    [SerializeField] private SpriteRenderer _sprite;

    //Internal varibales
    private int _healthRemainder;

    #region My Methods
    public void damage(int damageOfHit)
    {
      if ((_health - damageOfHit) <= 0) dead();
      else { _health -= damageOfHit; DamagedPlayerHealhtEvent.Raise(_health);
        Camera.ShakeCameraEvent.Raise(this, new Camera.CameraShakerEventArg(.2f, .1f));
        Audio_Manager.AudioManager.Instance.PlaySoundClip(_damageClip);}
    }
    private void dead(){
      Audio_Manager.AudioManager.Instance.PlaySoundClip(_deathClip);
      DamagedPlayerHealhtEvent.Raise(0); _health = _healthRemainder; 
      DamagedPlayerHealhtEvent.Raise(_health); }
    private void onPlayButtonPressedEventHandler() { DamagedPlayerHealhtEvent.Raise(_health); }
    private void OnGameStateChangedEventHandler(object sender , GameStateChangedEvent eventArgs) {
      if(eventArgs.CurrentGameState == GameState.PLAY_STATE) DamagedPlayerHealhtEvent.Raise(_health); 
    }
    private void changePlayerStats(PlayerScriptableObject newStats) {
      _health = newStats.Health; _sprite.sprite = newStats.PlayerImage; _healthRemainder = _health;
    }
    #endregion

    #region Unity's Methods
    private void Start(){
      _healthRemainder = _health; //Temporary. When save system implemented you should change that.
    }
    private void OnEnable(){
      PlayButtonPressedEvent.OnPlayButtonPressedEvent += onPlayButtonPressedEventHandler;
      GameStateEvents.OnGameStateChanged += OnGameStateChangedEventHandler;
      PlayerStatsChanged.PlayerStatsCahngedEvent += changePlayerStats;
    }
    private void OnDisable(){
      PlayButtonPressedEvent.OnPlayButtonPressedEvent -= onPlayButtonPressedEventHandler;
      GameStateEvents.OnGameStateChanged -= OnGameStateChangedEventHandler;
      PlayerStatsChanged.PlayerStatsCahngedEvent -= changePlayerStats;
    }
    #endregion
  }
}
