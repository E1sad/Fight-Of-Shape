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
}
