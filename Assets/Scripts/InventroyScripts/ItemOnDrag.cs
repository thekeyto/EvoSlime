using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Transform originalParent;
    public Inventory mybag;
    public Text number;
    private int currentItemID;
    public Item currentItem;
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentItem = originalParent.GetComponent<Slot>().slotItem;
        currentItemID = originalParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        if(eventData.pointerCurrentRaycast.gameObject!=null)
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerPressRaycast.gameObject != null)
        { 
            if(eventData.pointerCurrentRaycast.gameObject==null)
            {
                transform.SetParent(originalParent);
                transform.position = originalParent.position;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
            if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;

                var temp = mybag.itemList[currentItemID];
                mybag.itemList[currentItemID] = mybag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
                mybag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;

                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
            if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                mybag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = mybag.itemList[currentItemID];
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID != currentItemID)
                    mybag.itemList[currentItemID] = null;
                return;
            }

            if (eventData.pointerCurrentRaycast.gameObject.tag=="workbench")
            {
                Workbench tempParent = eventData.pointerCurrentRaycast.gameObject.GetComponent<Workbench>();
                if (tempParent.parent != null) tempParent.parent.itemNumber++;
                eventData.pointerCurrentRaycast.gameObject.GetComponent<Workbench>().getItem(currentItem);
                transform.SetParent(originalParent);
                transform.position = originalParent.position;
                currentItem.itemNumber--;
                InventoryManager.RefreshItem();
                return;
            }

            if (eventData.pointerCurrentRaycast.gameObject.name == "WBImage")
            {
                Workbench tempParent = eventData.pointerCurrentRaycast.gameObject.transform.GetComponentInParent<Workbench>();
                if (tempParent.parent != null) tempParent.parent.itemNumber++;
                eventData.pointerCurrentRaycast.gameObject.transform.GetComponentInParent<Workbench>().getItem(currentItem);
                transform.SetParent(originalParent);
                transform.position = originalParent.position;
                currentItem.itemNumber--;
                InventoryManager.RefreshItem();
                return;
            }

            transform.SetParent(originalParent);
                transform.position = originalParent.position;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
        }
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}