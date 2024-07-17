using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    [SerializeField] private Item item;
    public int amount = 1;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && item == null)
        {
            Player.Instance.GetCoin();
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player") && item != null)
        {
            InventoryManager.Instance.AddItem(item, amount);
            Destroy(gameObject);
        }
    }
}
