using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class handbookMenu : MonoBehaviour
{
    Item menuItem;
    public static handbookMenu instance;

    public Image picture;
    public Text name;
    public Text describe;
    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

    public void updateMenu(Item handbookItem)
    {
        Debug.Log("update");
        if (handbookItem.ifFound == true)
        {
            picture.sprite = handbookItem.itemImage;
            name.text = handbookItem.itemName;
            describe.text = handbookItem.description;
        }
        else
        {
            picture.sprite = handbookItem.itemImage;
            picture.color = new Color(0, 0, 0);
            describe.text = "还未得到该物品的信息";
        }
    }
    public void exit()
    {
        instance.gameObject.SetActive(false);
    }
}
