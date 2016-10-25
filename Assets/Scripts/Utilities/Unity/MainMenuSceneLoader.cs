using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Manages to load main menu scene.
/// </summary>
public class MainMenuSceneLoader : MonoBehaviour
{
    EventSystem system;

    // Use this for initialization
    void Start()
    {
        system = EventSystem.current;

        // Load Scene state
        PrepareSceneState();
    }

    private void PrepareSceneState()
    {
        var mainMenu = GameObject.FindGameObjectWithTag(UnityObjectTagName.MainMenu);
        if (mainMenu != null)
        {
            var view = mainMenu.GetComponent<MainMenuView>();
            var model = new MainMenuModel();
            var viewModel = new MainMenuViewModel(view, model);

            // notify view about current authenticated user
            viewModel.Notify(NotificationName.NotifyViewAuthenticatedUserChanged, this, model.AuthenticatedUser);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null)
            {

                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null)
                    inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret

                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }
        }
    }

}