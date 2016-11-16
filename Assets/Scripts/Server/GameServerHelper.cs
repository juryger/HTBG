using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Helper class for interacting with Game server.
/// </summary>
public class GameServerHelper
{
    // implementation of game server
    private JSNDrop jsnDrop;

    // list of multiplyayers' states retrieved from game state (unprocessed)
    private List<CharacterDTO> multiPlayersUnprocessed;

    private System.Object multiplayersLock = new System.Object();

    /// <summary>
    /// Constructor.
    /// </summary>
    public GameServerHelper()
    {
        jsnDrop = new JSNDrop("http://jsnDrop.com/Q", GeneralName.GameSyncId);

        multiPlayersUnprocessed = new List<CharacterDTO>();
    }

    /// <summary>
    /// Actual players of the game received during sync with game server
    /// </summary>
    public IEnumerable<CharacterDTO> GamePlayersSync
    {
        get
        {
            lock (multiplayersLock)
            {
                return multiPlayersUnprocessed;
            }
        }
    }

    /// <summary>
    /// Register client application at server. 
    /// </summary>
    /// <return>object for Unity3d corutine management</return>
    public IEnumerator RegisterClientApp()
    {
        return jsnDrop.jsnReg(GeneralName.GameSyncDefaultTable, "111888");
    }


    /// <summary>
    /// Cancel registration for all client applications.
    /// </summary>
    /// <returns></returns>
    public IEnumerator DropAppRegistration()
    {
        return jsnDrop.jsnKill(GeneralName.GameSyncDefaultTable, "111888");
    }

    /// <summary>
    /// Synchornizing player state with server.
    /// </summary>
    /// <param name="player">main player of current game</param>
    /// <return>object for Unity3d corutine management</return>
    public IEnumerator SyncPlayerState(CharacterDTO player)
    {
        return jsnDrop.jsnPut(
            GeneralName.GameSyncDefaultTable,
            string.Format("\"characterId\":\"{0}\"", player.CharacterId),
            player);
    }

    /// <summary>
    /// Remove player state from server.
    /// </summary>
    /// <param name="playerId">main player identifier</param>
    /// <return>object for Unity3d corutine management</return>
    public IEnumerator RemovePlayerState(string playerId)
    {
        return jsnDrop.jsnDel(
            GeneralName.GameSyncDefaultTable,
            string.Format("\"characterId\":\"{0}\"", playerId));
    }

    /// <summary>
    /// Request all players for current scene.
    /// </summary>
    /// <param name="sceneId">scene identifier</param>
    /// <return>object for Unity3d corutine management</return>
    public IEnumerator GetGamePlayers(string sceneId)
    {
        return jsnDrop.jsnGet<CharacterDTO>(
            GeneralName.GameSyncDefaultTable,
            string.Empty,
            a =>
            {
                lock (multiplayersLock)
                {
                    multiPlayersUnprocessed.Clear();

                    if (a == null)
                        return;

                    // filter players collection by sceneId and timestamp
                    var scenePlayers = new Dictionary<string, CharacterDTO>();
                    foreach (var item in a)
                    {
                        if (!string.Equals(item.SceneId, sceneId, StringComparison.InvariantCultureIgnoreCase))
                            continue;

                        if (DateTime.UtcNow.AddMinutes(-10) > DateTime.Parse(item.TimeStamp))
                            continue;

                        // bugfix of issue at jsnDrop game server (repetive entities)
                        if (!scenePlayers.ContainsKey(item.CharacterId))
                            scenePlayers.Add(item.CharacterId, item);
                    }

                    multiPlayersUnprocessed.AddRange(scenePlayers.Values);
                }
            });
    }
}
