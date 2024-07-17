using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    // Singleton instance
    public static HUD Instance { get; private set; }

    // Reference to the TextMeshPro component
    [SerializeField] private TextMeshProUGUI hudText;

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
      UpdateHUD(Player.Instance.coins.ToString());  
    }

    // Method to update the HUD text
    public void UpdateHUD(string message)
    {
        if (hudText != null)
        {
            hudText.text = "Coins " + message;
        }
        else
        {
            Debug.LogWarning("HUD Text is not assigned in the inspector.");
        }
    }
}
