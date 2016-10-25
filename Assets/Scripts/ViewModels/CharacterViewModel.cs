using System;

public class CharacterViewModel : BaseViewModel
{
    public CharacterModel Model { get; private set; }

    public CharacterViewModel(IView view, CharacterModel model) :
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