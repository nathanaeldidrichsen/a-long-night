using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Tool Item", menuName = "Item/Tool")]
public class ToolItem : Item
{
    [Header("Tool")]
    public ToolType tooltype;

    public enum ToolType
    {
        Axe,
        Hammer,
        Pickaxe,
        weapon
    }

    public override Item GetItem() {return this;}
    public override ToolItem  GetTool() {return this;}
    public override MiscItem  GetMisc() {return null;}
    public override ConsumableItem  GetConsumable() {return null;}

}
