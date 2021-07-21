using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bush : MonoBehaviour
{
    public Slider getSlider;
    public float getTime;
    float capturetime;
    public GameObject getCan;

    bool ifGet;
    ItemOnWorld bushItem;

    private void Start()
    {
        ifGet = false;
        bushItem = GetComponent<ItemOnWorld>();
        getCan.SetActive(false);
    }
    IEnumerator waitEatTime(slime slime)
    {
        yield return new WaitForSeconds(0.5f);
        slime.slimeAni.SetBool("eat", false);
        GrassPool.instance.desGrass(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            if (Input.GetKey(KeyCode.F))
            {
                Debug.Log(capturetime);
                getCan.SetActive(true);
                ifGet = true;
            }
            else
            {
                ifGet = false;
                capturetime = 0;
                getCan.SetActive(false);
            }
        }
        if (collision.tag == "slime"&& collision.GetComponent<slime>().isHan == true)
        {
            collision.GetComponent<slime>().slimeAni.SetBool("eat", true);
            StartCoroutine(waitEatTime(collision.GetComponent<slime>()));
            collision.GetComponent<slime>().isHan = false;
            collision.GetComponent<SlimeAction>().foodTime = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            if (Input.GetKey(KeyCode.F))
            {
                Debug.Log(capturetime);
                getCan.SetActive(true);
                ifGet = true;
            }
            else
            {
                ifGet = false;
                capturetime = 0;
                getCan.SetActive(false);
            }
        }
        if (collision.tag == "slime" && collision.GetComponent<slime>().isHan == true)
        {
            collision.GetComponent<slime>().slimeAni.SetBool("eat", true);
            StartCoroutine(waitEatTime(collision.GetComponent<slime>()));
            collision.GetComponent<slime>().isHan = false;
            collision.GetComponent<SlimeAction>().foodTime = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ifGet = false;
        capturetime = 0;
        getCan.SetActive(false);
    }

    private void Update()
    {
        if (ifGet==true&&Input.GetKey(KeyCode.F))
        {
            capturetime += Time.deltaTime;
        }
        getSlider.value = capturetime / getTime;
        if (capturetime >= getTime)
        {
            getCan.SetActive(false);
            bushItem.AddNewItem();
            capturetime = 0;
            GrassPool.instance.desGrass(gameObject);
        }
    }
}
