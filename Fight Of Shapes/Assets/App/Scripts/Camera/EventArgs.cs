using System;

namespace SOG.Camera{
  public class CameraShakerEventArg : EventArgs{
    public float Duration { private set; get; }
    public float Magnitude { private set; get; }

    public CameraShakerEventArg(float duration, float magnitude){
      Duration = duration; Magnitude = magnitude;}
  }
}
