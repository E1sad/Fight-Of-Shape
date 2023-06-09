using SOG.Enemy;
using UnityEngine;

namespace SOG
{
  public class Bullet : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private int _damage; 
    [SerializeField] private float _speed;

    [Header("Links")]
    [SerializeField] private Rigidbody2D _bulletRb;

    //Internal varibales
    [HideInInspector] public int Damage{ get { return _damage; } set { _damage = value; }}

    #region My Methods
    private void move(){
      _bulletRb.velocity = new Vector2(0,_speed*Time.deltaTime);
    }

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
        Destroy(gameObject);
      }
      if(collision.gameObject.CompareTag("Boundary")) {Destroy(gameObject);}
    }
    #endregion
  }
}
