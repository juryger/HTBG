/// <summary>
/// DTO object for saved game character.
/// </summary>
public class CharacterStateDTO : CharacterDTO
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public CharacterStateDTO()
    {
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="characterDto">character with initial statistics</param>
    public CharacterStateDTO(string gameStateId, CharacterDTO characterDto)
    {
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
    }

    public string GameStateId { get; set; }
}
