using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class pressForLongTime : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private float delay = 0.5f;

    private bool isDown = false;
    private float lastIsDownTime;

    void Update()
    {
        if (isDown)
        {
            if (playerUI.instance.elementWayUI.active == true) lastIsDownTime = Time.time;
            if (Time.time - lastIsDownTime > delay)
            {
                playerUI.instance.set_elementGene(gameObject.GetComponent<Slot>().slotItem);
                playerUI.instance.elementWay();
                Debug.Log("element");
                lastIsDownTime = Time.time;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
        lastIsDownTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isDown = false;
    }
}
