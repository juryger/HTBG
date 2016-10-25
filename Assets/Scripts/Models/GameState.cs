using System;
using System.Collections.Generic;

/// <summary>
/// Responsible for extended game state which include multiplayer characters, game characters, loot storages.
/// </summary>
public class ExtendedGameState
{
    private Dictionary<string, CharacterModel> characters;
    private Dictionary<string, CharacterModel> multiplayers;
    private Dictionary<string, InventoryItem> inventoryItems;

    public ExtendedGameState()
    {
        characters = new Dictionary<string, CharacterModel>();
        multiplayers = new Dictionary<string, CharacterModel>();
        inventoryItems = new Dictionary<string, InventoryItem>();
    }

    /// <summary>
    /// State of players in multiplayer game.
    /// </summary>
    public IDictionary<string, CharacterModel> Multiplayers
    {
        get
        {
            return multiplayers;
        }
    }

    /// <summary>
    /// State of NPCs.
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
    public Dictionary<string, InventoryItem> InventoryItems
    {
        get
        {
            return inventoryItems;
        }
    }

    public IEnumerable<CharacterModel> GetCharactersForScene(string sceneId)
    {
        throw new NotImplementedException();
    }

    public void ResetCharactersForScene(string sceneId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CharacterModel> GetMultiplayersForScene(string sceneId)
    {
        throw new NotImplementedException();
    }

    public void ResetMultiplayerForScene(string sceneId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CharacterModel> GetInventoryLootForScene(string sceneId)
    {
        throw new NotImplementedException();
    }

    public void ResetInventoryLootForScene(string sceneId)
    {
        throw new NotImplementedException();
    }

    public void AddCharacters(IEnumerable<CharacterModel> characterList)
    {
        foreach (var item in characterList)
        {
            if (Characters.ContainsKey(item.Id))
                continue;

            Characters.Add(item.Id, item);
        }
    }
}
