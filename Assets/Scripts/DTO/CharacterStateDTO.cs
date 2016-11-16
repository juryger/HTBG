using SQLite4Unity3d;
using System;
/// <summary>
/// DTO object for saved game character.
/// </summary>
public class CharacterStateDTO : BaseDTO
{
    public CharacterStateDTO()
    {
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="characterDto">character with initial statistics</param>
    public CharacterStateDTO(string gameStateId, CharacterDTO characterDto)
    {
        Name = characterDto.Name;
        Age = characterDto.Age;
        Gender = characterDto.Gender;
        CharacterType = characterDto.CharacterType;
        CommandHint = characterDto.CommandHint;
        GameStateId = gameStateId;
        SceneId = characterDto.SceneId;
        CharacterId = characterDto.CharacterId;
        InventoryId = characterDto.InventoryId;
        ArmorLevel = characterDto.ArmorLevel;
        DamageLevel = characterDto.DamageLevel;
        CarriedWeightLevel = characterDto.CarriedWeightLevel;
        XpLevel = characterDto.XpLevel;
        Health = HealthLevel = characterDto.HealthLevel;
        Stamina = StaminaLevel = characterDto.StaminaLevel;
        LocationX = characterDto.LocationX;
        LocationY = characterDto.LocationY;
        TimeStamp = characterDto.TimeStamp;
    }

    [PrimaryKey]
    public string GameStateId { get; set; }
    // note: also should be primary key, but not supported
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
    public string TimeStamp { get; set; }

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
