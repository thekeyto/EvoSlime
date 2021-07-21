using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class net : MonoBehaviour
{
    public float lit1;
    public float lit2;
    float pow2(float x)
    {
        return x * x;
    }

    float distance(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt(pow2(a.x - b.x) + pow2(a.y - b.y) + pow2(a.z - b.z));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "slime")
        {
            if (distance(collision.transform.position, transform.position) <= lit1)
            {
                Debug.Log(1);
                collision.GetComponent<slime>().active = false;
                Destroy(gameObject, 5.0f);
            }
            else if (distance(collision.transform.position, transform.position) <= lit2)
            {
                Debug.Log(2);
                collision.GetComponent<slime>().isHau = true;
                Destroy(gameObject, 1.0f);
            }
        }
        else
            Destroy(gameObject, 1.0f);
    }
}
