using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public GameObject infoPanel;

    //Register collision and if it is the player runs appropriate code to bring up game info
    //also checks tag to ensure colliion code only runs once as other colliders are present on 
    //player, this stops multiple instances of code activating.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("KeyInput"))
        {
            infoPanel.GetComponent<InfoPanelScript>().keyPickedUp();
            infoPanel.SetActive(true);
            Destroy(gameObject);
        }
    }

}
