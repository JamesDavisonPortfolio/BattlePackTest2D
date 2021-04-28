using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntervalScreenScript : MonoBehaviour
{
    public DataCarrier previousLevelData;
    public FinalData finalData;


    public GameObject summaryPanel;
    public TextMeshProUGUI mazeScore;
    public TextMeshProUGUI mazeTime;
    public TextMeshProUGUI bossScore;
    public TextMeshProUGUI bossInocrect;
    public TextMeshProUGUI levelScore;
    public TextMeshProUGUI totalScore;


    public GameObject bossSummaryPanel;
    public TextMeshProUGUI questionsIncorrectText;

    public TextMeshProUGUI[] questionTexts;
    public TextMeshProUGUI[] answerTexts;

    public string[] questions;
    public string[] answers;

    public int[] attempts;


    // Start is called before the first frame update
    void Start()
    {
        questions = previousLevelData.GetQuestions();
        answers = previousLevelData.GetAnswers();
        attempts = previousLevelData.GetAttempts();
        ActivateMainSumary();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateMainSumary()
    {
        summaryPanel.SetActive(true);
        bossSummaryPanel.SetActive(false);
        mazeScore.SetText("Maze Score: " + previousLevelData.GetLevelScore().ToString("00000"));
        mazeTime.SetText("Time To Complete: " + previousLevelData.getMinutes().ToString("00") + ":" + previousLevelData.getSeconds().ToString("00"));
        bossScore.SetText("Boss Score: " + previousLevelData.GetBossScore().ToString("00000"));
        bossInocrect.SetText("Answered Incorrectly: " + previousLevelData.GetWrong().ToString());
        levelScore.SetText("Level Score: " + (previousLevelData.GetBossScore() + previousLevelData.GetLevelScore()).ToString());
        totalScore.SetText("Total Score: " + finalData.getCurrentTotalScore().ToString("00000"));
    }

    public void ActivateBossSumamry()
    {
        bossSummaryPanel.SetActive(true);
        summaryPanel.SetActive(false);
        for(int i = 0; i < questions.Length; i++)
        {
            questionTexts[i].SetText(questions[i]);
            answerTexts[i].SetText(answers[i] + ". Incorrect Atempts: " + attempts[i]);
            if(attempts[i] == 0)
            {
                answerTexts[i].color = new Color32(49, 200, 0, 255);
            }
            else if(attempts[i] == 1)
            {
                answerTexts[i].color = new Color32(255, 235, 35, 255);
            }
            else if(attempts[i] >= 2)
            {
                answerTexts[i].color = new Color32(255, 0, 0, 255);
            }
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
