using UnityEngine;

namespace SOG.UI.MainMenu{
  public class MainMenuView : MonoBehaviour{
    /*[Header("Variables")]*/
    [Header("Links")]
    [SerializeField] private MainMenuController controller;

    //Internal varibales

    #region My Methods
    public void OnPlayButtonPressed(){controller.OnPlayButtonPressed();}
    public void OnShopMenuButtonpressed(){controller.OnShopMenuButtonpressed();}
    public void OnSettingsButtonPressed(){controller.OnSettingsButtonPressed();}
    public void OnCreditsButtonPressed(){ controller.OnCreditsButtonPressed();}
    #endregion

    #region Unity's Methods

    #endregion
  }
}