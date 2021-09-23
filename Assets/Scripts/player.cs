using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float netLit;
    public float speed;
    public float bulletSpeed;
    public GameObject playerBag;
    public GameObject net;
    public GameObject geneGun;

    int directh = 1;
    private Rigidbody2D rigidbody;
    private SpriteRenderer playerSprite;
    public Animator playerAni;
    public static player instance;

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

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
    static float pow2(float x)
    {
        return x * x;
    }

    public static float distance(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt(pow2(a.x - b.x) + pow2(a.y - b.y) + pow2(a.z - b.z));
    }

    Vector3 normalize(Vector3 a,Vector3 b)
    {
        return (a - b) / distance(a, b);
    }

    void shoot()
    {
        if(Input.GetMouseButtonDown(0)&&playerUI.instance.if_UIOpen==false)
        {
            playerAni.SetBool("attack", true);
            var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 mousePositionOnScreen = Input.mousePosition;
            mousePositionOnScreen.z = screenPosition.z;
            var mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
            mousePositionInWorld.z = 0;

            float m_fireAngle = Vector2.Angle(mousePositionInWorld - this.transform.position, Vector2.up);
            if(mousePositionInWorld.x>this.transform.position.x) m_fireAngle = -m_fireAngle;
            GameObject tempnet = Instantiate(net,transform.position, Quaternion.identity) as GameObject;
            tempnet.GetComponent<Rigidbody2D>().velocity= (mousePositionInWorld - transform.localPosition).normalized * bulletSpeed;
            tempnet.transform.eulerAngles = new Vector3(0, 0, m_fireAngle);
            Debug.Log(mousePositionInWorld);
            if (geneGun.GetComponent<geneGun>().gel != null)
            {
                tempnet.GetComponent<net>().gelgene = geneGun.GetComponent<geneGun>().gel.Genes;
                tempnet.GetComponent<net>().ability = elementManager.elementRect(geneGun.GetComponent<geneGun>().gel.Genes);
                tempnet.GetComponent<net>().netColor = geneGun.GetComponent<geneGun>().gelColor;
                geneGun.GetComponent<geneGun>().gel.itemNumber--;
                if (geneGun.GetComponent<geneGun>().gel.itemNumber == 0) geneGun.GetComponent<geneGun>().gel = null;
            }
            float step = bulletSpeed * Time.deltaTime;
            
            if (distance(mousePositionInWorld, transform.position) <= netLit)
            {
                tempnet.GetComponent<net>().destination = mousePositionInWorld;
            }
            else
            {
                tempnet.GetComponent<net>().destination=transform.localPosition+(mousePositionInWorld - transform.localPosition).normalized * netLit;
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
