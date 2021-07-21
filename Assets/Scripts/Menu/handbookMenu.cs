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
        picture.sprite = handbookItem.itemImage;
        name.text = handbookItem.itemName;
        describe.text = handbookItem.description;
    }
}
