using UnityEngine;

namespace SOG.Bullet{
  [CreateAssetMenu(fileName = "New Bullet", menuName =("Scriptable Objects/Bullet's ScriptableObject"))]
  public class BulletScriptableObject : ScriptableObject{
    public Sprite BulletShape;
    public int Damage;
  }
}
