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

    public const string PlayerPositionChanged = "player.position.changed";
    public const string PlayerMovementVectorChanged = "player.movement.vector.changed";
    public const string PlayerMovementHaulted = "player.movement.haulted";
    public const string PlayerStatisticsChanged = "player.statistics.changed";

    public const string RequestPlayerPosition = "request.player.position";
    public const string ResponsePlayerPosition = "response.player.position";

    public const string SceneNameUpdated = "scene.name.update";

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
