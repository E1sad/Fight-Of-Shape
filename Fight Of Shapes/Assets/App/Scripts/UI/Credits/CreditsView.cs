using SOG.Audio_Manager;
using UnityEngine;

namespace SOG.UI.Credits{
  public class CreditsView : MonoBehaviour{
    [Header("Variables")]
    [SerializeField] private AudioClip _buttonClip; 

    [Header("Links")]
    [SerializeField] private CreditsController controller;
    //Internal varibales

    #region My Methods
    public void OnBackButtonPressed(){
      controller.BackButtonPressed(); AudioManager.Instance.PlaySoundClip(_buttonClip); 
    }
    #endregion

    #region Unity's Methods

    #endregion
  }
}
