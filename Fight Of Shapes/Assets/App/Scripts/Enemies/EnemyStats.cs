using SOG.Bullet;
using SOG.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SOG.Enemy
{
  public class EnemyStats : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private int _health;
    [SerializeField] private int _damageOfHit;
    [SerializeField] private int _corner;
    [SerializeField] private int _scorePointOfEnemy;
    [SerializeField] private int _punishScorePointOfEnemy;
    [SerializeField] private float _duration;
    [SerializeField] private float _magnitude;
    [SerializeField] private AudioClip _deathClip;

    [Header("Links")]
    [SerializeField] private Slider _healthBar;

    //Internal varibales
    private int _healthRemainder;
    [HideInInspector] public int Corner { get { return _corner; } set { } }
    
    #region My Methods
    public void damage(int damageOfHit){
      SpawnDamageFeedbackEvent.Raise(this,new SpawnDamageFeedbackEventArgs(transform.position,damageOfHit,_corner));
      if (_health - damageOfHit <= 0){
        _healthBar.value = 0; UI.GamePlay.AddScoreEvent.Raise(_scorePointOfEnemy);
        Audio_Manager.AudioManager.Instance.PlaySoundClip(_deathClip); dead();}
      else { _health -= damageOfHit; StartCoroutine(ShakeEnemy()); }
      _healthBar.value = (float)_health/ _healthRemainder;
    }
    private void dead() { 
      _health = _healthRemainder; _healthBar.value = 1;
      DestroyEnemyEvent.Raise(this,new DestroyEnemyEventArgs(this));
    }
    public void RestartState() { _health = _healthRemainder; _healthBar.value = 1; }
    private IEnumerator ShakeEnemy(){
      float original_x = gameObject.transform.position.x;
      float elapsed = 0f;
      while (elapsed < _duration) {
        float x = Random.Range(-1f, 1f) * _magnitude;
        transform.position = new Vector3(original_x + x, transform.position.y,transform.position.z);
        elapsed += Time.deltaTime;
        yield return null;}
      transform.position = new Vector3(original_x, transform.position.y, transform.position.z);
    }
    public void SetHealthRemainder() { _healthRemainder = _health; }
    #endregion

    #region Unity's Methods
    private void OnCollisionEnter2D(Collision2D collision){
      if (collision.gameObject.CompareTag("Player")){
        collision.gameObject.GetComponent<PlayerStats>().damage(_damageOfHit);
        dead();}
      if (collision.gameObject.CompareTag("Boundary")) { dead(); 
        UI.GamePlay.AddScoreEvent.Raise(_punishScorePointOfEnemy);}
    }
    #endregion
  }
}