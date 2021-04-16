using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Carrier", menuName = "Data Carrier")]
public class DataCarrier : ScriptableObject
{
    public int levelScoreValue;
    public int bossScoreValue;
    public int timeToCompleteMins;
    public int timeToCompleteSecs;

    public int questionsAnsweredIncorrectly;

    public string[] levelQuestions;

    public string[] allLevelAnswers;

    public string[] levelAnswers;

    public int[] questionAttempts;
    public int totalWrong;

    public void SetTimeToComplete(int mins, int secs)
    {
        timeToCompleteMins = mins;
        timeToCompleteSecs = secs;
    }

    public int getMinutes()
    {
        return timeToCompleteMins;
    }

    public int getSeconds()
    {
        return timeToCompleteSecs;
    }
    public void SetLevelScore(int score)
    {
        levelScoreValue = score;
    }
    public int GetLevelScore()
    {
        return levelScoreValue;
    }

    public void SetBossScore(int score)
    {
        bossScoreValue = score;
    }
    public int GetBossScore()
    {
        return bossScoreValue;
    }

    public void SetQuestionsAnsweredIncorrectly(int Questions)
    {
        questionsAnsweredIncorrectly = Questions;
    }
    public int GetQuestionsAnsweredIncorrectly()
    {
        return questionsAnsweredIncorrectly;
    }

    public void SetQuestions(string[] questions)
    {
        levelQuestions = questions;
    }

    public string[] GetQuestions()
    {
        return levelQuestions;
    }

    public void SetAnswers(string[] questions)
    {
        levelAnswers = questions;
    }

    public string[] GetAnswers()
    {
        return levelAnswers;
    }
    public void SetAttempts(int[] attemptList)
    {
        questionAttempts = attemptList;
    }

    public int[] GetAttempts()
    {
        return questionAttempts;
    }

    public int GetWrong()
    {
        totalWrong = 0;
        for(int i = 0; i < questionAttempts.Length; i++)
        {
            totalWrong += (questionAttempts[i]);
        }
        return totalWrong;
    }
}
