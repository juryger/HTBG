using System;
using System.Collections.Generic;

/// <summary>
/// Responsible for extended game state which include multiplayer characters, game characters, loot storages.
/// </summary>
public class ExtendedGameState
{
    private Dictionary<string, CharacterModel> characters;
    private Dictionary<string, CharacterModel> multiplayers;
    private Dictionary<string, InventoryModel> lootStorages;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ExtendedGameState()
    {
        characters = new Dictionary<string, CharacterModel>();
        multiplayers = new Dictionary<string, CharacterModel>();
        lootStorages = new Dictionary<string, InventoryModel>();
    }

    /// <summary>
    /// State of all characters of the game.
    /// </summary>
    public IDictionary<string, CharacterModel> Characters
    {
        get
        {
            return characters;
        }
    }

    /// <summary>
    /// State of Loot.
    /// </summary>
    public IDictionary<string, InventoryModel> LootStorages
    {
        get
        {
            return lootStorages;
        }
    }

    #region Characters

    /// <summary>
    /// Add character to current game state.
    /// </summary>
    /// <param name="character">new character</param>
    public void AddCharacter(CharacterModel character)
    {
        // if gamestate contains character, that's mean his state is already actual
        if (characters.ContainsKey(character.Id))
            return;

        characters.Add(character.Id, character);
    }

    /// <summary>
    /// Remove character from current game state.
    /// </summary>
    /// <param name="characterId">character identifier</param>
    public void ResetCharacter(string characterId)
    {
        if (!characters.ContainsKey(characterId))
            return;

        characters.Remove(characterId);
    }

    /// <summary>
    /// Return characters of the required scene.
    /// </summary>
    /// <param name="sceneId">scene identifier</param>
    /// <returns>list of characters</returns>
    public IEnumerable<CharacterModel> GetCharacters(string sceneId)
    {
        var result = new List<CharacterModel>(characters.Values);

        return result.FindAll(x =>
            string.Equals(x.SceneId, sceneId, StringComparison.InvariantCultureIgnoreCase));
    }

    #endregion

    #region Inventory

    /// <summary>
    /// Add inventory storage to current game state.
    /// </summary>
    /// <param name="inventory">inventory</param>
    public void AddInventory(InventoryModel inventory)
    {
        if (LootStorages.ContainsKey(inventory.Id))
            return;

        lootStorages.Add(inventory.Id, inventory);
    }

    /// <summary>
    /// Remove inventory from curretn game state.
    /// </summary>
    /// <param name="inventoryId">inventory identifier</param>
    public void ResetInventory(string inventoryId)
    {
        if (!lootStorages.ContainsKey(inventoryId))
            return;

        lootStorages.Remove(inventoryId);
    }

    /// <summary>
    /// Return inventories of the required scene.
    /// </summary>
    /// <param name="sceneId">scene identifier</param>
    /// <returns>collection of inventories</returns>
    public IEnumerable<InventoryModel> GetInventory(string sceneId)
    {
        throw new NotImplementedException();

        // todo: need to link InventoryLoot with sceneId (ERD - SceneInventory)
        //var result = new List<InventoryModel>(lootStorages.Values);

        //return result.FindAll(x =>
        //    string.Equals(x..SceneId, sceneId, StringComparison.InvariantCultureIgnoreCase));
    }

    #endregion

    #region Multiplayers 

    /// <summary>
    /// Add multiplayer character to current game state.
    /// </summary>
    /// <param name="character">new character</param>
    public void AddOrUpdateMultiplayerCharacter(CharacterModel character)
    {
        // if gamestate contains character, that's mean his state is already actual
        if (multiplayers.ContainsKey(character.Id))
        {
            multiplayers[character.Id].UpdateInternalState(character);
            return;
        }

        multiplayers.Add(character.Id, character);
    }

    /// <summary>
    /// Remove multiplayer character from current game state.
    /// </summary>
    /// <param name="characterId">character identifier</param>
    public void ResetMultiplayerCharacter(string characterId)
    {
        if (!multiplayers.ContainsKey(characterId))
            return;

        var item = multiplayers[characterId];
        item.Dispose();

        multiplayers.Remove(characterId);
    }

    /// <summary>
    /// Return multiplayers for current scene.
    /// </summary>
    /// <param name="sceneId">scene identifier</param>
    /// <returns>list of multiplayers</returns>
    public IEnumerable<CharacterModel> GetMultiplayers(string sceneId)
    {
        var result = new List<CharacterModel>(multiplayers.Values);

        return result.FindAll(x =>
            string.Equals(x.SceneId, sceneId, StringComparison.InvariantCultureIgnoreCase));
    }

    /// <summary>
    /// Return outdated multiplayers for current scene.
    /// </summary>
    /// <param name="sceneId">scene identifier</param>
    /// <param name="syncPlayerIds">actual collection of multiplayer identifiers from Game server</param>
    /// <returns>outdated multiplayers</returns>
    public IEnumerable<CharacterModel> GetOutdatedMultiplayers(string sceneId, IEnumerable<string> syncPlayerIds)
    {
        var result = new List<CharacterModel>();

        var oldMultiplayerIds = new HashSet<string>(multiplayers.Keys);
        oldMultiplayerIds.ExceptWith(syncPlayerIds);

        foreach (var id in oldMultiplayerIds)
        {
            var item = multiplayers[id];

            if (!string.Equals(item.SceneId, sceneId, StringComparison.InvariantCultureIgnoreCase))
                continue;

            result.Add(item);
        }

        return result;
    }

    /// <summary>
    /// Return active multiplayers for current scene.
    /// </summary>
    /// <param name="sceneId">scene identifier</param>
    /// <param name="syncPlayerIds">actual collection of multiplayer identifiers from Game server</param>
    /// <returns>active multiplayers</returns>
    public IEnumerable<CharacterModel> GetActiveMultiplayers(string sceneId, IEnumerable<string> syncPlayerIds)
    {
        var result = new List<CharacterModel>();

        var oldMultiplayerIds = new HashSet<string>(multiplayers.Keys);
        oldMultiplayerIds.IntersectWith(syncPlayerIds);

        foreach (var id in oldMultiplayerIds)
        {
            var item = multiplayers[id];

            if (!string.Equals(item.SceneId, sceneId, StringComparison.InvariantCultureIgnoreCase))
                continue;

            result.Add(item);
        }

        return result;
    }

    /// <summary>
    /// Return new multiplayers for current scene.
    /// </summary>
    /// <param name="sceneId">scene identifier</param>
    /// <param name="syncPlayer">actual collection of multiplayers from Game server</param>
    /// <returns>new multiplayer identifiers</returns>
    public IEnumerable<string> GetNewMultiplayers(string sceneId, IEnumerable<CharacterDTO> syncPlayer)
    {
        var result = new List<string>();

        foreach (var item in syncPlayer)
        {
            if (multiplayers.ContainsKey(item.CharacterId))
                continue;

            if (!string.Equals(item.SceneId, sceneId, StringComparison.InvariantCultureIgnoreCase))
                continue;

            result.Add(item.CharacterId);
        }

        return result;
    }

    #endregion
}
