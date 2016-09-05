using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Model for npc object.
/// </summary>
public class NpcModel : CharacterModel
{
    // specific properties here
    public NpcModel(string name, string commandHint) : 
        base(name, commandHint)
    {
    }
}
