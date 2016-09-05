﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Main character model.
/// </summary>
public class PlayerModel : CharacterModel
{    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name">name of the player</param>
    /// <param name="playerAge">age of the player</param>
    /// <param name="height">height of the player</param>
    /// <param name="weight">weight</param>
    public PlayerModel(string name, int playerAge, int playerHeight, int playerWeight) : 
        base(typeof(PlayerModel).ToString(), name, string.Empty)
    {
        PlayerAge = playerAge;
        PlayerHeight = playerHeight;
        PlayerWeight = playerWeight;
    }

    // specific properties here
    public int PlayerAge { get; private set; }
    public int PlayerHeight { get; private set; }
    public int PlayerWeight { get; private set; }
}
