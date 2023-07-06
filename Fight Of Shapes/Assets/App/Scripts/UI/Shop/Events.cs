using SOG.Bullet;
using SOG.Player;

namespace SOG.UI.Shop{
  public static class ShopButtonPressedEvent{
    public delegate void ShopButtonPressedDelegate(string fromWhere);
    public static event ShopButtonPressedDelegate EventShopButtonPressed;
    public static void Raise(string where) {EventShopButtonPressed?.Invoke(where);}
  }

  public static class BackButtonPressedEvent{
    public static event System.EventHandler<BackButtonFromShopEventArguments> ShopMenuBackButtonPressedEvent;
    public static void Raise(object sender, BackButtonFromShopEventArguments eventArgs) {
      ShopMenuBackButtonPressedEvent?.Invoke(sender, eventArgs);
    }
  }
  public static class AddCoinEvent {
    public static event System.Action<int> EventAddCoin;
    public static void Raise(int coin) { EventAddCoin?.Invoke(coin); }
  }

  public static class PlayerStatsChanged {
    public delegate void PlayerStatsChangedDelegate(PlayerScriptableObject playerObject);
    public static event PlayerStatsChangedDelegate PlayerStatsCahngedEvent;
    public static void Raise(PlayerScriptableObject playerObject) {
      PlayerStatsCahngedEvent?.Invoke(playerObject);
    }
  }

  public static class BulletShapeChanged {
    public delegate void BulletShapeDelegate(BulletScriptableObject bulletShape);
    public static event BulletShapeDelegate BulletShapeChangedEvent;
    public static void Raise(BulletScriptableObject bulletShape) {
      BulletShapeChangedEvent?.Invoke(bulletShape);
    }
  }

  public static class CriticalBulletChance {
    public delegate void CriticalBulletChanceDelegate(int chance);
    public static event CriticalBulletChanceDelegate CriticalBulletChanceEvent;
    public static void Raise(int chance) {CriticalBulletChanceEvent?.Invoke(chance);}
  }
}
