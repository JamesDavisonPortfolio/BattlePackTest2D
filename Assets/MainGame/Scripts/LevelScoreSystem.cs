using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;


public class LevelScoreSystem : MonoBehaviour
{
    public DataCarrier levelData;
    public FinalData fullData;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    public int levelScore;

    private int actualScore;
    private int currentTotalScore;
    private int minutes;
    private int seconds;

    private float startTime;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        currentTotalScore = fullData.getCurrentTotalScore();
        scoreText.SetText(currentTotalScore.ToString());
        TimeUpdate();
        actualScore = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        TimeUpdate();
        ScoreUpdate();
    }

    void ScoreUpdate()
    {
        actualScore = (( levelScore - ((int)currentTime ) * 10));
        scoreText.SetText("Score: " + actualScore.ToString("00000"));
    }

    public void SubmitLevelScore()
    {
        TimeUpdate();
        levelData.SetTimeToComplete(minutes, seconds);
        levelData.SetLevelScore(actualScore);
    }

    void TimeUpdate()
    {
        currentTime = Time.time - startTime;
        minutes = ((int)currentTime / 60);
        seconds = ((int)currentTime % 60);

        timerText.SetText(minutes.ToString("00") + ":" + seconds.ToString("00"));
    }
}
