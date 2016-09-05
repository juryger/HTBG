using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Main menu item.
/// </summary>
public class MenuItem
{
    /// <summary>
    /// Name of menu item.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Title of menu item.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// Indicate if item is visible.
    /// </summary>
    public bool IsVisible { get; set; }

    public MenuItem(string name, string title, bool isVisible)
    {
        Name = name;
        Title = title;
        IsVisible = isVisible;
    }
}
