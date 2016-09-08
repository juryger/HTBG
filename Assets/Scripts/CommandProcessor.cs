using System;
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
            case "pick":
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
            case "t":
            case "turn":
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
                strResult = "Got Hault";

                GameStateManager.Instance.Player.SetMovementVector(new UnityVector2(0, 0));
                break;
            case "a":
            case "action":
                break;
            case "v":
            case "visit":
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

                GameStateManager.Instance.InitializeScene(sceneName, GeneralName.DefaultSpawnPointName);
                SceneManager.LoadScene(sceneName);
                break;
            default:
                break;
        }

        display(strResult);
    }
}