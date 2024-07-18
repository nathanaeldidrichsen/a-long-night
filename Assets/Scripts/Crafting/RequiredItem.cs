using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequiredItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Item reqitem;

    public void SetRequiredItem(Item item)
    {
        reqitem = item;
        image.sprite = reqitem.itemIcon;
    }

    public Item GetRequiredItem()
    {
        return reqitem;
    }
}
