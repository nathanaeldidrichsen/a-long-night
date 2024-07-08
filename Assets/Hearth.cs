using System.Collections.Generic;
using UnityEngine;

public class Hearth : MonoBehaviour, Buildable
{
    // Singleton instance
    public static Hearth Instance { get; private set; }

    // List to keep track of all buildings
    private List<Building> buildings = new List<Building>();

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
