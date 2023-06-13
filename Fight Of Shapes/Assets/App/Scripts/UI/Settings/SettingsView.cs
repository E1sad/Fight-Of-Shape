using UnityEngine;

namespace SOG.UI.Settings
{
  public class SettingsView : MonoBehaviour
  {
    //[Header("Variables")]

    [Header("Links")]
    [SerializeField] private SettingsController controller;
    //Internal varibales

    #region My Methods
    public void OnBackButtonPressedEvent() { controller.BackButtonPressed(); }
    #endregion

    #region Unity's Methods

    #endregion
  }
}
