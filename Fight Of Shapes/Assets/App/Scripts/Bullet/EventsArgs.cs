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

    public DestroyBulletEventArgs(Bullet bullet){
      ThisBullet = bullet;
    }
  }
}
