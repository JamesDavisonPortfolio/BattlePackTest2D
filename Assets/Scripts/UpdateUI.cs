using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    public Text enemyName;
    public Text enemyAttack;
    public Text score;
    public Image chance;
    public Text option1;
    public Text option2;
    public Text option3;

    GameObject[] hpCovers;
    [HideInInspector] public int currentQuestion = 0;

    EnemyUnit _enemy;

    void Start()
    {
        hpCovers = GameObject.FindGameObjectsWithTag("EnemyHpCovers");

        fullUpdate(1);
    }

    /// <summary>
    /// fully updates the user interface
    /// </summary>
    /// <param name="questionNo"> the question number to be asked (actual) </param>
    public void fullUpdate(int questionNo)
    {
        _enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyUnit>();

        DisplayName();

        DisplayHealth();

        DisplayQuestion(questionNo);
        DisplayChance2();
    }

    #region Display Statements
    /// <summary>
    /// updates enemy name plate
    /// </summary>
    public void DisplayName()
    {
        enemyName.text = _enemy.enemyName;
    }

    /// <summary>
    /// updates the enemy health bar
    /// </summary>
    public void DisplayHealth()
    {
        for (int i = 0; i < hpCovers.Length; i++)
        {
            //converts number in the name of the hpCover to an int 
            //then compares it to the hp value of the enemy
            if ((hpCovers[i].name[10] - '0') <= _enemy.curHP - 1)
            {
                hpCovers[i].SetActive(false);
            }
            else
            {
                if (!hpCovers[i].activeSelf)
                    hpCovers[i].SetActive(true);
            }
        }
    }

    /// <summary>
    /// updates the box on the left to the question passed in
    /// </summary>
    /// <param name="questionNo"> the question number to be asked (actual) </param>
    public void DisplayQuestion(int questionNo) 
    {
        questionNo--;

        try { enemyAttack.text = _enemy.questions[questionNo]; }
        catch { Debug.LogError("INVALID QUESTION INPUTTED");  return; }

        currentQuestion = questionNo;

        DisplayButton(ref option1, 0);
        DisplayButton(ref option2, 1);
        DisplayButton(ref option3, 2);
    }

    /// <summary>
    /// updates the box on the left to the text passed in
    /// </summary>
    /// <param name="text"> string of text that will be displayed </param>
    public void DisplayResult(string text)
    {
        enemyAttack.text = text;
    }

    /// <summary>
    /// controls the enabled / disable of the ui box which informs the player if this is their first time answering a question
    /// </summary>
    /// <param name="enable"> the bool passed in </param>
    public void DisplayChance2(bool enable = false)
    {
        chance.gameObject.SetActive(enable);
    }

    /// <summary>
    /// Updates the display button, changing the text and colour when pressed
    /// </summary>
    /// <param name="option"> the button you wish to alter </param>
    /// <param name="i"> the question (out of 3) you wish to display </param>
    void DisplayButton(ref Text option, int i)
    {
        option.text = _enemy.answerArrays[currentQuestion][i];

        ColorBlock cb = option.GetComponentInParent<Button>().colors;

        if (_enemy.checkCorrectAnswer(currentQuestion+1, option.text))
        {
            cb.pressedColor = Color.green;
        }
        else
        {
            cb.pressedColor = Color.red;
        }

        option.GetComponentInParent<Button>().colors = cb;
    }

    /// <summary>
    /// updates the display of the score
    /// </summary>
    /// <param name="battleScore"> score int passed in</param>
    public void DisplayScore(int battleScore)
    {
        if (battleScore == 0)
        {
            score.text = "Current Score: 000";
        }
        else
        {
            score.text = "Current Score: " + battleScore; 
        }
    }
    #endregion

    /// <summary>
    /// checks if there is a new enemy on the field
    /// </summary>
    /// <returns>true = enemy is different; false = no change</returns>
    public bool CheckForNewEnemy()
    {
        if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyUnit>() != _enemy)
            return true;
        else
            return false;
    }

}
