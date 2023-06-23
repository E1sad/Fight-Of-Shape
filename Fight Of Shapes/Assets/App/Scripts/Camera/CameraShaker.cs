using System.Collections;
using UnityEngine;

namespace SOG.Camera{
  public class CameraShaker : MonoBehaviour{
    #region My Methods
    private IEnumerator Shake(float duration, float magnitude) {
      Vector3 originalPos = transform.position;
      float elapsed = 0f;
      while (elapsed < duration){
        float x = Random.Range(-1f, 1f) * magnitude;
        float y = Random.Range(-1f, 1f) * magnitude;
        transform.position = new Vector3(x, y, originalPos.z);
        elapsed += Time.deltaTime;
        yield return null;}
      transform.position = originalPos;
    }
    private void CameraShakerEventHandler(object sender, CameraShakerEventArg eventArgs) {
      StartCoroutine(Shake(eventArgs.Duration, eventArgs.Magnitude));}
    #endregion

    #region Unity's Methods
    private void OnEnable(){
      ShakeCameraEvent.CameraShakerEvent += CameraShakerEventHandler;
    }
    private void OnDisable()
    {
      ShakeCameraEvent.CameraShakerEvent -= CameraShakerEventHandler;
    }
    #endregion
  }
}
