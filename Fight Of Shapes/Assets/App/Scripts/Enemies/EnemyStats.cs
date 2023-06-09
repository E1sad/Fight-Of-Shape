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
    [HideInInspector] public int Corner { get { return _corner; } set { }}

    /*[Header("Links")]*/
    //Internal varibales
    private EnemySpawner _spawner;

    #region My Methods
    public void damage(int damageOfHit)
    {if (_health - damageOfHit <= 0) dead();
      _health -= damageOfHit;
    }

    public void SetSpawner(EnemySpawner spawner) { _spawner = spawner; }

    private void dead(){_spawner.Destroyed(this);}

    #endregion

    #region Unity's Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.CompareTag("Player")){
        collision.gameObject.GetComponent<PlayerStats>().damage(_damageOfHit);
        dead();}
      if (collision.gameObject.CompareTag("Boundary")) { dead(); }
    }

    #endregion
  }
}