using System;
/// <summary>
/// Model for main menu.
/// </summary>
public class MainMenuModel : BaseModel
{
    public MainMenuModel()
    {
    }

    public bool HasAuthenticatedUser
    {
        get
        {
            return null != GameStateManager.Instance.AuthenticatedUser;
        }
    }

    public UserModel AuthenticatedUser
    {
        get
        {
            return GameStateManager.Instance.AuthenticatedUser;
        }
    }

    public override T ConvertToDTO<T>(params object[] data)
    {
        throw new NotImplementedException();
    }
}