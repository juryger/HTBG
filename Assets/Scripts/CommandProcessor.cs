using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommandProcessor
{
    // todo: move to SceneViewModel

    public void Parse(String pCmdStr, Action<string> display)
    {
        var strResult = "Unable to interpret entered text as command. See a list of commands by enter 'commands on'";

        // tokenise the command
        pCmdStr = pCmdStr.ToLower();
        var parts = pCmdStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length <= 0)
        {
            display(strResult);
            return;
        }

        // process the tokens
        switch (parts[0])
        {
            case "m":
            case "menu":
                if (!GameStateManager.Instance.ActiveScene.CommandHints.ToLower().Contains("menu"))
                    break;

                Time.timeScale = 0;

                // load main menu in additive mode
                SceneManager.LoadScene(SceneName.MainMenu, LoadSceneMode.Additive);
                strResult = "";
                break;
            case "p":
            case "pick":
                if (!GameStateManager.Instance.ActiveScene.CommandHints.ToLower().Contains("pick"))
                    break;

                if (parts.Length == 1)
                    break;

                if (parts[1] == "up")
                {
                    strResult = "Got Pick up";

                    // todo: pick up command
                    // GameModel.Pickup(...);
                }
                break;
            case "g":
            case "go":
                if (!GameStateManager.Instance.ActiveScene.CommandHints.ToLower().Contains("go"))
                    break;

                if (parts.Length == 1)
                    break;

                switch (parts[1])
                {
                    case "north":
                        strResult = "Got Go North";
                        GameStateManager.Instance.Player.SetMovementVector(new UnityVector2(0, 1f));
                        break;
                    case "south":
                        strResult = "Got Go South";
                        GameStateManager.Instance.Player.SetMovementVector(new UnityVector2(0, -1f));
                        break;
                    case "east":
                        strResult = "Got Go East";
                        GameStateManager.Instance.Player.SetMovementVector(new UnityVector2(1f, 0));
                        break;
                    case "west":
                        strResult = "Got Go West";
                        GameStateManager.Instance.Player.SetMovementVector(new UnityVector2(-1f, 0));
                        break;
                    default:
                        break;
                }
                break;
            case "r":
            case "run":
                if (!GameStateManager.Instance.ActiveScene.CommandHints.ToLower().Contains("run"))
                    break;

                if (parts.Length == 1)
                    break;

                switch (parts[1])
                {
                    case "north":
                        strResult = "Got Run North";
                        GameStateManager.Instance.Player.SetMovementVector(new UnityVector2(0, 1f), true);
                        break;
                    case "south":
                        strResult = "Got Run South";
                        GameStateManager.Instance.Player.SetMovementVector(new UnityVector2(0, -1f), true);
                        break;
                    case "east":
                        strResult = "Got Run East";
                        GameStateManager.Instance.Player.SetMovementVector(new UnityVector2(1f, 0), true);
                        break;
                    case "west":
                        strResult = "Got Run West";
                        GameStateManager.Instance.Player.SetMovementVector(new UnityVector2(-1f, 0), true);
                        break;
                    default:
                        break;
                }
                break;
            case "t":
            case "turn":
                if (!GameStateManager.Instance.ActiveScene.CommandHints.ToLower().Contains("turn"))
                    break;

                if (parts.Length == 1)
                    break;

                switch (parts[1])
                {
                    case "north":
                        strResult = "Got Turn North";

                        GameStateManager.Instance.Player.SetMovementVector(new UnityVector2(0, 0.1f));
                        break;
                    case "south":
                        strResult = "Got Turn South";

                        GameStateManager.Instance.Player.SetMovementVector(new UnityVector2(0, -0.1f));
                        break;
                    case "east":
                        strResult = "Got Turn East";

                        GameStateManager.Instance.Player.SetMovementVector(new UnityVector2(0.1f, 0));
                        break;
                    case "west":
                        strResult = "Got Turn West";

                        GameStateManager.Instance.Player.SetMovementVector(new UnityVector2(-0.1f, 0));
                        break;
                    default:
                        break;
                }
                break;
            case "h":
            case "hault":
                if (!GameStateManager.Instance.ActiveScene.CommandHints.ToLower().Contains("hault"))
                    break;

                strResult = "Got Hault";

                GameStateManager.Instance.Player.SetMovementVector(new UnityVector2(0, 0));
                break;
            case "a":
            case "action":
                if (!GameStateManager.Instance.ActiveScene.CommandHints.ToLower().Contains("action"))
                    break;

                break;
            case "v":
            case "visit":
                if (!GameStateManager.Instance.ActiveScene.CommandHints.ToLower().Contains("visit"))
                    break;

                if (parts.Length == 1)
                    break;

                var sceneNumber = 0;
                var sceneName = String.Empty;
                if (int.TryParse(parts[1], out sceneNumber))
                {
                    if (sceneNumber == 1)
                        sceneName = SceneName.SkeletonInn;
                    else if (sceneNumber == 2)
                        sceneName = SceneName.SkeletonInnSuburbs;
                }

                GameStateManager.Instance.SetActiveSpawnPoint(GeneralName.DefaultSpawnPointName);
                SceneManager.LoadScene(sceneName);
                break;
            default:
                break;
        }

        display(strResult);
    }
}