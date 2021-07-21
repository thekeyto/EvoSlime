using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Workbench : MonoBehaviour
{
    public Item parent;
    public Image parentImage;
    public void getItem(Item tempItem)
    {
        parent = tempItem;
    }

    private void Update()
    {
        if (parent != null)
            parentImage.sprite = parent.itemImage;
        else
            parentImage.sprite = null;
    }
}
