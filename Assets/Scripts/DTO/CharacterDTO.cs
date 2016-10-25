using SQLite4Unity3d;
using System;

/// <summary>
/// Data Transfer Object for Character.
/// </summary>
public class CharacterDTO : BaseDTO
{
    [PrimaryKey]
    public string CharacterId { get; set; }
    public string InventoryId { get; set; }
    public string SceneId { get; set; }
    public CharacterKind CharacterType { get; set; }
    public string CommandHint { get; set; }

    public string Name { get; set; }
    public CharacterGender Gender { get; set; }
    public int Age { get; set; }

    public float LocationX { get; set; }
    public float LocationY { get; set; }

    public int ArmorLevel { get; set; }
    public int DamageLevel { get; set; }
    public int HealthLevel { get; set; }
    public int StaminaLevel { get; set; }
    public float CarriedWeightLevel { get; set; }
    public int XpLevel { get; set; }
    public int Health { get; set; }
    public int Stamina { get; set; }
    public int XpPoints { get; set; }

    public override T ConvertToModel<T>(params object[] data)
    {
        var inventory = (InventoryModel)data[0];
        if (inventory == null)
            throw new ArgumentNullException("data[0]");

        return new CharacterModel(
            CharacterId,
            Name,
            CommandHint,
            CharacterType,
            Age,
            Gender,
            new CharacterStatisticsModel(
                ArmorLevel,
                DamageLevel,
                HealthLevel,
                StaminaLevel,
                CarriedWeightLevel,
                XpLevel,
                0),
            inventory) as T;
    }
}
