using System;

/// <summary>
/// Represent Unity position on the scene.
/// </summary>
[Serializable]
public class UnityPosition
{
    public float X { get; private set; }
    public float Y { get; private set; }
    public float Z { get; private set; }

    public UnityPosition(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}
