using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Model for main menu.
/// </summary>
public class MenuModel : IModel
{
    private Dictionary<string, MenuItem> items;

    public MenuModel()
    {
        items = new Dictionary<string, MenuItem>();

        items.Add(MenuItemName.Resume, new MenuItem(MenuItemName.Resume, "Resume game", false));
        items.Add(MenuItemName.New, new MenuItem(MenuItemName.New, "New game", true));
        items.Add(MenuItemName.Multiplayer, new MenuItem(MenuItemName.Multiplayer, "Mulitplayer game", false));
        items.Add(MenuItemName.Load, new MenuItem(MenuItemName.Load, "Load game", false));
        items.Add(MenuItemName.Save, new MenuItem(MenuItemName.Save, "Save game", false));
        items.Add(MenuItemName.Exit, new MenuItem(MenuItemName.Exit, "Exit game", true));
    }

    public BaseController Controller { get; private set; }

    public void SetController(BaseController controller)
    {
        if (Controller != null)
            throw new ApplicationException("Controller has been already initialized.");

        Controller = controller;
    }

    public void UpdateMenuItem(string name, bool isVisible)
    {
        if (!items.ContainsKey(name))
            throw new ApplicationException(
                String.Format("MenuItem with name {0} not found."));

        items[name].IsVisible = isVisible;

        if (Controller != null)
            Controller.Notify(ControllerNotification.MenuItemChanged, this, items[name]);
    }
}
