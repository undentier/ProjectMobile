[System.Serializable]
public class LevelSave 
{
    public int levelNumber;
    public int score;

    public LevelSave (LevelsSO level)
    {
        levelNumber = level.levelNumber;
        score = level.score;
    }
}
