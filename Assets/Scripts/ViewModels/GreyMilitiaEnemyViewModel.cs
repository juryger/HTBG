using System;

public class GreyMilitiaEnemyViewModel : BaseViewModel
{
    public GreyMilitiaEnemyModel Model { get; private set; }

    public GreyMilitiaEnemyViewModel(IView view, GreyMilitiaEnemyModel model) :
        base(view, model)
    {
        Model = model;
        Model.SetViewModel(this);
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        throw new NotImplementedException();
    }
}