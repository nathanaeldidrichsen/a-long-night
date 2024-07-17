using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Consumable Item", menuName = "Item/Consumable")]
public class ConsumableItem : Item
{

    [Header("Consumable")]
    public int healthAdded;

    public override Item GetItem() {return this;}
    public override ToolItem  GetTool() {return null;}
    public override MiscItem  GetMisc() {return null;}
    public override ConsumableItem  GetConsumable() {return this;}


}
