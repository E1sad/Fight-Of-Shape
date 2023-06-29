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
    public void SetIsGamePlayState(bool set) { _isGamePlayState = set; }
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
        DestroyBulletEvent.Raise(this, new DestroyBulletEventArgs(this));}
      if(collision.gameObject.CompareTag("Boundary")){
        DestroyBulletEvent.Raise(this, new DestroyBulletEventArgs(this));}
    }
    #endregion
  }
}