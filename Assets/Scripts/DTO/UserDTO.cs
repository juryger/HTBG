using SQLite4Unity3d;

/// <summary>
/// Data Transfer Object for User.
/// </summary>
public class UserDTO : BaseDTO
{
    [PrimaryKey]
    public string Login { get; set; }
    public string Password { get; set; }
    public string CharacterId { get; set; }

    public override T ConvertToModel<T>(params object[] data)
    {
        return new UserModel(Login, CharacterId) as T;
    }
}