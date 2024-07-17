using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Maximum health value
    public int maxHealth = 10;
    
    // Current health value
    public int currentHealth;
    
    // Recovery counter (time interval between taking damage)
    public float recoveryTime = 0.2f;
    private Animator anim;
    
    // Whether the object can take damage
    private bool canTakeDamage = true;
    public GameObject hurtParticle;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        // Initialize current health to maximum health
        currentHealth = maxHealth;
    }

    // Method to take damage
    public void TakeDamage(int damageAmount)
    {
        if (canTakeDamage)
        {
            if(anim != null)
            {
            if(hurtParticle != null)
            {
            Instantiate(hurtParticle,transform.position, Quaternion.identity);
            }
            anim.Play("hurt");
            }
            // Reduce current health by the damage amount
            currentHealth -= damageAmount;

            // Check if health is below or equal to zero
            if (currentHealth <= 0)
            {
                // Call the Die method if health is zero or less
                Die();
            }
            else
            {
                // Start the recovery coroutine
                StartCoroutine(Recovery());
            }
        }
    }

    // Method to die
    public void Die()
    {
        // Handle the object's death (e.g., destroy the object, play an animation, etc.)
        Debug.Log(gameObject.name + " has died!");
        Destroy(gameObject); // Destroy the game object
    }

    // Coroutine for recovery time
    private IEnumerator Recovery()
    {
        // Set canTakeDamage to false
        canTakeDamage = false;

        // Wait for the recovery time
        yield return new WaitForSeconds(recoveryTime);

        // Set canTakeDamage to true
        canTakeDamage = true;
    }

    // Method to heal
    public void Heal(int healAmount)
    {
        // Increase current health by the heal amount, ensuring it doesn't exceed max health
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
    }
}
