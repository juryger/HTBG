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

    public ExtendedGameState()
    {
        characters = new Dictionary<string, CharacterModel>();
        multiplayers = new Dictionary<string, CharacterModel>();
        lootStorages = new Dictionary<string, InventoryModel>();
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
    public IDictionary<string, InventoryModel> LootStorages
    {
        get
        {
            return lootStorages;
        }
    }

    public IEnumerable<CharacterModel> GetCharactersForScene(string sceneId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CharacterModel> GetMultiplayersForScene(string sceneId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CharacterModel> GetInventoryLootForScene(string sceneId)
    {
        throw new NotImplementedException();
    }

    public void AddCharacter(CharacterModel character)
    {
        // if gamestate contains character, that's mean his state is already actual
        if (characters.ContainsKey(character.Id))
            return;

        characters.Add(character.Id, character);
    }

    public void ResetCharacter(string characterId)
    {
        if (!characters.ContainsKey(characterId))
            return;

        // todo: notify view about reset
        characters.Remove(characterId);
    }

    public void AddMultiplayer(CharacterModel character)
    {
        if (multiplayers.ContainsKey(character.Id))
        {
            // update position and scene of player
            var playerItem = multiplayers[character.Id];
            playerItem.SetSceneId(playerItem.SceneId);
            playerItem.SetPosition(character.Position);
            return;
        }

        multiplayers.Add(character.Id, character);
    }

    public void ResetMultiplayer(string characterId)
    {
        if (!multiplayers.ContainsKey(characterId))
            return;

        // todo: notify view about reset
        multiplayers.Remove(characterId);
    }

    public void AddInventory(InventoryModel inventory)
    {
        // if gamestate contains character, that's mean his state is already actual
        if (LootStorages.ContainsKey(inventory.Id))
            return;

        lootStorages.Add(inventory.Id, inventory);
    }

    public void ResetInventory(string inventoryId)
    {
        if (!lootStorages.ContainsKey(inventoryId))
            return;

        lootStorages.Remove(inventoryId);
    }
}
