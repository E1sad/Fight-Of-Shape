using SOG.Enemy;
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
    [HideInInspector] private int _damage;
    [HideInInspector] public int Damage{ get { return _damage; } set { _damage = value; }}
    [HideInInspector] private BulletSpawner _spawner = null;

    #region My Methods
    private void move(){_bulletRb.velocity = new Vector2(0,_speed*Time.deltaTime);}
    
    public void SetBulletSpawner(BulletSpawner spawner) { _spawner = spawner; }

    #endregion

    #region Unity's Methods
    private void Update()
    {
      move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.CompareTag("Enemy")) {
        collision.gameObject.GetComponent<EnemyStats>().damage(_damage);
        _spawner.Destroyed(this);
      }
      if(collision.gameObject.CompareTag("Boundary")) { _spawner.Destroyed(this);}
    }
    #endregion
  }
}
