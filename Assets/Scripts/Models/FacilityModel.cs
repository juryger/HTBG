using System;
/// <summary>
/// Base class for all uncollectable objects of the game.
/// </summary>
public class FacilityModel : ThingModel
{
    /// <summary>
    /// Represent some simple state of facility.
    /// </summary>
    public bool IsSwitchOn { get; private set; }

    public FacilityModel(string id, string name, string commandHint, bool isSwitchOn) :
        base(id, name, commandHint)
    {
        IsSwitchOn = isSwitchOn;
    }

    public override T ConvertToDTO<T>(params object[] data)
    {
        throw new NotImplementedException();
    }
}
