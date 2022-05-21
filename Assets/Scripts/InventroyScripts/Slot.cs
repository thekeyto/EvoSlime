using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotID;
    public Item slotItem;
    public Image slotImage;
    public Text itemName;
    public Text itemNumber;
    public Text itemDescription;
    public GameObject grid;
    public void  setupslot(Item thisItem,string tag)
    {
        grid.tag = tag;
        slotImage.tag = tag;
        if(itemNumber!=null)
        itemNumber.tag = tag;
        if (thisItem == null) return;
        //Debug.Log(thisItem.name);
        //Debug.Log(thisItem.itemNumber);
        slotItem = thisItem;
        slotImage.sprite = thisItem.itemImage;
        if (itemNumber != null)
            itemNumber.text = thisItem.itemNumber.ToString();
        if (itemName != null)
            itemName.text = thisItem.itemName;
            transform.localScale = new Vector3(1, 1, 1);
    }

    public void set_eleGene()
    {
        playerUI.instance.set_elementGene(slotItem);
    }

    public void setDescription()
    {
        if (itemDescription != null)
            itemDescription.text = slotItem.description;
    }

    public void setElementWay()
    {
        if (mainMenuUI.instance != null&&slotItem.myClassic!=Item.classic.thing)
        {
            mainMenuUI.instance.set_elementGene(slotItem);
            mainMenuUI.instance.elementWay();
        }
    }
}
