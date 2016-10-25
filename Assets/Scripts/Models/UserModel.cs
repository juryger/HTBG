/// <summary>
/// Represent user which play game.
/// </summary>
public class UserModel : BaseModel
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="login">login of the user</param>
    /// <param name="characterId">identifier of character associated with user</param>
    public UserModel(string login, string characterId)
    {
        Login = login;
        CharacterId = characterId;
    }

    // specific properties here
    public string Login { get; private set; }
    public string CharacterId { get; private set; }

    public override T ConvertToDTO<T>(params object[] data)
    {
        return new UserDTO()
        {
            Login = Login,
            Password = data[0].ToString(),
            CharacterId = CharacterId,
        } as T;
    }
}
