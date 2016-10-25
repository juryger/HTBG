using System;
/// <summary>
/// Base class for all collectible objects of the game.
/// </summary>
public class LootModel : BaseModel
{
    public LootModel(string id, string name, LootKind kind, float weight)
    {
        Id = id;
        LootType = kind;
        Weight = weight;
    }

    public string Id { get; private set; }
    public float Weight { get; private set; }
    public LootKind LootType { get; private set; }

    public override T ConvertToDTO<T>(params object[] data)
    {
        throw new NotImplementedException();
    }
}
