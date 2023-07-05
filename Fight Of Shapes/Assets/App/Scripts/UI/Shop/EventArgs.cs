using System;

namespace SOG.UI.Shop{
  public class BackButtonFromShopEventArguments : EventArgs{
    public string PreviousPage { private set; get; }
    public BackButtonFromShopEventArguments(string previousPage) { PreviousPage = previousPage; }
  }
}
