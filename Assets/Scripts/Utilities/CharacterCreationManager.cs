using System;


/// <summary>
/// Class used to defien factory methods for creating characters.
/// </summary>
internal abstract class CharacterCreator
{
    /// <summary>
    /// Factory method for creating character base on age and gender.
    /// </summary>
    /// <param name="name">name of a character</param>
    /// <param name="commandHint">command for interacting with a character</param>
    /// <param name="sceneId">scene identifier where player is located</param>
    /// <param name="age">age of character</param>
    /// <param name="gender">gender of a character</param>
    /// <returns>character</returns>
    public abstract CharacterModel CreatCharacter(string name, string commandHint, string sceneId, int age, CharacterGender gender);

    protected int CalculateDamageLevel(int age, CharacterGender gender)
    {
        var level = 0;

        var agec = GetAgeCategory(age);
        switch (agec)
        {
            case AgeCategory.YouthAge:
                level = (gender == CharacterGender.Femail ? 10 : 15);
                break;
            case AgeCategory.MiddleAge:
                level = (gender == CharacterGender.Femail ? 15 : 30);
                break;
            case AgeCategory.ElderAge:
                level = (gender == CharacterGender.Femail ? 7 : 12);
                break;
            case AgeCategory.OldAge:
                level = (gender == CharacterGender.Femail ? 1 : 5);
                break;
            default:
                break;
        }

        return level;
    }

    protected int CalculateHealthLevel(int age, CharacterGender gender)
    {
        var level = 0;

        var agec = GetAgeCategory(age);
        switch (agec)
        {
            case AgeCategory.YouthAge:
                level = 30;
                break;
            case AgeCategory.MiddleAge:
                level = 50;
                break;
            case AgeCategory.ElderAge:
                level = 20;
                break;
            case AgeCategory.OldAge:
                level = 5;
                break;
            default:
                break;
        }

        return level;
    }

    protected int CalculateStaminahLevel(int age, CharacterGender gender)
    {
        var level = 0;

        var agec = GetAgeCategory(age);
        switch (agec)
        {
            case AgeCategory.YouthAge:
                level = (gender == CharacterGender.Femail ? 30 : 40);
                break;
            case AgeCategory.MiddleAge:
                level = (gender == CharacterGender.Femail ? 35 : 50);
                break;
            case AgeCategory.ElderAge:
                level = (gender == CharacterGender.Femail ? 20 : 25);
                break;
            case AgeCategory.OldAge:
                level = (gender == CharacterGender.Femail ? 5 : 10);
                break;
            default:
                break;
        }

        return level;
    }

    protected int CalculateCarriedWeigthLevel(int age, CharacterGender gender)
    {
        var level = 0;

        var agec = GetAgeCategory(age);
        switch (agec)
        {
            case AgeCategory.YouthAge:
                level = (gender == CharacterGender.Femail ? 30 : 40);
                break;
            case AgeCategory.MiddleAge:
                level = (gender == CharacterGender.Femail ? 35 : 60);
                break;
            case AgeCategory.ElderAge:
                level = (gender == CharacterGender.Femail ? 15 : 20);
                break;
            case AgeCategory.OldAge:
                level = (gender == CharacterGender.Femail ? 1 : 5);
                break;
            default:
                break;
        }

        return level;
    }

    private AgeCategory GetAgeCategory(int age)
    {
        var ageCat = AgeCategory.NonExist;

        if (age > 0 && age < 20)
        {
            return AgeCategory.YouthAge;
        }
        else if (age >= 20 && age < 50)
        {
            return AgeCategory.MiddleAge;
        }
        else if (age >= 50 && age < 70)
        {
            return AgeCategory.ElderAge;
        }
        else if (age >= 70)
        {
            return AgeCategory.OldAge;
        }

        return ageCat;
    }

    private enum AgeCategory
    {
        NonExist = 0,
        YouthAge = 1,
        MiddleAge = 2,
        ElderAge = 3,
        OldAge = 4,
    }
}

/// <summary>
/// Class used for creation players based on factory methods.
/// </summary>
internal class PlayerCreator : CharacterCreator
{
    public override CharacterModel CreatCharacter(string name, string commandHint, string sceneId, int age, CharacterGender gender)
    {
        var statistics = new CharacterStatisticsModel(
            0,
            CalculateDamageLevel(age, gender),
            CalculateHealthLevel(age, gender),
            CalculateStaminahLevel(age, gender),
            CalculateCarriedWeigthLevel(age, gender),
            1,
            0);

        var inventory = new InventoryModel(Guid.NewGuid().ToString(), "PlayerInventory", "inventory (i)", InventoryKind.Character);

        return new CharacterModel(
            Guid.NewGuid().ToString(),
            name,
            commandHint,
            CharacterKind.Player,
            sceneId,
            age,
            gender,
            statistics,
            inventory);
    }
}

/// <summary>
/// Class used for creation npc characters based on factory methods.
/// </summary>
internal class NpcCreator : CharacterCreator
{
    public override CharacterModel CreatCharacter(string name, string commandHint, string sceneId, int age, CharacterGender gender)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Class used for creation low rank enemies characters based on factory methods.
/// </summary>
internal class LowRankEnemyCreator : CharacterCreator
{
    public override CharacterModel CreatCharacter(string name, string commandHint, string sceneId, int age, CharacterGender gender)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Class used for creation lieutenant enemies characters based on factory methods.
/// </summary>
internal class LieutenantEnemyCreator : CharacterCreator
{
    public override CharacterModel CreatCharacter(string name, string commandHint, string sceneId, int age, CharacterGender gender)
    {
        throw new NotImplementedException();
    }
}
