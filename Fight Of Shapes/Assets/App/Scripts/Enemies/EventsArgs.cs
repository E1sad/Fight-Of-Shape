using System;

namespace SOG.Enemy{
  public class DestroyEnemyEventArgs : EventArgs{
    public EnemyStats ThisEnemy { get; private set; }
    public DestroyEnemyEventArgs(EnemyStats enemy) { ThisEnemy = enemy; }
  }
}
