/// <summary>
/// Represent statistics of the character.
/// </summary>
public abstract class CharacterStatisticsModel : BaseModel
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

    public int ArmorLevel { get; private set; }

    public int DamageLevel { get; private set; }

    public int HealthLevel { get; private set; }

    public int Health { get; private set; }

    public int StaminaLevel { get; private set; }

    public int Stamina { get; private set; }

    public float MaxCarriedWeight { get; private set; }

    public float CarriedWeight { get; private set; }
}
