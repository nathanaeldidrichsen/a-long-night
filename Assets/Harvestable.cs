using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    // Enum to specify the type of harvestable object
    public enum HarvestableType { Tree, Ore }
    public HarvestableType harvestableType;
    private Animator anim;

    // Maximum health value for the harvestable object
    public float maxHealth = 100f;

    // Current health value
    private float currentHealth;

    // Recovery counter (time interval between taking damage)
    public float recoveryTime = 0.2f;

    // Whether the object can be harvested
    private bool canBeHarvested = true;
    public GameObject hurtParticle;


    // List of possible items to drop when destroyed
    public List<GameObject> dropItems;

    // Drop chance for each item in the list
    public float dropChance = 0.5f; // 50% chance

    void Start()
    {
        // Initialize current health to maximum health
        currentHealth = maxHealth;
        if(gameObject.GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
    }

    // Method to take damage
    public void TakeDamage(float damageAmount)
    {
        if (canBeHarvested)
        {
            // Reduce current health by the damage amount
            currentHealth -= damageAmount;
                        if(hurtParticle != null)
            {
            anim.Play("hurt");
            Instantiate(hurtParticle,transform.position, Quaternion.identity);
            }

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

    // Method to handle destruction of the object
    private void Die()
    {
        // Handle the object's destruction (e.g., play an animation, etc.)
        Debug.Log(gameObject.name + " has been destroyed!");

        // Attempt to drop items
        DropItems();

        // Destroy the game object
        Destroy(gameObject);
    }

    // Coroutine for recovery time
    private IEnumerator Recovery()
    {
        // Set canBeHarvested to false
        canBeHarvested = false;

        // Wait for the recovery time
        yield return new WaitForSeconds(recoveryTime);

        // Set canBeHarvested to true
        canBeHarvested = true;
    }

    // Method to attempt dropping items
    private void DropItems()
    {
        foreach (GameObject item in dropItems)
        {
            // Generate a random value between 0 and 1
            float randomValue = Random.value;

            // Check if the random value is less than the drop chance
            if (randomValue < dropChance)
            {
                // Instantiate the item at the current position with no rotation
                GameObject drop = Instantiate(item, transform.position, Quaternion.identity);
                drop.GetComponent<Ejector>().launchOnStart = true;
            }
        }
    }

    // Method to heal the harvestable object (if necessary)
    public void Heal(float healAmount)
    {
        // Increase current health by the heal amount, ensuring it doesn't exceed max health
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
    }

    // OnTriggerEnter2D method to handle damage based on tags
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check the type of harvestable object and the tag of the other collider
        // if ((harvestableType == HarvestableType.Tree && other.CompareTag("Axe")) ||
        //     (harvestableType == HarvestableType.Ore && other.CompareTag("Pickaxe")))
        // {
        //     // Take damage (you can adjust the damage amount as needed)
        //     TakeDamage(10f); // Example damage amount
        // }


        if ((harvestableType == HarvestableType.Tree && other.CompareTag("PlayerAttack")) ||
    (harvestableType == HarvestableType.Ore && other.CompareTag("PlayerAttack")))
        {
            TakeDamage(1); // Example damage amount
        }
    }
}
