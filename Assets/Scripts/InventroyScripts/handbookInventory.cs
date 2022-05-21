using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="playerBagInventory",menuName ="Inventory/New playerBag Inventory")]
public class playerBagInventory : ScriptableObject
{
    public List<Item> itemlist = new List<Item>();
}
