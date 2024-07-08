using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    //     void OnCollisionEnter2D(Collision2D other)
    // {
    //             if(other.gameObject.CompareTag("Player"))
    //     {
    //         Player.Instance.GetCoin();
    //         Destroy(other.gameObject);
    //     }
    // }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.GetCoin();
            Destroy(gameObject);
        }
    }
}
