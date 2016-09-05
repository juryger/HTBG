using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Base class for all creatures of the game.
/// </summary>
public class CharacterModel : ThingModel
{    
    public CharacterModel(string name, string commandHint) : 
        base(typeof(CharacterModel).ToString(), name, commandHint)
    {        
    }

    public CharacterModel(string key, string name, string commandHint) :
    base(key, name, commandHint)
    {
    }

    // specific properties here
}
