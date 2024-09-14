using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    bool done = false;
    [SerializeField] GameObject textPopUp;
    [SerializeField] GameObject doneTextPopUp;
    [SerializeField] GameObject pineApple;
    [SerializeField] MovingPlatforms platform;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovment>().hasPineApple == true)
            {
                done = true;
                Destroy(pineApple);
                platform.active = true;
            }
            if (done == false)
            {
                textPopUp.SetActive(true);
            }
            else
            {
                doneTextPopUp.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (done == false)
            {
                textPopUp.SetActive(false);
            }
            else
            {
                doneTextPopUp.SetActive(false);
            }
        }
    }
}
