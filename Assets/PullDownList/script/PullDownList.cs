using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullDownList : MonoBehaviour
{
    private List<GameObject> itemPanelList;
    public GameObject itemPanel;
    public Inventory handbook;

    private void Awake()
    {
        itemPanelList = new List<GameObject>();
    }
    // Use this for initialization
    void setNewItem(string name,int parent,Item thisitem)
    {
        GameObject tempItemPanel=Instantiate(itemPanel);
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
        setNewItem("星系", 0,null);
        setNewItem("史莱姆", 0,null);
        setNewItem("物品", 0,null);
        refresh();
    }

    void refresh()
    {
        for (int i = 0; i < handbook.itemList.Count; i++)
        {
            Item tempitem = handbook.itemList[i];
            if (tempitem == null) continue;
            if (handbook.itemList[i].classic == "slime")
            {
                setNewItem(tempitem.itemName, 2,tempitem);
            }
            else if (tempitem.classic == "star")
            {
                setNewItem(tempitem.itemName, 1,tempitem);
            }
            else if(tempitem.classic=="thing")
            {
                setNewItem(tempitem.itemName, 3,tempitem);
            }
        }

    }
}
