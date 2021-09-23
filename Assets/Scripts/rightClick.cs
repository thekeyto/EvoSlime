using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class rightClick : MonoBehaviour
{
    public UnityEvent rightClk;

    private void Start()
    {
        rightClk.AddListener(new UnityAction(buttonRightClick));
    }

    void buttonRightClick()
    {
        playerUI.instance.set_elementGene(gameObject.GetComponent<Slot>().slotItem);
        playerUI.instance.elementWay();
    }
}
