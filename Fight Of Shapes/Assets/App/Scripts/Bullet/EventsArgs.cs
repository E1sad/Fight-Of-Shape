using System;
using UnityEngine;

namespace SOG.Bullet{
  public class SpawnBulletEventArgs : EventArgs {
    public Vector2 Position { get; private set; }
    public Quaternion Rotation { get; private set; }
    public SpawnBulletEventArgs(Vector2 position, Quaternion rotation){
      Position = position; Rotation = rotation;}
  }
  public class DestroyBulletEventArgs : EventArgs{
    public Bullet ThisBullet { get; private set; }
    public DestroyBulletEventArgs(Bullet bullet){ThisBullet = bullet;}
  }
  public class SpawnDamageFeedbackEventArgs: EventArgs { 
    public Vector3 Position { private set; get; }
    public int Damage { private set; get; }
    public int Corner { private set; get; }
    public SpawnDamageFeedbackEventArgs(Vector3 position, int damage, int corner){
      Position = position; Damage = damage; Corner = corner;
    }
  }
}
