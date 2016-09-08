public class MenuViewModel : BaseViewModel
{
    public MenuModel Model { get; private set; }

    public MenuViewModel(IView view, MenuModel model)
        : base(view, model)
    {
        Model = model;
        Model.SetViewModel(this);
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        switch (eventPath)
        {
            case ViewModelNotification.MenuItemChanged:
                var menuItem = data[0] as MenuItem;
                View.Notify(eventPath, source, menuItem.Name, menuItem.IsVisible);
                break;
            default:
                break;
        }
    }
}