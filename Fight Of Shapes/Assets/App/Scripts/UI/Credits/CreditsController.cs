using UnityEngine;
using SOG.UI.MainMenu;

namespace SOG.UI.Credits{
  public class CreditsController : MonoBehaviour{
    [Header("Links")]
    [SerializeField] private CreditsView view;

    #region My Methods
    public void BackButtonPressed() { BackButtonPressedEvent.Raise(); view.gameObject.SetActive(false); }
    private void CreditsButtonPressedEventHandler() { view.gameObject.SetActive(true); }
    #endregion

    #region Unity's Methods
    private void OnEnable(){
      CreditsButtonPressedEvent.OnCreditsButtonPressed += CreditsButtonPressedEventHandler;
    }
    private void OnDisable(){
      CreditsButtonPressedEvent.OnCreditsButtonPressed -= CreditsButtonPressedEventHandler;
    }
    #endregion
  }
}
