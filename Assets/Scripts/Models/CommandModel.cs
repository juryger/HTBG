using System;
/// <summary>
/// Model for command that player can trigger in relation to game object.
/// </summary>
public class CommandModel : BaseModel
{
    public CommandModel(string id, string title, char shortcutKey)
    {
        Id = id;
        Title = title;
        ShortcutKey = shortcutKey;
    }

    /// <summary>
    /// Command identifier.
    /// </summary>
    public string Id { get; internal set; }

    /// <summary>
    /// Command title.
    /// </summary>
    public string Title { get; internal set; }

    /// <summary>
    /// Shortcut key for trigger command.
    /// </summary>
    public char ShortcutKey { get; internal set; }

    public override T ConvertToDTO<T>(params object[] data)
    {
        throw new NotImplementedException();
    }
}
