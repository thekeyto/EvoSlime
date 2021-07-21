using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float netLit;
    public float speed;
    public GameObject playerBag;
    public GameObject net;

    int directh = 1;
    private Rigidbody2D rigidbody;
    private SpriteRenderer playerSprite;
    public Animator playerAni;
    void Start()
    {
        playerAni = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        playerBag.SetActive(true);
        playerBag.SetActive(false);
    }

    // Update is called once per frame

    void Flip(float h)
    {
        Vector3 temp = transform.localScale;
        if ((h > 0.1f && directh == 0) || (h < -0.1f && directh == 1))
        {
            temp.x *= -1;
            directh ^= 1;
        }
        transform.localScale = temp;
    }

    void playerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 input = (h*transform.right+ v*transform.up).normalized;
        Flip(h);
        if (input.x != 0 || input.y != 0)
            playerAni.SetBool("isRun", true);
        else playerAni.SetBool("isRun", false);
        rigidbody.velocity = input * speed;
    }

    void Mybag()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (playerBag.active == false) playerBag.SetActive(true);
            else playerBag.SetActive(false);
            InventoryManager.RefreshItem();
        }
    }
    float pow2(float x)
    {
        return x * x;
    }

    float distance(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt(pow2(a.x - b.x) + pow2(a.y - b.y) + pow2(a.z - b.z));
    }
    void shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            playerAni.SetBool("attack", true);
            var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 mousePositionOnScreen = Input.mousePosition;
            mousePositionOnScreen.z = screenPosition.z;
            var mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
            mousePositionInWorld.z = 0;
            GameObject tempslime = Instantiate(net,transform.parent) as GameObject;
            Debug.Log(mousePositionInWorld);
            if (distance(mousePositionInWorld, transform.position) <= netLit)
            {
                tempslime.transform.position = mousePositionInWorld;
            }
            else
            {
                tempslime.transform.position = (mousePositionInWorld - transform.position) / distance(mousePositionInWorld, transform.position) * netLit;
            }
        }
    }

    void Update()
    {
        playerMove();
        Mybag();
        shoot();
    }
}
