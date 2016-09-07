using System;

/// <summary>
/// Represent Unity vector of movement.
/// </summary>
[Serializable]
public class UnityVector2
{
    public float X { get; private set; }
    public float Y { get; private set; }

    public UnityVector2(float x, float y)
    {
        X = x;
        Y = y;
    }
}
