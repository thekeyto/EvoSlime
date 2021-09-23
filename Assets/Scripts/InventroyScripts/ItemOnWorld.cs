using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;
    public Inventory playerInventory;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player") && Input.GetKeyDown(KeyCode.F))
        {
            collision.gameObject.GetComponent<player>().playerAni.SetBool("attack", true);
            Debug.Log("catch");
            AddNewItem();
            if (gameObject.tag == "slime") slimePool.instance.desSlime(gameObject);
            if (gameObject.tag == "grass") GrassPool.instance.desGrass(gameObject);
        }
    }
    public void AddNewItem()
    {
        thisItem.ifFound = true;
        if (!playerInventory.itemList.Contains(thisItem))
        {
            for (int i = 0; i < playerInventory.itemList.Count; i++)
            {
                if (playerInventory.itemList[i] == null)
                {
                    playerInventory.itemList[i] = thisItem;
                    break;
                }
            }
        }
        else
        {
            thisItem.itemNumber += 1;
        }
        InventoryManager.RefreshItem();
    }
}
