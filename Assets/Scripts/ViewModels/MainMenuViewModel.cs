using UnityEngine.SceneManagement;

public class MainMenuViewModel : BaseViewModel
{
    public MainMenuModel Model { get; private set; }

    public MainMenuViewModel(IView view, MainMenuModel model)
        : base(view, model)
    {
        Model = model;
        Model.SetViewModel(this);
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        UserModel user = null;
        switch (eventPath)
        {
            case NotificationName.NotifyViewAuthenticatedUserChanged:
                user = data != null ? data[0] as UserModel : null;
                View.Notify(eventPath, source, user);
                break;
            case NotificationName.LoginExistingUser:
                if (!GameStateManager.Instance.LoginExistingUser(data[0].ToString(), data[1].ToString()))
                {
                    View.Notify(NotificationName.NotifyViewUserAuthenticationFailed, this, "Authentication failed, check that your login and password are correct.");
                    return;
                }

                View.Notify(NotificationName.NotifyViewAuthenticatedUserChanged, this, GameStateManager.Instance.AuthenticatedUser);

                // load last saved game
                GameStateManager.Instance.LoadLastSavedGame();

                // navigate to scene
                SceneManager.LoadScene(GameStateManager.Instance.ActiveScene.Name);
                break;
            case NotificationName.RegisterNewUser:
                if (!GameStateManager.Instance.RegisterNewUser(
                    data[0].ToString(),
                    data[1].ToString(),
                    int.Parse(data[2].ToString()),
                    (CharacterGender)int.Parse(data[3].ToString())))
                {
                    View.Notify(NotificationName.NotifyViewUserAuthenticationFailed, this, "Registration failed, probably user with such login already registered.");
                    return;
                }

                View.Notify(NotificationName.NotifyViewAuthenticatedUserChanged, this, GameStateManager.Instance.AuthenticatedUser);

                // navigate to scene
                SceneManager.LoadScene(GameStateManager.Instance.ActiveScene.Name);
                break;
            case NotificationName.LogoutUser:
                GameStateManager.Instance.LogoutUser();

                // navigate to main menu scene (because current scene is gameplay scene with additive menu scene)
                SceneManager.LoadScene(SceneName.MainMenu);

                break;
            case NotificationName.LoadLastSavedGame:
                GameStateManager.Instance.LoadLastSavedGame();

                // navigate to scene
                SceneManager.LoadScene(GameStateManager.Instance.ActiveScene.Name);

                break;
            case NotificationName.SaveGame:
                GameStateManager.Instance.SaveGame();
                View.Notify(NotificationName.NotifyGameSavedSuccessfully, this, "Games saved.");
                break;
            default:
                break;
        }
    }
}