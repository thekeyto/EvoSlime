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
            playerUI.instance.bagUI();
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

    void changeTile(Vector3 mousePositionInWorld)
    {
        GetComponent<mudAndConcreate>().cellTransform.position = mousePositionInWorld;
        GetComponent<mudAndConcreate>().changeTile1();
    }

    void shoot()
    {
        if(Input.GetMouseButtonDown(0)&&playerUI.instance.if_UIOpen==false)
        {
            playerAni.SetBool("attack", true);
            var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 mousePositionOnScreen = Input.mousePosition;
            Debug.Log(screenPosition);
            mousePositionOnScreen.z = screenPosition.z;
            Debug.Log(mousePositionOnScreen);
            var mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
            mousePositionInWorld.z = 0;
            Debug.Log(mousePositionInWorld);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitt = new RaycastHit();
            Physics.Raycast(ray, out hitt, 100);
            if (hitt.transform != null) Debug.Log(hitt.transform);

            float m_fireAngle = Vector2.Angle(mousePositionInWorld - this.transform.position, Vector2.up);
            if(mousePositionInWorld.x>this.transform.position.x) m_fireAngle = -m_fireAngle;
            GameObject tempnet = Instantiate(net,transform.position, Quaternion.identity) as GameObject;
            tempnet.GetComponent<Rigidbody2D>().velocity= (mousePositionInWorld - transform.localPosition).normalized * bulletSpeed;
            tempnet.transform.eulerAngles = new Vector3(0, 0, m_fireAngle);
            if (geneGun.GetComponent<geneGun>().gel != null)
            {
                tempnet.GetComponent<net>().gelgene = geneGun.GetComponent<geneGun>().gel.Genes;
                tempnet.GetComponent<net>().ability = elementManager.elementRect(geneGun.GetComponent<geneGun>().gel.Genes);
                Debug.Log(tempnet.GetComponent<net>().ability);

                tempnet.GetComponent<net>().netColor = geneGun.GetComponent<geneGun>().gelColor;
                geneGun.GetComponent<geneGun>().gel.itemNumber--;
                if (geneGun.GetComponent<geneGun>().gel.itemNumber == 0) geneGun.GetComponent<geneGun>().gel = null;

                if(tempnet.GetComponent<net>().ability==mudAndConcreate.typeEnum.concreteToMud.ToString())
                {
                    Debug.Log("change");
                    GetComponent<mudAndConcreate>().myTypeEnum = mudAndConcreate.typeEnum.concreteToMud;
                    changeTile(mousePositionInWorld);
                }
                if (tempnet.GetComponent<net>().ability == mudAndConcreate.typeEnum.mudToConcrete.ToString())
                {
                    GetComponent<mudAndConcreate>().myTypeEnum = mudAndConcreate.typeEnum.mudToConcrete;
                    changeTile(mousePositionInWorld);
                }
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
