using UnityEngine;

namespace SOG.UI.Credits{
  public class CreditsView : MonoBehaviour{
    //[Header("Variables")]

    [Header("Links")]
    [SerializeField] private CreditsController controller;
    //Internal varibales

    #region My Methods
    public void OnBackButtonPressed(){ controller.BackButtonPressed(); }
    #endregion

    #region Unity's Methods

    #endregion
  }
}
