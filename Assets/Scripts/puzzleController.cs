using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Zhuzi1;
    public bool isSetup;
    public GameObject lazer;
    public GameObject checkPoint;

    // Update is called once per frame
    void Update()
    {
        CheckPos();
        TargetLazer();
    }
    private void CheckPos()
    {
        if ((Zhuzi1.transform.position - checkPoint.transform.position).magnitude <0.1)
        {
            isSetup = true;
        }
        else
        {
            isSetup = false;
        }
    }
    private void TargetLazer()
    {
        if (isSetup)
        {
            lazer.transform.localScale = new Vector3(1, 0.1f, 1);
        }
        else
        {
            lazer.transform.localScale = new Vector3(0, 0.1f, 1);
        }
    }
}
