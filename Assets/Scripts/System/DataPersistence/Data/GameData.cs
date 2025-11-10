using System.Collections;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int playerLevel;
    public Vector3 playerPosition;
    
    //the values defind in this constructor will be default values
    // When new game player will be level 0
    public GameData()
    {
        this.playerLevel = 0;
        this.playerPosition = Vector3.zero;
    }
}
