using System;
/// <summary>
/// Base class for all creatures of the game.
/// </summary>
public class CharacterModel : ThingModel
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="id">character identifier</param>
    /// <param name="name">character name</param>
    /// <param name="commandHint">command hint for interaction with character</param>
    /// <param name="kind">character kind</param>
    /// <param name="sceneId">reference to scene by its identifier</param>
    /// <param name="age">character age</param>
    /// <param name="gender">character gender</param>
    /// <param name="statistics">character statistics</param>
    /// <param name="inventory">character inventory</param>
    public CharacterModel(string id, string name, string commandHint, CharacterKind kind, string sceneId, int age, CharacterGender gender,
        CharacterStatisticsModel statistics, InventoryModel inventory) :
        base(id, name, commandHint)
    {
        SceneId = sceneId;
        Age = age;
        Gender = gender;
        CharacterType = kind;

        Statistics = statistics;
        statistics.SetCharacterModel(this);

        Inventory = inventory;

        TimeStamp = DateTime.UtcNow;
    }

    // specific properties here
    public string SceneId { get; internal set; }
    public int Age { get; internal set; }
    public CharacterGender Gender { get; internal set; }
    public CharacterKind CharacterType { get; internal set; }
    public CharacterStatisticsModel Statistics { get; internal set; }
    public InventoryModel Inventory { get; internal set; }
    public DateTime TimeStamp { get; internal set; }

    public override void SetViewModel(BaseViewModel viewModel)
    {
        base.SetViewModel(viewModel);

        SyncStatistics();
    }

    public override T ConvertToDTO<T>(params object[] data)
    {
        T result = null;

        // create initail character for user
        var characterDto = new CharacterDTO()
        {
            SceneId = SceneId,
            CharacterId = Id,
            Name = Name,
            Age = Age,
            Gender = Gender,
            CharacterType = CharacterType,
            CommandHint = CommandHint,
            InventoryId = Inventory.Id,
            ArmorLevel = Statistics.ArmorLevel,
            DamageLevel = Statistics.DamageLevel,
            HealthLevel = Statistics.HealthLevel,
            StaminaLevel = Statistics.StaminaLevel,
            CarriedWeightLevel = Statistics.CarriedWeightLevel,
            XpLevel = Statistics.XpLevel,
            Health = Statistics.CurrentHealth,
            Stamina = Statistics.CurrentStamina,
            XpPoints = Statistics.XpPoints,
            TimeStamp = TimeStamp.ToString(),
        };

        result = characterDto as T;
        if (data.Length > 0)
        {
            // request current position of player
            ViewModel.Notify(NotificationName.RequestCharacterPosition, this);

            characterDto.LocationX = Position.X;
            characterDto.LocationY = Position.Y;

            result = new CharacterStateDTO(data[0].ToString(), characterDto) as T;
        }

        return result;
    }

    /// <summary>
    /// Allows to change character's current scene.
    /// </summary>
    /// <param name="value"></param>
    public void SetSceneId(string value)
    {
        SceneId = value;
        TimeStamp = DateTime.UtcNow;
    }

    /// <summary>
    /// Update movement vector for character.
    /// </summary>
    /// <param name="vector">current vector of movement</param>
    /// <param name="isRun">flag which indicate is vector for running or otherwise for walking</param>
    public void SetMovementVector(UnityVector2 vector, bool isRun = false)
    {
        if (disposedValue)
            return;

        TimeStamp = DateTime.UtcNow;

        if (ViewModel != null)
        {
            if (vector.X == 0 && vector.Y == 0)
                ViewModel.Notify(NotificationName.CharacterMovementHaulted, this);
            else
                ViewModel.Notify(NotificationName.CharacterMovementVectorChanged, this, vector, isRun);
        }
    }

    /// <summary>
    /// Allows to remove character from active scnene (killed or moved to other scene)
    /// </summary>
    public void RemoveCharacterFromScene()
    {
        ViewModel.Notify(NotificationName.RemoveCharacterFromScene, this);
    }

    /// <summary>
    /// Allows to sync statistics with View.
    /// </summary>
    public void SyncStatistics()
    {
        if (disposedValue)
            return;

        ViewModel.Notify(NotificationName.CharacterStatisticsChanged, this, Statistics);
    }

    /// <summary>
    /// Allows to sync state of character with new values.
    /// </summary>
    /// <param name="newState">new state</param>
    public void UpdateInternalState(CharacterModel newState)
    {
        // note: should not be update
        //SetPosition(newState.Position, false);

        Statistics = newState.Statistics;
        Inventory = newState.Inventory;
        TimeStamp = newState.TimeStamp;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Statistics.Dispose();
            Inventory.Dispose();
        }

        base.Dispose(disposing);
    }
}
