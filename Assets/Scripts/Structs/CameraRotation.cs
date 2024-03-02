using System;
using UnityEngine;

[Serializable]
public struct CameraRotation
{
    [SerializeField]
    float pitch;

    [SerializeField]
    float yaw;

    public float getPitch()
    {
        return pitch;
    }

    public float getYaw() 
    {
        return yaw;
    }
    public void setPitch(float value)
    {
        pitch = value;
    }

    public void setYaw(float value)
    {
        yaw = value;
    }
}