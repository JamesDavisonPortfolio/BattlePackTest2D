using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Final Data", menuName = "Final Data")]
public class FinalData : ScriptableObject
{
    public DataCarrier[] allLevelSumarries;

    public int totalScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getCurrentTotalScore()
    {
        totalScore = 0;
        for(int i = 0; i < allLevelSumarries.Length; i++)
        {
            totalScore += (allLevelSumarries[i].GetBossScore() + allLevelSumarries[i].GetLevelScore());
        }
        return totalScore;
    }
}
