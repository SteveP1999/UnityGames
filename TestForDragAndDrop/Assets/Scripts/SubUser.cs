using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SubUser
{
    public string userName;
    public string age;   // young-old
    public int levelInNewArrival;
    public int bestScoreInOrderGame;
    public int bestSerialScoreInPairGame;
    public int bestParallelScoreInPairGame;
    public bool active;
}
