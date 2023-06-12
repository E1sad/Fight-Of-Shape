namespace SOG.Enemy{
  public static class DestroyEnemyEvent{
    public static event System.EventHandler<DestroyEnemyEventArgs> EventDestroyEnemy;

    public static void Raise(object sender, DestroyEnemyEventArgs eventArgs){
      EventDestroyEnemy?.Invoke(sender, eventArgs);
    }
  }
}
