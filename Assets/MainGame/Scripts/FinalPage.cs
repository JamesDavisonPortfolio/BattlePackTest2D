using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalPage : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalMazeScoreText;
    public TextMeshProUGUI finalBossScoreText;
    public TextMeshProUGUI finalAnswersWrongText;

    public DataCarrier[] levelDetails;

    int finalScore;
    int finalMazeScore;
    int finalBossScore;
    int finalAnswersWrong;

    void Start()
    {
        GetScores();
        finalScoreText.SetText("Total Score: " + finalScore.ToString("00000"));
        finalMazeScoreText.SetText("Total Maze Score: " + finalMazeScore.ToString("00000"));
        finalBossScoreText.SetText("Final Boss Score: " + finalBossScore.ToString("00000"));
        finalAnswersWrongText.SetText("Total Incorrect Answers: " + finalAnswersWrong.ToString());
    }

    void GetScores()
    {
        for(int i = 0; i < levelDetails.Length; i++)
        {
            finalMazeScore += levelDetails[i].GetLevelScore();
            finalBossScore += levelDetails[i].GetBossScore();
            finalAnswersWrong += levelDetails[i].GetWrong();
        }
        finalScore = (finalMazeScore + finalBossScore);
    }
}
