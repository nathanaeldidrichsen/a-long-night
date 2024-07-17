using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBuilding : MonoBehaviour
{
    [SerializeField] private GameObject arrowUI;
    [SerializeField] private GameObject ui;


    private bool pressedE;
    private bool playerInRange;


    void Update()
    {
        if (playerInRange && !pressedE)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Pressed E key");
                ui.SetActive(true);
                arrowUI.SetActive(false);
                StartCoroutine(PressedE());
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            arrowUI.SetActive(true);

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            arrowUI.SetActive(false);
            ui.SetActive(false);

        }
    }

    public IEnumerator PressedE()
    {
        pressedE = true;
        yield return new WaitForSeconds(1);
        pressedE = false;
    }

}
