using SOG.Player;
using UnityEngine;

namespace SOG.Enemy
{
  public class EnemyStats : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private int _health;
    [SerializeField] private int _damageOfHit;
    [SerializeField] private int _corner;
    [SerializeField] private int _scorePointOfEnemy;
    [HideInInspector] public int Corner { get { return _corner; } set { }}

    //Internal varibales
    [SerializeField] private int _healthRemainder;

    #region My Methods
    public void damage(int damageOfHit){
      if (_health - damageOfHit <= 0) {
        SOG.UI.GamePlay.AddScoreEvent.Raise(_scorePointOfEnemy); dead(); }
      else _health -= damageOfHit;
    }
    private void dead() { _health = _healthRemainder; DestroyEnemyEvent.Raise(this,new DestroyEnemyEventArgs(this));}
    #endregion

    #region Unity's Methods
    private void OnCollisionEnter2D(Collision2D collision){
      if (collision.gameObject.CompareTag("Player")){
        collision.gameObject.GetComponent<PlayerStats>().damage(_damageOfHit);
        dead();}
      if (collision.gameObject.CompareTag("Boundary")) { dead(); }
    }
    private void Start(){
      _healthRemainder = _health;
    }
    #endregion
  }
}