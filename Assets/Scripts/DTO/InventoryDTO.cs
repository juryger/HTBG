using SQLite4Unity3d;
/// <summary>
/// Data Transfer Object for Inventory.
/// </summary>
public class InventoryDTO : BaseDTO
{
    [PrimaryKey]
    public string InventoryId { get; set; }
    public string Name { get; set; }
    public string CommandHint { get; set; }
    public InventoryKind InventoryType { get; set; }

    public override T ConvertToModel<T>(params object[] data)
    {
        return new InventoryModel(InventoryId, Name, CommandHint, InventoryType) as T;
    }
}

