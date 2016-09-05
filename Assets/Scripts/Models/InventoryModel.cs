using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Represent inventory store for characters of the game.
/// </summary>
public class InventoryModel: IModel
{
    private int maxInventoryItems = 9;

    public InventoryModel(int maxInventoryItems)
    {
        this.maxInventoryItems = maxInventoryItems;
    }

    /// <summary>
    /// Instance of controller
    /// </summary>
    public BaseController Controller { get; private set; }

    /// <summary>
    /// Maximum nubmer of cells of inventory for placing items.
    /// </summary>
    public int MaxInventoryItems {
        get { return maxInventoryItems; }
    }

    public void SetController(BaseController controller)
    {
        if (Controller != null)
            throw new ApplicationException("Controller has been already initialized.");

        Controller = controller;
    }
}
