using UnityEngine;

namespace SOG.Enemy
{
  public class EnemyMovement : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private float speed;

    [Header("Links")]
    [SerializeField] private Rigidbody2D enemyRb;

    //Internal varibales

    #region My Methods
    public void SetSpeed(float _speed) { speed = _speed; }
    #endregion

    #region Unity's Methods
    private void Update()
    {
      enemyRb.velocity = Vector2.up * (-speed)* Time.deltaTime;
    }
    #endregion
  }
}
