/// <summary>
/// Consts for notification names between View and ViewModel.
/// </summary>
public class NotificationName
{
    public const string NotifyViewSyncState = "notify.view.sync";
    public const string NotifyViewAuthenticatedUserChanged = "notify.view.user.auth.changed";
    public const string NotifyViewUserAuthenticationFailed = "notify.view.user.auth.failed";
    public const string NotifyUserRegisterFailed = "notify.view.user.register.failed";
    public const string NotifyGameSavedSuccessfully = "notify.view.game.save.success";

    public const string CharacterPositionChanged = "character.position.changed";
    public const string CharacterMovementVectorChanged = "character.movement.vector.changed";
    public const string CharacterMovementHaulted = "character.movement.haulted";
    public const string CharacterStatisticsChanged = "character.statistics.changed";

    public const string RequestCharacterPosition = "request.character.position";
    public const string ResponseCharacterPosition = "response.character.position";

    public const string RemoveCharacterFromScene = "character.remove.from.scene";

    public const string CommandInputEntered = "command.input.entered";
    public const string LoginExistingUser = "command.auth.login";
    public const string RegisterNewUser = "command.auth.register";
    public const string LogoutUser = "command.auth.logout";
    public const string SaveGame = "command.game.save";
    public const string LoadLastSavedGame = "command.game.load.last.save";

    protected NotificationName()
    {
    }
}
