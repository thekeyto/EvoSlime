using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="handbookInventory",menuName ="Inventory/New Handbook Inventory")]
public class handbookInventory : ScriptableObject
{
    public List<handbookItem> itemlist = new List<handbookItem>();
}
