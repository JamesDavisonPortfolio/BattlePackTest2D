using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossContact : MonoBehaviour
{
    public LevelScoreSystem levelScore;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("touch happened");
        if (other.CompareTag("KeyInput"))
        {
            Debug.Log("boss touched");
            levelScore.SubmitLevelScore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
