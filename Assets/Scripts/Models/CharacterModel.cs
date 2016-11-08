﻿/// <summary>
/// Base class for all creatures of the game.
/// </summary>
public class CharacterModel : ThingModel
{
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
    }

    // specific properties here
    public string SceneId { get; internal set; }
    public int Age { get; internal set; }
    public CharacterGender Gender { get; internal set; }
    public CharacterKind CharacterType { get; internal set; }
    public CharacterStatisticsModel Statistics { get; internal set; }
    public InventoryModel Inventory { get; internal set; }

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
        };

        result = characterDto as T;
        if (data.Length > 0)
        {
            // request current position of player
            ViewModel.Notify(NotificationName.RequestPlayerPosition, this);

            characterDto.LocationX = Position.X;
            characterDto.LocationY = Position.Y;

            result = new CharacterStateDTO(data[0].ToString(), characterDto) as T;
        }

        return result;
    }

    public void SetSceneId(string value)
    {
        SceneId = value;
    }

    public void SetMovementVector(UnityVector2 vector, bool isRun = false)
    {
        if (disposedValue)
            return;

        if (ViewModel != null)
        {
            if (vector.X == 0 && vector.Y == 0)
                ViewModel.Notify(NotificationName.PlayerMovementHaulted, this);
            else
                ViewModel.Notify(NotificationName.PlayerMovementVectorChanged, this, vector, isRun);
        }
    }

    /// <summary>
    /// Allows to sync statistics with View.
    /// </summary>
    public void SyncStatistics()
    {
        if (disposedValue)
            return;

        ViewModel.Notify(NotificationName.PlayerStatisticsChanged, Statistics);
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
