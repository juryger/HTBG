using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Unity script for main menu screen.
/// </summary>
public class MainMenuView : MonoBehaviour, IView
{
    public BaseViewModel ViewModel { get; private set; }

    public void Notify(string eventPath, object source, params object[] data)
    {
        switch (eventPath)
        {
            case NotificationName.NotifyViewAuthenticatedUserChanged:
                UserModel user = data != null && data[0] != null ?
                    (UserModel)data[0] :
                    null;

                var userGreetingObject = GameObject.FindGameObjectWithTag(UnityObjectTagName.AuthenticatedUserGreeting);
                if (userGreetingObject != null)
                {
                    var greetingText = userGreetingObject.GetComponent<Text>();
                    greetingText.text = user != null ?
                        String.Format("Greeting, {0}!", user.Login) :
                        "Greeting!";
                }

                if (user == null)
                    NavigateToMainMenuScreen(MainMenuScreen.AuthenticationMenuScreen);
                else
                    NavigateToMainMenuScreen(MainMenuScreen.GamePauseMenuScreen);

                break;
            case NotificationName.NotifyViewUserAuthenticationFailed:
            case NotificationName.NotifyUserRegisterFailed:
                SetValidationMessage(data[0].ToString());
                break;
            case NotificationName.NotifyGameSavedSuccessfully:
                ResumeGame();
                break;
        }
    }

    private void SetValidationMessage(string message)
    {
        foreach (var text in gameObject.GetComponentsInChildren<Text>())
        {
            if (text.name == "ValidationText")
            {
                text.text = message;
                break;
            }
        }
    }

    public void SetViewModel(BaseViewModel viewModel)
    {
        if (ViewModel != null)
            throw new ApplicationException("ViewModel has been already initialized.");

        ViewModel = viewModel;
    }

    public void Dispose()
    {
        ViewModel = null;
    }

    /// <summary>
    /// Authenticate existing user.
    /// </summary>
    /// <param name="loginScreen">screen which contains login parameters</param>
    public void LoginUser(Canvas loginScreen)
    {
        SetValidationMessage(string.Empty);

        string login = "", password = "";
        foreach (var input in loginScreen.GetComponentsInChildren<InputField>())
        {
            switch (input.name)
            {
                case "LoginInputField":
                    login = input.text;
                    break;
                case "PasswordInputField":
                    password = input.text;
                    break;
            }
        }

        // authenticate user
        ViewModel.Notify(NotificationName.LoginExistingUser, this, login, password);
    }

    /// <summary>
    /// Register new user.
    /// </summary>
    /// <param name="loginScreen">screen which contains register new user parameters</param>
    public void RegisterUser(Canvas loginScreen)
    {
        SetValidationMessage(string.Empty);

        string login = "", password = "", repassword = "";
        int age = 0;
        CharacterGender gender = CharacterGender.Male;

        foreach (var input in loginScreen.GetComponentsInChildren<InputField>())
        {
            switch (input.name)
            {
                case "LoginInputField":
                    login = input.text;
                    break;
                case "PasswordInputField":
                    password = input.text;
                    break;
                case "ReenterPasswordInputField":
                    repassword = input.text;
                    break;
                case "AgeInputField":
                    int.TryParse(input.text, out age);
                    break;
            }
        }

        if (password != repassword)
        {
            SetValidationMessage("Passwords mismatched");
            return;
        }

        var genderDropDown = loginScreen.GetComponentInChildren<Dropdown>();
        if (genderDropDown != null && genderDropDown.name == "GenderDropdown")
            gender = (CharacterGender)genderDropDown.value;

        // register user
        ViewModel.Notify(NotificationName.RegisterNewUser, this, login, password, age, ((int)gender).ToString());
    }

    public void LogoutUser()
    {
        SetValidationMessage(string.Empty);

        ViewModel.Notify(NotificationName.LogoutUser, this);

        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// Load last save game command handler.
    /// </summary>
    public void LoadLastSavedGame()
    {
        SetValidationMessage(string.Empty);

        ViewModel.Notify(NotificationName.LoadLastSavedGame, this);

        ResumeGame();
    }

    /// <summary>
    /// Save game command handler.
    /// </summary>
    public void SaveGame()
    {
        SetValidationMessage(string.Empty);

        ViewModel.Notify(NotificationName.SaveGame, this);
    }

    /// <summary>
    /// Terminate game.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Navigate to authentication menu screen.
    /// </summary>
    public void GoToAuthenticationMenuScreen()
    {
        NavigateToMainMenuScreen(MainMenuScreen.AuthenticationMenuScreen);
    }

    /// <summary>
    /// Navigate to login screen.
    /// </summary>
    public void GoToLoginScreen()
    {
        NavigateToMainMenuScreen(MainMenuScreen.LoginPlayerScreen);

        foreach (var text in gameObject.GetComponentsInChildren<Text>())
        {
            if (text.name == "ValidationText")
            {
                text.text = string.Empty;
                break;
            }
        }
    }

    /// <summary>
    /// Navigate to register new user screen.
    /// </summary>
    public void GoToRegisterUserScreen()
    {
        NavigateToMainMenuScreen(MainMenuScreen.NewPlayerScreen);

        foreach (var text in gameObject.GetComponentsInChildren<Text>())
        {
            if (text.name == "ValidationText")
            {
                text.text = string.Empty;
                break;
            }
        }
    }

    /// <summary>
    /// Naviage to required main menu screen.
    /// </summary>
    /// <param name="screenName">screen name</param>
    private void NavigateToMainMenuScreen(string screenName)
    {
        if (String.IsNullOrEmpty(screenName))
            return;

        var mainMenuRoot = GameObject.FindGameObjectWithTag(UnityObjectTagName.MainMenu);
        if (mainMenuRoot != null)
        {
            foreach (var canvas in mainMenuRoot.GetComponentsInChildren<Canvas>(true))
            {
                if (canvas.name == MainMenuScreen.LogoScreen)
                    continue;

                if (canvas.name == screenName)
                {

                    canvas.gameObject.SetActive(true);
                    continue;
                }

                canvas.gameObject.SetActive(false);
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;

        // load main menu in additive mode
        SceneManager.UnloadScene(SceneName.MainMenu);
    }
}
