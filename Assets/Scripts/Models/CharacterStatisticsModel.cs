using System;
/// <summary>
/// Represent statistics of the character.
/// </summary>
public class CharacterStatisticsModel : IDisposable
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="armorLevel">base armor level</param>
    /// <param name="damageLevel">base damage level</param>
    /// <param name="healthLevel">base health level</param>
    /// <param name="staminaLevel">base stamina level</param>
    /// <param name="carriedWeightLevel">base carried weight level</param>
    /// <param name="xpLevel">current experience level</param>
    /// <param name="xpLevel">experience points</param>
    public CharacterStatisticsModel(int armorLevel, int damageLevel, int healthLevel, int staminaLevel, float carriedWeightLevel, int xpLevel, int xpPoints)
    {
        CurrentArmor = ArmorLevel = armorLevel;
        CurrentDamage = DamageLevel = damageLevel;
        CurrentHealth = HealthLevel = healthLevel;
        CurrentStamina = StaminaLevel = staminaLevel;
        CurrentCarriedWeight = CarriedWeightLevel = carriedWeightLevel;
        XpLevel = xpLevel;
    }

    public CharacterModel Character { get; private set; }

    #region Leveled values

    public int ArmorLevel { get; private set; }

    public int DamageLevel { get; private set; }

    public int HealthLevel { get; private set; }

    public int StaminaLevel { get; private set; }

    public float CarriedWeightLevel { get; private set; }

    public int XpLevel { get; private set; }

    #endregion

    #region Current values

    public float CurrentArmor { get; private set; }

    public float CurrentDamage { get; private set; }

    public int CurrentHealth { get; private set; }

    public int CurrentStamina { get; private set; }

    public float CurrentCarriedWeight { get; private set; }

    public int XpPoints { get; private set; }

    #endregion

    #region Flags which tell what item of statistics has changed

    private bool isHealthChanged;
    public bool IsHealthChanged { get { var b = isHealthChanged; isHealthChanged = false; return b; } }

    private bool isStaminaChanged;
    public bool IsStaminaChanged { get { var b = isStaminaChanged; isStaminaChanged = false; return b; } }

    private bool isArmorChanged;
    public bool IsArmorChanged { get { var b = isArmorChanged; isArmorChanged = false; return b; } }

    private bool isDamageChanged;
    public bool IsDamageChanged { get { var b = isDamageChanged; isDamageChanged = false; return b; } }

    private bool isCarriedWeigthChanged;
    public bool IsCarriedWeigthChanged { get { var b = isCarriedWeigthChanged; isCarriedWeigthChanged = false; return b; } }

    private bool isXpPointsChanged;
    public bool IsXpPointsChanged { get { var b = isXpPointsChanged; isXpPointsChanged = false; return b; } }

    #endregion

    /// <summary>
    /// Allows to link Character with it statistics.
    /// </summary>
    /// <param name="character">character model</param>
    public void SetCharacterModel(CharacterModel character)
    {
        if (isDisposed)
            return;

        if (Character != null)
            throw new ApplicationException("CharacterModel already assign at Statistics");

        Character = character;
    }

    public void ChangeHealthByValue(int value)
    {
        CurrentHealth += value;

        if (CurrentHealth > HealthLevel)
            CurrentHealth = HealthLevel;

        if (CurrentHealth < 0)
            CurrentHealth = 0;

        // Notify View
        if (isDisposed)
            return;

        isHealthChanged = true;
        Character.SyncStatistics();
    }

    public void ChangeStaminaByValue(int value)
    {
        CurrentStamina += value;

        if (CurrentStamina > StaminaLevel)
            CurrentStamina = StaminaLevel;

        if (CurrentStamina < 0)
            CurrentStamina = 0;

        // Notify ViewModel
        if (isDisposed)
            return;

        isStaminaChanged = true;
        Character.SyncStatistics();
    }

    public void ChangeArmorByValue(int value)
    {
        CurrentArmor += value;

        if (CurrentArmor > ArmorLevel)
            CurrentStamina = StaminaLevel;

        if (CurrentStamina < 0)
            CurrentStamina = 0;

        // Notify ViewModel
        if (isDisposed)
            return;

        isArmorChanged = true;
        Character.SyncStatistics();
    }

    public void ChangeDamageByValue(int value)
    {
        CurrentDamage += value;

        if (CurrentArmor > ArmorLevel)
            CurrentStamina = StaminaLevel;

        if (CurrentStamina < 0)
            CurrentStamina = 0;

        // Notify ViewModel
        if (isDisposed)
            return;

        isDamageChanged = true;
        Character.SyncStatistics();
    }

    public void ChangeCarriedWeightByValue(int value)
    {
        CurrentCarriedWeight += value;

        if (CurrentCarriedWeight > CarriedWeightLevel)
            CurrentCarriedWeight = CarriedWeightLevel;

        if (CurrentCarriedWeight < 0)
            CurrentCarriedWeight = 0;

        // Notify ViewModel
        if (isDisposed)
            return;

        isCarriedWeigthChanged = true;
        Character.SyncStatistics();
    }

    public void AddXpPoints(int value)
    {
        XpPoints += Math.Abs(value);

        if (XpPoints >= 1000)
        {
            XpLevel += 1;
            XpPoints = 0;
        }

        // Notify ViewModel
        if (isDisposed)
            return;

        isXpPointsChanged = true;
        Character.SyncStatistics();
    }

    #region Disposable

    private bool isDisposed;

    public void Dispose()
    {
        isDisposed = true;
        Character = null;
    }

    #endregion
}
