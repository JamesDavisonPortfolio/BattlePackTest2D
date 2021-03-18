using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    UpdateUI updateUI;

    public string enemyName;
    public int maxHP;
    [HideInInspector] public int curHP;

    //array of questions
    public string[] questions;

    public List<int> incorrectlyAnsweredQs;

    public string[] allAnswers;
    public string[] correctAnswers;

    public string[][] answerArrays; //a jagged array - stores an array within an array

    void Awake()
    {
        updateUI = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<UpdateUI>();

        incorrectlyAnsweredQs.Add(-1);

        #region Error Prevention
        if ((allAnswers.Length) % 3 != 0)
        {
            Debug.LogError("Some questions do not have 3 answers");
            Application.Quit();
            return;
        }

        if ((questions.Length) * 3 != allAnswers.Length)
        {
            Debug.LogError("Number of questions is not relative to the amount of answers. 3 per Question");
            Application.Quit();
            return;
        }

        foreach (string answer in allAnswers)
        {
            try 
            { 
                string[] tempDiv = answer.Split(':');
                if (tempDiv[1] == "n")
                {

                }
                else if (tempDiv[1] == "y")
                {

                }
                else
                {
                    Debug.LogWarning(tempDiv[0] + " is incorrectly formatted");
                }
            }
            catch
            {
                Debug.LogError("Question " + answer + "is not formatted properly! " +
                "Remember to include a ':' along with a 'y' or 'n' depending on if the answer is correct"); return;
            }
        }

        if (maxHP > 8)
        {
            maxHP = 8;
            Debug.LogWarning("HP cannot be higher than 8!");
        }
        #endregion

        curHP = maxHP;
        answerArrays = new string[questions.Length][];
        correctAnswers = new string[questions.Length];

        int allAnswerCounter = 0;
        for (int i = 0; i < questions.Length; i++)
        {
            answerArrays[i] = new string[3] { allAnswers[allAnswerCounter], allAnswers[allAnswerCounter + 1], allAnswers[allAnswerCounter + 2] };

            ParseCorrectAnswer(i);

            allAnswerCounter += 3;
        }
        
        #region Debug Output
        //int r = 0;
        //for (int d=0; d < answerArrays.Length; d++)
        //{
        //    Debug.Log("Question " + d);
        //    Debug.Log(answerArrays[d][r]);
        //    Debug.Log(answerArrays[d][r+1]);
        //    Debug.Log(answerArrays[d][r+2]);
        //    Debug.Log("--------------------");
        //}

        //for (int c=0; c < correctAnswers.Length; c++)
        //{
        //    Debug.Log(correctAnswers[c]);
        //}
        #endregion
    }

    /// <summary>
    /// parses the correct answer out of the three items sorted into the the array answerArrays
    /// </summary>
    /// <param name="i"> the current iteration of the answerArrays sorting loop </param>
    void ParseCorrectAnswer(int i)
    {
        for (int j = 0; j < answerArrays[i].Length; j++)
        {
            string[] div = answerArrays[i][j].Split(':');

            answerArrays[i][j] = div[0];

            if (div[1] == "y" && correctAnswers[i] == null)
            {
                correctAnswers[i] = answerArrays[i][j];
            }
        }
    }

    /// <summary>
    /// checks the answer of the question against array of correct answers
    /// </summary>
    /// <param name="questionNumber"> the actual number of the current question </param>
    /// <param name="answer"> the answer the player has just input </param>
    /// <returns></returns>
    public bool checkCorrectAnswer(int questionNumber, string answer)
    {
        if (answer == correctAnswers[questionNumber-1])
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// function for the enemy to take damage
    /// </summary>
    /// <param name="dmg"> how much damage will be dealt </param>
    /// <returns> true = enemy is dead; false = enemy is alive </returns>
    public bool TakeDamage(int dmg)
    {
        curHP -= dmg;

        updateUI.DisplayHealth();

        if (curHP <= 0)
            return true;
        else
            return false;
    }
}
