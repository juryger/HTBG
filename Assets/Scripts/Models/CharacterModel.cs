/// <summary>
/// Base class for all creatures of the game.
/// </summary>
public class CharacterModel : ThingModel
{
    public CharacterModel(string id, string name, string commandHint, CharacterKind kind, int age, CharacterGender gender,
        CharacterStatisticsModel statistics, InventoryModel inventory) :
        base(id, name, commandHint)
    {
        Age = age;
        Gender = gender;
        CharacterType = kind;

        Statistics = statistics;
        statistics.SetCharacterModel(this);

        Inventory = inventory;
    }

    // specific properties here
    public int Age { get; internal set; }
    public CharacterGender Gender { get; internal set; }
    public CharacterKind CharacterType { get; internal set; }
    public CharacterStatisticsModel Statistics { get; internal set; }
    public InventoryModel Inventory { get; internal set; }

    public override T ConvertToDTO<T>(params object[] data)
    {
        var dto = new CharacterDTO()
        {
            SceneId = data[0].ToString(),
            CharacterId = Id,
            InventoryId = Inventory.Id,
            LocationX = Position.X,
            LocationY = Position.Y,
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

        // create initail character for user
        if (data[1] != null)
        {
            // request current position of player
            ViewModel.Notify(NotificationName.RequestPlayerPosition, this);

            dto = new CharacterStateDTO(data[1].ToString(), dto);
        }

        return dto as T;
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
