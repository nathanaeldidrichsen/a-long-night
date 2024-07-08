using System.Collections.Generic;
using UnityEngine;

public class Hearth : MonoBehaviour, Buildable
{
    // Singleton instance
    public static Hearth Instance { get; private set; }

    // List to keep track of all buildings
    private List<Building> buildings = new List<Building>();
    public GameObject fencePrefab;

    void Awake()
    {
        // Implement the singleton pattern
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


    public void SpawnFence(Transform startEdge)
    {
        // Get the start and end positions
        Vector3 startPosition = startEdge.position;
        Vector3 endPosition = transform.position;

        // Calculate the distance between the start and end positions
        float distance = Vector3.Distance(startPosition, endPosition);

        // Instantiate the fence prefab
        GameObject fence = Instantiate(fencePrefab, startPosition, Quaternion.identity, transform);

        // Calculate the direction from start to end
        Vector3 direction = (endPosition - startPosition).normalized;

        // Set the position and size of the fence
        fence.transform.position = (startPosition + endPosition) / 2; // Set position to the midpoint
        SpriteRenderer sr = fence.GetComponent<SpriteRenderer>();

        if (sr != null)
        {
            // Assuming the fence sprite is 1 unit wide
            sr.size = new Vector2(distance, sr.size.y);
        }

        // Rotate the fence to align with the direction
        fence.transform.right = direction;
    }

    // Function to add a building to the list
    public void AddBuilding(Building building)
    {
        if (!buildings.Contains(building))
        {
            buildings.Add(building);
        }
    }

    // Function to remove a building from the list
    public void RemoveBuilding(Building building)
    {
        if (buildings.Contains(building))
        {
            buildings.Remove(building);
        }
    }

    // Function to upgrade all buildings
    public void UpgradeAllBuildings()
    {
        foreach (Building building in buildings)
        {
            ((Buildable)building).Upgrade();
        }
    }

    // Implementing the Building interface

    public void Upgrade()
    {
        // Code to upgrade the hearth itself
        Debug.Log("Hearth upgraded!");
    }

    public void Sell()
    {
        // Code to sell the hearth
        Debug.Log("Hearth sold!");
    }
}
