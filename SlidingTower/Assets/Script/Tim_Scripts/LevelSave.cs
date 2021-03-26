[System.Serializable]
public class LevelSave 
{
    public int levelNumber;
    public string name;
    public string description;
    public int score;

    public LevelSave (LevelsSO level)
    {
        levelNumber = level.levelNumber;
        score = level.score;
    }
}
