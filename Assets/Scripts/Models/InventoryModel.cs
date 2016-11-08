/// <summary>
/// Represent inventory store for characters of the game.
/// </summary>
public class InventoryModel : ThingModel
{
    public InventoryModel(string Id, string name, string commandHind, InventoryKind kind)
        : base(Id, name, commandHind)
    {
        MaxInventoryItems = 9;
        InventoryType = kind;
    }

    /// <summary>
    /// Inventory type.
    /// </summary>
    public InventoryKind InventoryType { get; private set; }

    /// <summary>
    /// Maximum nubmer of cells of inventory for placing items.
    /// </summary>
    public int MaxInventoryItems { get; private set; }

    public override T ConvertToDTO<T>(params object[] data)
    {
        return new InventoryDTO()
        {
            InventoryId = Id,
            InventoryType = InventoryType,
            Name = Name,
            CommandHint = CommandHint,
        } as T;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            //
        }

        base.Dispose(disposing);
    }
}
