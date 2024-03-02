using System;
using UnityEngine;

[Serializable]
public struct CameraAngle
{
    [SerializeField]
    float min;

    [SerializeField]
    float max;

    public float getMin()
    {
        return min;
    }

    public float getMax()
    {
        return max;
    }
}