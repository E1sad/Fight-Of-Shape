using UnityEngine;

namespace SOG.Player{
  [CreateAssetMenu(fileName = "New Player", menuName = "Scriptable Objects/Player's stats")]
  public class PlayerScriptableObject : ScriptableObject{
    public Sprite PlayerImage;
    public int Health;
    public float frequency;
  }
}
