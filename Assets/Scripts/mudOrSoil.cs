using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mudOrSoil : MonoBehaviour
{
    SpriteRenderer rockRender;
    public Sprite soil;
    public Sprite mud;
    public bool if_soil;
    private void Start()
    {
        rockRender = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "net")
        {
            if (collision.GetComponent<net>().ability == "mudToSoil" && if_soil == false)
            {
                GetComponent<BoxCollider2D>().isTrigger = true;
                rockRender.sprite = soil;
                if_soil = true;
            }
            if (collision.GetComponent<net>().ability == "soilToMud" && if_soil == true)
            {
                GetComponent<BoxCollider2D>().isTrigger = false;
                rockRender.sprite = soil;
                if_soil = false;
            }
        }
    }
}
