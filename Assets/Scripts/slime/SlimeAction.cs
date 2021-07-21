using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAction : MonoBehaviour
{
    public float actionCoolTime;
    public float actionInterval;
    public float avoidDistance;
    public float ariDistance;
    public float transSpeed;
    public float hanTime;
    GameObject player;
    float nowTime;
    public float foodTime;
    List<GameObject> disGrass;

    private Vector3 Ranvec;
    private int flag = -1;
    float pow2(float x)
    {
        return x * x;
    }

    float distance(Vector3 a,Vector3 b)
    {
        return Mathf.Sqrt(pow2(a.x - b.x) + pow2(a.y - b.y) + pow2(a.z - b.z));
    }

    private void Start()
    {
        gameObject.GetComponent<slime>().active = true;
        gameObject.GetComponent<slime>().isHau = false;
        gameObject.GetComponent<slime>().isHan = false;
        player = GameObject.FindWithTag("player");
    }

    void avoid()
    {
        if ((transform.position - player.transform.position).x <= 0) GetComponent<slime>().setDirect(1);
        else GetComponent<slime>().setDirect(-1);
        float step = transSpeed * Time.deltaTime;
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, transform.position+transform.position-player.transform.position, step);
    }


    void arrive(GameObject gra)
    {
        if (gra != null)
        {
            if((gra.transform.position-transform.position).x<=0) GetComponent<slime>().setDirect(1);
            else GetComponent<slime>().setDirect(-1);
            float step = transSpeed * Time.deltaTime;
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, gra.transform.position, step);
        }
    }

    IEnumerator actionCool()
    {
        yield return new WaitForSeconds(actionCoolTime);
        nowTime = 0;
        flag = -1;
    }

    private int QuickSort_Once(int begin, int end,Vector3 nowPos)
    {
        int pivot =begin;
        int i = begin;
        int j = end;
        while (i < j)
        {
            while (distance(disGrass[j].transform.position, nowPos) >= distance(disGrass[pivot].transform.position, nowPos) && i < j) j--;
            disGrass[i] = disGrass[j];

            while (distance(disGrass[i].transform.position, nowPos) <= distance(disGrass[pivot].transform.position, nowPos) && i < j) i++;
            disGrass[j] =disGrass[i];  
        }
        disGrass[i] = disGrass[pivot];
        return i;
    }

    private void QuickSort(int begin, int end,Vector3 nowPos)
    {
        if (begin >= end) return;  
        int pivotIndex = QuickSort_Once(begin, end,nowPos); 

        QuickSort( begin, pivotIndex - 1,nowPos);  
        QuickSort( pivotIndex + 1, end,nowPos);   
    }

    IEnumerator runAway()
    {
        yield return new WaitForSeconds(5);
        GetComponent<slime>().active = true;
    }

    void action()
    {
        if (gameObject.GetComponent<slime>().active == false)
        {
            StartCoroutine(runAway());
            return;
        }
        if (foodTime >= hanTime) gameObject.GetComponent<slime>().isHan = true;

        if (gameObject.GetComponent<slime>().isHau && distance(transform.position, player.transform.position) < avoidDistance)
        {
            avoid();
        }
        else
        {
            gameObject.GetComponent<slime>().isHau = false;
            if (gameObject.GetComponent<slime>().isHan ==true&& GrassPool.instance.myGrassActive.Count >= 1)
            {
                disGrass = new List<GameObject>();
                    for (int i = 0; i < GrassPool.instance.myGrassActive.Count; i++)
                    {
                        disGrass.Add(GrassPool.instance.myGrassActive[i]);
                    }
                    GrassPool.refresh();
                    QuickSort(0, disGrass.Count - 1, transform.position);
                    arrive(disGrass[0]);
            }
            else
            {
                if (flag == -1)
                {
                    flag = 1;
                    Ranvec = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0).normalized;

                }
                else
                {
                    float step = transSpeed * Time.deltaTime;
                    if (( Ranvec*100 - transform.position ).x <= 0) GetComponent<slime>().setDirect(1);
                    else GetComponent<slime>().setDirect(-1);
                    gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, Ranvec*100, step);

                }
            }
        }
    }

    private void Update()
    {
        nowTime += Time.deltaTime;
        foodTime += Time.deltaTime;
        if (!GetComponent<slime>().isHau&&nowTime > actionInterval)
        {
            StartCoroutine(actionCool());
        }
        else action();
    }
}
