using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Recipe", menuName = "Crafting/Recipe")]

public class Recipe : ScriptableObject
{
    public string recipeName;
    public List<Item> requiredItems; //the required items will be a string ex. woodwoodore
    public Item craftedItem;
}
