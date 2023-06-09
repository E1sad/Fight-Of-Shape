using SOG.Player;
using UnityEngine;

namespace SOG.Enemy
{
  public class EnemyStats : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private int health;
    [SerializeField] private int damageOfHit;

    /*[Header("Links")]*/
    //Internal varibales

    #region My Methods
    public void damage(int damageOfHit)
    {
      if (health - damageOfHit <= 0) dead();
      health -= damageOfHit;
    }

    private void dead()
    {
      Destroy(gameObject); //Temporary kill of enemy.
    }

    #endregion

    #region Unity's Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.CompareTag("Player")){
        collision.gameObject.GetComponent<PlayerStats>().damage(damageOfHit);
        Destroy(gameObject);}
    }

    #endregion
  }
}
