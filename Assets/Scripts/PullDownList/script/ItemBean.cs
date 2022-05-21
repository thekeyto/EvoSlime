using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBean:ItemBeanbase
{

    public string name { get; set; }
    public int age { get; set; }
    public ItemBean(string _name)
    {
        name = _name;
    }
}
