using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class researchUI : MonoBehaviour
{
    public Item nowItem;
    public Text descrption;
    public void setDescription()
    {
        descrption.text = nowItem.description;
    }
}
