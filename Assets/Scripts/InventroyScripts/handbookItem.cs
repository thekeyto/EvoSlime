using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Handbook",menuName ="Inventory/New Hanbook")]
public class handbookItem : ScriptableObject
{
    public bool ifFound;
    public string name;
    public string classic;
    public Sprite picture;
    [TextArea]
    public string description;
}
