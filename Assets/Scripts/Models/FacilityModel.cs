using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for all uncollectable objects of the game.
/// </summary>
public class FacilityModel : ThingModel
{
    /// <summary>
    /// Represent some simple state of facility.
    /// </summary>
    public bool IsSwitchOn { get; private set; }

    public FacilityModel(string name, string commandHint, bool isSwitchOn) :
        base(typeof(CharacterModel).ToString(), name, commandHint)
    {
        IsSwitchOn = isSwitchOn;
    }
}
