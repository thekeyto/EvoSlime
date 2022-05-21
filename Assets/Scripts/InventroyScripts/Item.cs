using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="New Item",menuName ="Inventory/New Item")]
public class Item : ScriptableObject
{
    public enum classic { thing,slime,star,gel};
    public classic myClassic;
    public bool ifFound;
    public int itemNumber;
    public string itemName;
    public Sprite itemImage;
    [TextArea ]
    public string Genes;
    public string description;
}


