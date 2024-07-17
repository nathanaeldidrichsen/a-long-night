using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Misc Item", menuName = "Item/Misc")]
public class MiscItem : Item
{
    public override Item GetItem() {return this;}
    public override ToolItem  GetTool() {return null;}
    public override MiscItem  GetMisc() {return this;}
    public override ConsumableItem  GetConsumable() {return null;}

}
