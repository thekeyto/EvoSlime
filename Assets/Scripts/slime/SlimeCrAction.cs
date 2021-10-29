using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCrAction : MonoBehaviour
{
    public bool active;
    public float actionCoolTime;
    public float actionInterval;
    public float avoidDistance;
    public float ariDistance;
    public float transSpeed;
    public float CrTime;
    public float waitCrTime;
    GameObject player;
    float nowTime;
    List<GameObject> disGrass;
    public bool isHau;


    private Vector3 Ranvec;
    private int flag = -1;
    float pow2(float x)
    {
        return x * x;
    }

    float distance(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt(pow2(a.x - b.x) + pow2(a.y - b.y) + pow2(a.z - b.z));
    }

    private void Start()
    {
        flag = -1;
        active = true;
        isHau = false;
        player = GameObject.FindWithTag("player");
    }

    void avoid()
    {
        if ((transform.position - player.transform.position).x <= 0) GetComponent<slime>().setDirect(1);
        else GetComponent<slime>().setDirect(-1);
        float step = transSpeed * Time.deltaTime;
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, transform.position + transform.position - player.transform.position, step);
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "grass")
        {
            isHan = false;
            foodTime = 0;
        }
    }*/

    IEnumerator actionCool()
    {
        yield return new WaitForSeconds(actionCoolTime);
        nowTime = 0;
        flag = -1;
    }

    IEnumerator runAway()
    {
        yield return new WaitForSeconds(5);
        GetComponent<slime>().active = true;
    }

    void action()
    {
        if (GetComponent<slime>().active == false)
        {
            StartCoroutine(runAway());
            return;
        }

        if (sun.ifDay==true)
        {
            if (waitCrTime >= CrTime)
            {
                waitCrTime = 0;
                Ranvec = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
                GrassPool.instance.insGrass(new Vector3(Ranvec.x, Ranvec.y, 0),GrassPool.instance.transform);
            }
        }


        if (isHau && distance(transform.position, player.transform.position) < avoidDistance)
        {
            avoid();
        }
        else
        {
            isHau = false;
            {
                if (flag == -1)
                {
                    flag = 1;
                    Ranvec = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0).normalized;
                }
                else
                {
                    if ((Ranvec*100 - transform.position).x <= 0) GetComponent<slime>().setDirect(1);
                    else GetComponent<slime>().setDirect(-1);
                    float step = transSpeed * Time.deltaTime;
                    gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, Ranvec * 100, step);

                }
            }
        }
    }

    private void Update()
    {
        nowTime += Time.deltaTime;
        waitCrTime += Time.deltaTime;
        if (!GetComponent<slime>().isHau&&nowTime > actionInterval)
        {
            StartCoroutine(actionCool());
        }
        else action();
    }
}
