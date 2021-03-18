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
    public GameObject backButton;
    public GameObject forwardButton;
    public GameObject[] pages;
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
        ActivateArchive();
    }

    public void ActivateArchive()
    {
        gameObject.SetActive(true);
        pageNumber = keysFound;
        ChangePage();
        Time.timeScale = 0;
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
        switch (pageNumber)
        {
            case 1:
                cyberInfoText.SetText("First Page Information");
                break;
            case 2:
                cyberInfoText.SetText("Secong Page Information");
                break;
            case 3:
                cyberInfoText.SetText("Third Page Information");
                break;
        }
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
