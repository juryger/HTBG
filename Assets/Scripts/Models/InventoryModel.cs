/// <summary>
/// Represent inventory store for characters of the game.
/// </summary>
public class InventoryModel : BaseModel
{
    private int maxInventoryItems = 9;

    public InventoryModel(int maxInventoryItems)
    {
        this.maxInventoryItems = maxInventoryItems;
    }

    /// <summary>
    /// Maximum nubmer of cells of inventory for placing items.
    /// </summary>
    public int MaxInventoryItems
    {
        get { return maxInventoryItems; }
    }
}
