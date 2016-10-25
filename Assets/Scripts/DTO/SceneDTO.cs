using SQLite4Unity3d;

/// <summary>
/// Data Transfer Object for Scene.
/// </summary>
public class SceneDTO : BaseDTO
{
    [PrimaryKey]
    public string SceneId { get; set; }
    public string Title { get; set; }
    public string CommandHints { get; set; }

    public override T ConvertToModel<T>(params object[] data)
    {
        return new SceneModel(
            SceneId,
            Title,
            CommandHints) as T;
    }

    #region Helper methods

    public string[] GetCommandHintsAsArray()
    {
        return CommandHints.Split(';');
    }

    #endregion

}