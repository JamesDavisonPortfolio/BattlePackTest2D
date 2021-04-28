using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPanelScript : MonoBehaviour
{

    public int keysFound;
    public int pageNumber;
    public TextMeshProUGUI cyberInfoText;
    public TextMeshProUGUI cyberPageNumber;
    public DataCarrier levelData;
    public GameObject backButton;
    public GameObject forwardButton;
    public GameObject[] pages;
    public GameObject bossBarrier;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && pageNumber > 1)
        {
            PageBack();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && pageNumber < keysFound)
        {
            PageForward();
        }
    }

    public void Return()
    {

    }
    public void keyPickedUp()
    {
        keysFound++;
        if(keysFound == 5)
        {
            Destroy(bossBarrier);
        }
        ActivateArchive();
    }

    public void ActivateArchive()
    {
        gameObject.SetActive(true);
        if(keysFound > 0)
        {
            pageNumber = keysFound;
            ChangePage();
            Time.timeScale = 0;
        }
        else
        {
            cyberInfoText.SetText("No keys found.");
        }
    }

    public void PageForward()
    {
        pageNumber++;
        ChangePage();
    }

    public void PageBack()
    {
        pageNumber--;
        ChangePage();
    }

    void ChangePage()
    {
        cyberInfoText.SetText(levelData.levelInformation[pageNumber - 1]);
        cyberPageNumber.SetText(pageNumber.ToString() + " / " + keysFound.ToString());
        CheckButtons();
    }

    void CheckButtons()
    {
        if (pageNumber == 1)
        {
            backButton.SetActive(false);
        }
        else if (pageNumber > 1)
        {
            backButton.SetActive(true);
        }

        if (pageNumber == keysFound)
        {
            forwardButton.SetActive(false);
        }
        else if (pageNumber < keysFound)
        {
            forwardButton.SetActive(true);
        }
    }
}
