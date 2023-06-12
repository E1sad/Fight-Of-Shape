using SOG.Enemy;
using SOG.Game_Manager;
using System;
using UnityEngine;

namespace SOG.Bullet
{
  public class Bullet : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private float _speed;

    [Header("Links")]
    [SerializeField] private Rigidbody2D _bulletRb;

    //Internal varibales
    private int _damage;
    [HideInInspector] public int Damage{ get { return _damage; } set { _damage = value; }}
    [HideInInspector] public Rigidbody2D BulletRb { get { return _bulletRb; } set { } }
    private bool _isGamePlayState;

    #region My Methods
    private void gameStateHandler(object sender, GameStateChangedEvent eventargs){
      switch (eventargs.CurrentGameState){
        case GameState.IDLE_STATE: restartAndIdleState(); break;
        case GameState.RESTART_STATE: restartAndIdleState(); break;
        case GameState.PAUSE_STATE: pauseState(); break;
        case GameState.PLAY_STATE: gamePlayState(); break;
        default: break;}
    }
    private void restartAndIdleState() { _isGamePlayState = false; }
    private void pauseState() { _isGamePlayState = false; }
    private void gamePlayState() { _isGamePlayState = true; }
    public void SetIsGamePlayState(bool set) { _isGamePlayState = set; }
    private void move(){_bulletRb.velocity = new Vector2(0,_speed*Time.deltaTime);}

    #endregion

    #region Unity's Methods
    private void Update(){
      if (!_isGamePlayState) return;
      move();
    }
    private void Start(){
      _isGamePlayState = true;
    }
    private void OnCollisionEnter2D(Collision2D collision){
      if (collision.gameObject.CompareTag("Enemy")) {
        collision.gameObject.GetComponent<EnemyStats>().damage(_damage);
        DestroyBulletEvent.Raise(this, new DestroyBulletEventArgs(this));}
      if(collision.gameObject.CompareTag("Boundary")){
        DestroyBulletEvent.Raise(this, new DestroyBulletEventArgs(this));}
    }
    private void OnEnable(){
      GameStateEvents.OnGameStateChanged += gameStateHandler;
    }
    private void OnDisable(){
      GameStateEvents.OnGameStateChanged -= gameStateHandler;
    }
    #endregion
  }
}