/// <summary>
/// Manages of creation different type of game models.
/// </summary>
internal static class GameEnvironmentManager
{
    /// <summary>
    /// Create player.
    /// </summary>
    /// <param name="name">name of the character</param>
    /// <param name="commandHint">command hint for interaction with this character</param>
    /// <param name="age">age of the character</param>
    /// <param name="gender">gender of the character</param>
    /// <returns>player model</returns>
    public static CharacterModel CreatePlayer(string name, string commandHint, int age, CharacterGender gender)
    {
        var characterCreator = new PlayerCreator();

        return characterCreator.CreatCharacter(name, commandHint, age, gender);
    }
}
