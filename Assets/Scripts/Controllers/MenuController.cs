public class MenuController : BaseController
{
    public MenuModel Model { get; private set; }

    public MenuController(IView view, MenuModel model)
        : base(view, model)
    {
        Model = model;
        Model.SetController(this);
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        switch (eventPath)
        {
            case ControllerNotification.MenuItemChanged:
                var menuItem = data[0] as MenuItem;
                View.Notify(eventPath, source, menuItem.Name, menuItem.IsVisible);
                break;
            default:
                break;
        }
    }
}