using UnityEngine;

public class Attack : MonoBehaviour
{
    // Damage amount to apply
    public int damage = 1;
    public bool isEnemy;
    public bool isPlayer;


    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the "Player" or "Enemy" tag
        if (isEnemy && other.CompareTag("Player") || isEnemy && other.CompareTag("PlayerStructure"))
        {
            // Get the Health component from the collided object
            Health health = other.GetComponent<Health>();

            // If the Health component exists, apply damage
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

            // Check if the collided object has the "Player" or "Enemy" tag
        if (isPlayer && other.CompareTag("Enemy"))
        {
            // Get the Health component from the collided object
            Health health = other.GetComponent<Health>();

            // If the Health component exists, apply damage
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
