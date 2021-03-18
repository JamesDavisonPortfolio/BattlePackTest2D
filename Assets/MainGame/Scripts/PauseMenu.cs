using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void ResumeGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
