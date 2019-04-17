[System.Serializable]
public class CustomAI
{
    public enum HighestOrLowest
    {
        Highest,
        Lowest
    }

    public enum Properties
    {
        maxHitPoints,
        hitPoints,
        initiative,
        damage,
        percentChanceToHit,
        healthPercentageDifference,
    }

    public HighestOrLowest highestOrLowest;
    public Properties propertyName;
}