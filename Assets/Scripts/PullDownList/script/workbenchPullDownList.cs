using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class workbenchPullDownList : MonoBehaviour
{
    private List<GameObject> itemPanelList;
    public GameObject itemPanel;
    public Inventory playerBag;

    private void Awake()
    {
        itemPanelList = new List<GameObject>();
    }
    // Use this for initialization
    void setNewItem(string name, int parent, Item thisitem)
    {
        GameObject tempItemPanel = Instantiate(itemPanel);
        if (thisitem) tempItemPanel.GetComponent<ItemPanel>().panelItem = thisitem;
        itemPanelList.Add(tempItemPanel);
        tempItemPanel.GetComponent<ItemPanelBase>().InitPanelContent(new ItemBean(name));
        if (parent == 0)
        {
            tempItemPanel.GetComponent<ItemPanelBase>().SetBaseParent(this.transform);
        }
        else
            tempItemPanel.GetComponent<ItemPanelBase>().SetItemParent(itemPanelList[parent].GetComponent<ItemPanelBase>());
    }
    void Start()
    {
        itemPanelList.Add(gameObject);
        refresh();
    }

    void refresh()
    {
        for (int i = 0; i < playerBag.itemList.Count; i++)
        {
            Item tempitem = playerBag.itemList[i];
            if (tempitem == null) continue;
            Debug.Log(playerBag.itemList[i].myClassic);
            Debug.Log(Item.classic.slime);
            if (playerBag.itemList[i].myClassic == Item.classic.slime)
            {
                setNewItem(tempitem.itemName, 0, tempitem);
            }
        }

    }
}
