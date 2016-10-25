using System;
/// <summary>
/// Represent state of the game.
/// </summary>
public class GameStateModel : BaseModel
{
    public GameStateModel(string id, string login, string currentSceneId, string title, DateTime created) :
        base()
    {
        Id = id;
        Login = login;
        CurrentSceneId = currentSceneId;
        Title = title;
        Created = created;
    }

    // specific properties here
    public string Id { get; internal set; }
    public string Login { get; internal set; }
    public string CurrentSceneId { get; internal set; }
    public string Title { get; internal set; }
    public DateTime Created { get; internal set; }

    public override T ConvertToDTO<T>(params object[] data)
    {
        return new GameStateDTO()
        {
            GameStateId = Id,
            Login = this.Login,
            CurrentScneneId = CurrentSceneId,
            Title = this.Title,
            SaveDate = this.Created,
        } as T;
    }
}
