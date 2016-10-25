using System;
/// <summary>
/// Data Transfer Object for game state.
/// </summary>
public class GameStateDTO : BaseDTO
{
    public string GameStateId { get; set; }
    public string Login { get; set; }
    public string CurrentScneneId { get; set; }
    public string Title { get; set; }
    public DateTime SaveDate { get; set; }

    public override T ConvertToModel<T>(params object[] data)
    {
        return new GameStateModel(
            GameStateId,
            Login,
            CurrentScneneId,
            Title,
            SaveDate) as T;
    }
}
