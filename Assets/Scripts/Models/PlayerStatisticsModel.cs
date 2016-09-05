using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Represent statistics of the player.
/// </summary>
public class PlayerStatisticsModel: CharacterStatisticsModel
{
    public int XpPoints { get; private set; }

    public int XpLevel { get; private set; }

    public PlayerStatisticsModel(int armorLevel, int damageLevel, int healthLevel, int health, int staminaLevel,
        float maxCarriedWeight, float carriedWeight, int xpPoint, int xpLevel) : 
            base(armorLevel, damageLevel, healthLevel, health, staminaLevel,
                maxCarriedWeight, carriedWeight)
    {
        XpPoints = xpPoint;
        XpLevel = xpLevel;
    }
}
