using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    Transform initialParent;
    private void Awake()
    {
        initialParent = transform.parent;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                gameObject.transform.SetParent(collision.gameObject.transform);
            }
            else
            {
                gameObject.transform.SetParent(collision.gameObject.transform.parent);
            }
        }
        else
        {
            gameObject.transform.SetParent(initialParent);
        }
    }
}
