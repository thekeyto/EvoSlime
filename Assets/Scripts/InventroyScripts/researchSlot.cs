using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class researchSlot : MonoBehaviour
{
    public int slotID;
    public Item slotItem;
    public Image slotImage;
    public Text itemNumber;
    public Text itemName;
    public GameObject grid;
    public void setupslot(Item thisItem, string tag)
    {
        grid.tag = tag;
        slotImage.tag = tag;
        itemNumber.tag = tag;
        if (thisItem == null) return;
        //Debug.Log(thisItem.name);
        //Debug.Log(thisItem.itemNumber);
        slotItem = thisItem;
        slotImage.sprite = thisItem.itemImage;
        itemName.text = thisItem.itemName;
        
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void set_eleGene()
    {
        //playerUI.instance.set_elementGene(slotItem);
    }
}
