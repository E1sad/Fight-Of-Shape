using SOG.Bullet;
using SOG.UI.GamePlay;
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
      else { _health -= damageOfHit; DamagedPlayerHealhtEvent.Raise(_health);
        Camera.ShakeCameraEvent.Raise(this, new Camera.CameraShakerEventArg(.2f, .1f));}
    }
    private void dead(){ DamagedPlayerHealhtEvent.Raise(0);}

    #endregion

    #region Unity's Methods
    private void Start(){
      DamagedPlayerHealhtEvent.Raise(_health);
    }
    #endregion
  }
}
