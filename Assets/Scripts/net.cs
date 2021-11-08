using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class net : MonoBehaviour
{
    bool if_catch;
    float lifetime = 3;
    float nowtime = 0;
    public Vector3 destination;
    public float lit1;
    public float lit2;
    public Color netColor;
    public SpriteRenderer netSprite;
    public string gelgene;
    public string ability;
    private void Awake()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        if_catch = false;
        netSprite=GetComponent<SpriteRenderer>();
        netColor = new Color(255, 255, 255, 255);
    }
    private void Start()
    {
       netSprite.color = netColor;
    }
    private void Update()
    {

        netSprite.color = netColor;
        if (distance(destination, transform.position) < 0.5)
        {
            GetComponent<CapsuleCollider2D>().enabled = true;
            this.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            nowtime += Time.deltaTime;
            if(nowtime>1&&if_catch == false) Destroy(gameObject);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "slime")
        {
            if (distance(collision.transform.position, transform.position) <= lit1)
            {
                if_catch = true;
                collision.GetComponent<slime>().active = false;
                Destroy(gameObject, 5.0f);
            }
            else if (distance(collision.transform.position, transform.position) <= lit2)
            {
                collision.GetComponent<slime>().isHau = true;
            }
        }
    }
}
