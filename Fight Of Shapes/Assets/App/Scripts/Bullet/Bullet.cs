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
    [SerializeField] private AudioClip _hitClip;

    [Header("Links")]
    [SerializeField] private Rigidbody2D _bulletRb;
    [SerializeField] private TrailRenderer _trailRenderer;

    //Internal varibales
    private int _damage;
    [HideInInspector] public int Damage{ get { return _damage; } set { _damage = value; }}
    [HideInInspector] public Rigidbody2D BulletRb { get { return _bulletRb; } set { } }
    private bool _isGamePlayState;

    #region My Methods
    public void SetIsGamePlayState(bool set) { _isGamePlayState = set; }
    public void SetTrailColor(float r,float g,float b) {
      _trailRenderer.startColor = new Color32((byte)r, (byte)g, (byte)b, 255 );
      _trailRenderer.endColor = new Color32((byte)r, (byte)g, (byte)b, 0);
    }
    private void move(){_bulletRb.velocity = new Vector2(0,_speed*Time.deltaTime);}

    #endregion

    #region Unity's Methods
    private void Update(){
      if (!_isGamePlayState) return;
      move();
    }
    private void OnCollisionEnter2D(Collision2D collision){
      if (collision.gameObject.CompareTag("Enemy")) {
        collision.gameObject.GetComponent<EnemyStats>().damage(_damage);
        Audio_Manager.AudioManager.Instance.PlaySoundClip(_hitClip);
        _trailRenderer.Clear();
        DestroyBulletEvent.Raise(this, new DestroyBulletEventArgs(this));}
      if(collision.gameObject.CompareTag("Boundary")){
        _trailRenderer.Clear();
        DestroyBulletEvent.Raise(this, new DestroyBulletEventArgs(this));}
    }
    #endregion
  }
}