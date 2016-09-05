using UnityEngine;
using System.Collections;

/// <summary>
/// Represent an item from player's inventory.
/// </summary>
public class InventoryItem
{
    /// <summary>
    /// Name of the item.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Brief description of item.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Flag indicates if item is equiped on player.
    /// </summary>
    public bool IsEquiped { get; private set; }

    /// <summary>
    /// Indicate if item is able to be equiped.
    /// </summary>
    public bool IsEquipable { get; private set; }

    /// <summary>
    /// Instance of loot related with this inventory item.
    /// </summary>
    public LootModel LootItem { get; private set; }
}
