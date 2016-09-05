using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ControllerNotification
{
    public const string MenuItemChanged = "menu.item.changed";
    public const string SyncViewState = "sync.view.state";

    public const string GameObjectPositionChanged = "game.object.position.changed";
    public const string GameObjectMovementVectorChanged = "game.object.movement.vector.changed";
    public const string GameObjectMovementHalted = "game.object.movement.halted";

    public const string PlayerHealthLevelChanged = "player.health.level.changed";
    public const string PlayerStaminaLevelChanged = "player.stamina.level.changed";

    public const string SceneNameUpdated = "scene.name.update";

    public const string CommandInputHit = "command.input.hit";

    protected ControllerNotification()
    {
        
    }
}
