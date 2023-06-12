using System;

namespace SOG.Bullet{
  public static class SpawnBulletEvent{
    public static event EventHandler<SpawnBulletEventArgs> EventSpawnBullet;

    public static void Raise(object sender, SpawnBulletEventArgs eventArgs){
      EventSpawnBullet?.Invoke(sender, eventArgs);}
  }

  public static class DestroyBulletEvent{
    public static event EventHandler<DestroyBulletEventArgs> EventDestroyBullet;

    public static void Raise(object sender, DestroyBulletEventArgs eventArgs){
      EventDestroyBullet?.Invoke(sender, eventArgs);}
  }
}