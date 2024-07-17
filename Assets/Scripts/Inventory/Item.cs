using UnityEngine;

public abstract class Item : ScriptableObject
{
    [Header("Item")]
    public string itemName;
    public Sprite itemIcon;
    public bool isStackable = true;

    public abstract Item GetItem();
    public abstract ToolItem GetTool();
    public abstract MiscItem GetMisc();
    public abstract ConsumableItem GetConsumable();

}
