using SQLite4Unity3d;
using System;
using UnityEngine;

/// <summary>
/// Data Transfer Object for Character.
/// </summary>
[Serializable]
public class CharacterDTO : BaseDTO
{
    [SerializeField]
    private string characterId;
    [SerializeField]
    private string inventoryId;
    [SerializeField]
    private string sceneId;
    [SerializeField]
    private CharacterKind characterType;
    [SerializeField]
    private string commandHint;
    [SerializeField]
    private string name;
    [SerializeField]
    private CharacterGender gender;
    [SerializeField]
    private int age;
    [SerializeField]
    private float locationX;
    [SerializeField]
    private float locationY;
    [SerializeField]
    private int armorLevel;
    [SerializeField]
    private int damageLevel;
    [SerializeField]
    private int healthLevel;
    [SerializeField]
    private int staminaLevel;
    [SerializeField]
    private float carriedWeightLevel;
    [SerializeField]
    private int xpLevel;
    [SerializeField]
    private int health;
    [SerializeField]
    private int stamina;
    [SerializeField]
    private int xpPoints;
    [SerializeField]
    private string timeStamp;

    [PrimaryKey]
    public string CharacterId { get { return characterId; } set { characterId = value; } }
    public string InventoryId { get { return inventoryId; } set { inventoryId = value; } }
    public string SceneId { get { return sceneId; } set { sceneId = value; } }
    public CharacterKind CharacterType { get { return characterType; } set { characterType = value; } }
    public string CommandHint { get { return commandHint; } set { commandHint = value; } }

    public string Name { get { return name; } set { name = value; } }
    public CharacterGender Gender { get { return gender; } set { gender = value; } }
    public int Age { get { return age; } set { age = value; } }

    public float LocationX { get { return locationX; } set { locationX = value; } }
    public float LocationY { get { return locationY; } set { locationY = value; } }

    public int ArmorLevel { get { return armorLevel; } set { armorLevel = value; } }
    public int DamageLevel { get { return damageLevel; } set { damageLevel = value; } }
    public int HealthLevel { get { return healthLevel; } set { healthLevel = value; } }
    public int StaminaLevel { get { return staminaLevel; } set { staminaLevel = value; } }
    public float CarriedWeightLevel { get { return carriedWeightLevel; } set { carriedWeightLevel = value; } }
    public int XpLevel { get { return xpLevel; } set { xpLevel = value; } }
    public int Health { get { return health; } set { health = value; } }
    public int Stamina { get { return stamina; } set { stamina = value; } }
    public int XpPoints { get { return xpPoints; } set { xpPoints = value; } }
    public string TimeStamp { get { return timeStamp; } set { timeStamp = value; } }

    public override T ConvertToModel<T>(params object[] data)
    {
        var inventory = (InventoryModel)data[0];
        if (inventory == null)
            throw new ArgumentNullException("data[0]");

        var model = new CharacterModel(
            CharacterId,
            Name,
            CommandHint,
            CharacterType,
            SceneId,
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
            inventory);

        model.SetPosition(new UnityPosition(LocationX, LocationY, 0));

        return model as T;
    }
}
