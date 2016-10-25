/// <summary>
/// Data Transfer Object for association entity between SceneDTO and CharacterDTO (many-to-many).
/// </summary>
public class SceneCharacterDTO
{
    public string SceneId { get; set; }
    public string CharacterId { get; set; }
    public float LocationX { get; set; }
    public float LocationY { get; set; }
}
