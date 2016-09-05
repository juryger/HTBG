using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Represent statistics of the character.
/// </summary>
public abstract class CharacterStatisticsModel: IModel
{
    public CharacterStatisticsModel(int armorLevel, int damageLevel, int healthLevel, int health, int staminaLevel,
        float maxCarriedWeight, float carriedWeight)
    {
        ArmorLevel = armorLevel;
        HealthLevel = healthLevel;
        Health = health;
        StaminaLevel = staminaLevel;
        MaxCarriedWeight = maxCarriedWeight;
        CarriedWeight = carriedWeight;
    }

    public BaseController Controller { get; private set; }

    public int ArmorLevel { get; private set; }

    public int DamageLevel { get; private set; }

    public int HealthLevel { get; private set; }

    public int Health { get; private set; }

    public int StaminaLevel { get; private set; }

    public int Stamina { get; private set; }

    public float MaxCarriedWeight { get; private set; }

    public float CarriedWeight { get; private set; }

    public void SetController(BaseController controller)
    {
        if (Controller != null)
            throw new ApplicationException("Controller has been already initialized.");

        Controller = controller;
    }
}
