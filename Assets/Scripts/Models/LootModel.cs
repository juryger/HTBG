using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for all collectible objects of the game.
/// </summary>
public class LootModel : ThingModel
{
    public float Weight { get; private set; }

    public LootModel(string name, string commandHint, float weight) :
        base(typeof(CharacterModel).ToString(), name, commandHint)
    {
        Weight = weight;
    }
}
