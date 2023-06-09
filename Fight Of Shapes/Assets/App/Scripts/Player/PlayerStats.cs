using SOG.Bullet;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.Player
{
  public class PlayerStats : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private int _health;
    [Header("Links")]
    [SerializeField] private GameObject[] _shapes;

    //Internal varibales

    #region My Methods
    public void damage(int damageOfHit)
    {
      if ((_health - damageOfHit) <= 0) dead();
      _health -= damageOfHit;
    }

    private void dead()
    {
      Debug.Log("Player is dead!");
    }

    #endregion

    #region Unity's Methods

    #endregion
  }
}
