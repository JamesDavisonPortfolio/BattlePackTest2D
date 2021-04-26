using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StateMachine : MonoBehaviour
{
    //editor vars
    public GameObject enemyPrefab;

    public Transform enemyLocation;

    public DataCarrier levelData;
    //script references
    UpdateUI updateUI;
    EnemyUnit enemyUnit;

    //system vars
    /// <summary>
    /// current question 
    /// </summary>
    int questionNumber = 1;

    int battleScore, correctQuestions;

    /// <summary>
    /// a list of questions the player gets wrong that is added to after the second time they get them wrong
    /// </summary>
    List<int> incorrectQuestions;

    /// <summary>
    /// a bool which is set to true once the end of the list of questions is enabled which allows 
    /// the program to revisit questions the player got incorrect
    /// </summary>
    public bool incorrectLoop = false;
    int incorrectCounter = 0;

    /// <summary>
    /// stores and counts how many times a question is answered incorrectly
    /// </summary>
    int[] timesAttempted;

    /// <summary>
    /// controls whether the player can input
    /// </summary>
    bool clickyButton = true;

    void Awake()
    {
        updateUI = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<UpdateUI>();

        GameObject enemyGo = Instantiate(enemyPrefab, enemyLocation);
        enemyUnit = enemyGo.GetComponent<EnemyUnit>();
        enemyLocation.GetComponentInChildren<Image>().sprite = enemyUnit.GetComponent<SpriteRenderer>().sprite;
        timesAttempted = new int[enemyUnit.questions.Length];
    }

    void Start()
    {
        incorrectQuestions = new List<int>();
    }

    /// <summary>
    /// calculates the players score and cycles through arrays in the incorrect loop
    /// </summary>
    /// <param name="result"></param>
    void UpdateScore(bool result)
    {
        clickyButton = false;
        // if the player answered correctly
        if (result)
        {
            //things that happen in both loops
            incorrectCounter = 0;
            correctQuestions++;
            battleScore += 200;
            updateUI.DisplayResult("That answer was... correct!");
            enemyUnit.TakeDamage(1);

            //things that happen in incorrect loop
            if (incorrectLoop)
            {
                enemyUnit.incorrectlyAnsweredQs.RemoveAt(1);
            }
        }
        //if the player answered incorrectly
        else
        {
            //things that happen in both loops
            timesAttempted[questionNumber - 1]++;
            updateUI.DisplayResult("That answer was... incorrect");
            if (battleScore > 0)
            {
                battleScore -= 100;
                if (battleScore < 0)
                    battleScore = 0;
            }

            //things that happen in incorrect loop
            if (incorrectLoop)
            {
                // removes from list and adds to another as the system uses the second slot for calculations
                // but the incorrect questions still need to be stored somewhere
                incorrectCounter++;
                if (incorrectCounter >= 1)
                    this.incorrectQuestions.Add(enemyUnit.incorrectlyAnsweredQs[1]);
                ///enemyUnit.incorrectlyAnsweredQs.RemoveAt(1);
            }

            //things that happen in first loop
            else
            {
                enemyUnit.incorrectlyAnsweredQs.Add(questionNumber);
            }
        }

        updateUI.DisplayScore(battleScore);
        UpdateQuestion();
    }

    /// <summary>
    /// calculates the parameters to update the question on display
    /// </summary>
    void UpdateQuestion()
    {
        // checks to see if the questions are over and if there are any incorrect questions
        // if true, enables the "incorrect loop"
        if (enemyUnit.incorrectlyAnsweredQs.Count > 1 &&
            questionNumber == enemyUnit.questions.Length)
        {
            incorrectLoop = true;
        }

        // checks if it should calculate the question on display
        // firstly checks if the question isn't at the end of the array of questions yet and if the bool incorrect loop is not enabled
        // or checks if the variable "incorrect loop" is true and if the length of the list of incorrect answers is greater than 1
        if (incorrectLoop && enemyUnit.incorrectlyAnsweredQs.Count > 1 || !incorrectLoop && questionNumber < enemyUnit.questions.Length)
        {
            clickyButton = true;

            //if its not the "incorrect loop", simply adds 1 to question counter
            //also ensures chance2 is disabled
            if (!incorrectLoop)
            {
                updateUI.DisplayChance2();
                questionNumber++;
            }
            else
            {
                //ensures chance 2 is enabled
                updateUI.DisplayChance2(true);
                //updates question on display
                questionNumber = enemyUnit.incorrectlyAnsweredQs[1];
            }

            //passes it onto the updateUI class with question number in toe
            updateUI.DisplayQuestion(questionNumber);
        }
        else
        {
            EndBattle();
        }
    }

    /// <summary>
    /// sets up parameters to call the interval screen
    /// </summary>
    void EndBattle()
    {
        //counts the amount of questions that are incorrect
        int aoIncorrectQuestions = this.incorrectQuestions.Count;

        //new array that stores bools as to whether the questions were answered correct or false
        bool[] results = new bool[enemyUnit.questions.Length];
        // loops through the array and assigns true or false depending on if the questions number
        // is found in the "incorrect Questions" array
        for (int i = 0; i < results.Length; i++)
        {
            if (CollateAnswers(i))
                results[i] = false;
            else
                results[i] = true;
        }

        Debug.LogWarning("End Application - Cause: Questions Ended!");

        /// SAVING TO SCRIPTABLE OBJECT WOULD GO HERE
        /// 
        /// I RECOMMEND SAVING:
        /// string[]: enemyUnit.questions - has all questions stored as an array of strings
        /// string[]: enemyUnit.correctAnswers - has all correct answers stored with their positions stored relative to the question asked (ie Question 1 would be position 1 (index 0) in the array
        /// bool[]: results - has all results stored as an array of booleans
        /// int[]: timesAttempted - stores the amount of times each question was attempted as an array of integers
        /// int: aoIncorrectQuestions - the amount of questions incorrectly answered twice
        /// int: battleScore - score from battle
        /// 
        levelData.SetQuestions(enemyUnit.questions);
        levelData.SetAnswers(enemyUnit.correctAnswers);
        levelData.SetAttempts(timesAttempted);
        levelData.SetBossScore(battleScore);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        return;
    }

    /// <summary>
    /// for use with EndBattle() cycles through incorrectQuestions Array
    /// </summary>
    /// <param name="i"> value i in EndBattle() </param>
    /// <returns></returns>
    bool CollateAnswers(int i)
    {
        for (int j = 0; j < incorrectQuestions.Count; j++)
        {
            if (i + 1 == incorrectQuestions[j])
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// updates the enemy on display
    /// // currently unused
    /// </summary>
    public void UpdateEnemy()
    {
        if (updateUI.CheckForNewEnemy())
        {
            enemyUnit = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyUnit>();
        }
    }

    /// <summary>
    /// called when Button1 is pressed
    /// </summary>
    public void OnButton1()
    {
        Debug.Log("Button1 - " + updateUI.option1.text + " - pressed");
        if (clickyButton)
        {
            UpdateScore(enemyUnit.checkCorrectAnswer(questionNumber, updateUI.option1.text, true));
        }
    }

    /// <summary>
    /// called when Button2 is pressed
    /// </summary>
    public void OnButton2()
    {
        Debug.Log("Button1 - " + updateUI.option2.text + " - pressed");
        if (clickyButton)
            UpdateScore(enemyUnit.checkCorrectAnswer(questionNumber, updateUI.option2.text, true));
    }

    /// <summary>
    /// called when Button3 is pressed
    /// </summary>
    public void OnButton3()
    {
        Debug.Log("Button1 - " + updateUI.option3.text + " - pressed");
        if (clickyButton)
            UpdateScore(enemyUnit.checkCorrectAnswer(questionNumber, updateUI.option3.text, true));
    }
}
