using System;

namespace SOG.Camera{
  public static class ShakeCameraEvent {
    public static event EventHandler<CameraShakerEventArg> CameraShakerEvent;

    public static void Raise(object sender, CameraShakerEventArg eventArg){
      CameraShakerEvent?.Invoke(sender, eventArg);
    }
  }
}
