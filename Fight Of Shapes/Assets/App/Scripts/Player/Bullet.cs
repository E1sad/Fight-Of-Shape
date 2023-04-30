using UnityEngine;

namespace SOG
{
  public class Bullet : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private float _damage; 
    [SerializeField] private float _speed;

    [Header("Links")]
    [SerializeField] private Rigidbody2D _bulletRb;

    //Internal varibales
    [HideInInspector] public float Damage{ get { return _damage; } set { _damage = value; }}

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
    #endregion
  }
}
