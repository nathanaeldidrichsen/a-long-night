using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Building : MonoBehaviour, Buildable
{
    public GameObject ui;
    public Animator uiAnim;
    public List<GameObject> upgrades;
    public int currentUpgradeNumber;
    public GameObject currentBuilding;
    private bool pressedE;
    private bool playerInRange;
    public bool isCrate = true;
    public bool isDestroyable = true;
    private Health health;
    [SerializeField] private GameObject CrateBuilding;

    void Start()
    {
        health = GetComponent<Health>();
    }

    // Adjusted OnTriggerStay2D to handle UI activation and input detection
    void Update()
    {
        if (playerInRange && !pressedE && currentUpgradeNumber < upgrades.Count)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Pressed E key");
                Upgrade();
                StartCoroutine(PressedE());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && currentUpgradeNumber < upgrades.Count)
        {
            playerInRange = true;
            ui.SetActive(true);
            uiAnim.Play("idle");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && currentUpgradeNumber < upgrades.Count)
        {
            // This block can be used for other logic if needed while the player is in range
        }
    }

    public void ResetToCrate()
    {
        health.currentHealth = health.maxHealth;
        isCrate = true;
        Destroy(currentBuilding);
        currentBuilding = Instantiate(CrateBuilding, transform.position, Quaternion.identity, transform);
        currentUpgradeNumber = 0;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            uiAnim.Play("fadeout");
            ui.SetActive(false);
        }
    }

    public IEnumerator PressedE()
    {
        pressedE = true;
        yield return new WaitForSeconds(1);
        pressedE = false;
    }

public void Upgrade()
{
    if (currentUpgradeNumber < upgrades.Count && Player.Instance.coins > 0)
    {
        health.maxHealth += 2;
        health.currentHealth = health.maxHealth;
        isCrate = false;
        Player.Instance.SpendCoins(1);
        uiAnim.Play("buy");
        Destroy(currentBuilding);
        currentBuilding = Instantiate(upgrades[currentUpgradeNumber], transform.position, Quaternion.identity, transform);
        currentUpgradeNumber++; // Increment upgrade number after upgrade
    }
}
}