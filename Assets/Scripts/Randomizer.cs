using UnityEngine;

public static class Randomizer
{
    public static Vector3 GetHorisontalPosition(int maxRangeX, int maxRangeZ)
    {
        int positionX = Random.Range(-maxRangeX, maxRangeX);        
        int positionZ = Random.Range(-maxRangeZ, maxRangeZ);

        return new Vector3(positionX, 0, positionZ);
    }
}