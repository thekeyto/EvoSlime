using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public Inventory mybag;
    public GameObject slotGrid;
    //public slot slotPrefab;
    public GameObject emptyslot;
    public Text itemInformation;
    public List<GameObject> slots = new List<GameObject>();
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    private void OnEnable()
    {
        RefreshItem();
        //instance.itemInformation.text = "";
    }
    public static void UpdateItemInfo(string itemDicripotion)
    {
        instance.itemInformation.text = itemDicripotion;
    }
    /* public static void CreateNewItem(Item item)
     {
         slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
         newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
         newItem.slotItem = item;
         newItem.slotImage.sprite = item.itemImage;
         newItem.slotNum.text = item.itemHeld.ToString();
     }*/

    public static void RefreshItem()
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }

        for (int i = 0; i < instance.mybag.itemList.Count; i++)
        {
            instance.slots.Add(Instantiate(instance.emptyslot,instance.slotGrid.transform));
            instance.slots[i].GetComponent<Slot>().slotID = i;
            instance.slots[i].GetComponent<Slot>().setupslot(instance.mybag.itemList[i]);
        }
    }
}
