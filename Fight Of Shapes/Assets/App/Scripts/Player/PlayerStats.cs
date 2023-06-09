using System.Collections.Generic;
using UnityEngine;

namespace SOG.Player
{
  public class PlayerStats : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private int health;
    [Header("Links")]
    [SerializeField] private GameObject[] shapes;

    //Internal varibales

    #region My Methods
    public void damage(int damageOfHit)
    {
      if ((health - damageOfHit) <= 0) dead();
      health -= damageOfHit;
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
