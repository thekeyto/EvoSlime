using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sun : MonoBehaviour
{
    static public bool ifDay;
    public float dayTime;
    public float nowMinTime;
    public float nowSecTime;
    public float timeSpeed;

    // Start is called before the first frame update
    void Start()
    {
    }

    void checkDay()
    {
        nowSecTime += Time.deltaTime*timeSpeed;
        if(nowSecTime>=60)
        {
            nowSecTime = 0;
            nowMinTime++;
        }
        if (6 <= nowMinTime && nowMinTime <= 18)
            ifDay = true;
        else ifDay = false;
    }

    void sunRotate()
    {
        Vector3 tempvec=transform.rotation.eulerAngles;
        float nowTime = nowMinTime + nowSecTime / 60;
        tempvec.y = tempvec.z = 0;
        if (0.25*dayTime <= nowTime && nowTime <= 0.75*dayTime)
            tempvec.x = Mathf.Lerp(-11, 85, (nowTime - 0.25f*dayTime) / (0.5f*dayTime));
        if (0.75*dayTime <= nowTime && nowTime <= dayTime)
            tempvec.x = Mathf.Lerp(85, 185, (nowTime - 0.75f*dayTime) / (0.25f*dayTime));
        if (0 <= nowTime && nowTime < 0.25*dayTime)
            tempvec.x = Mathf.Lerp(-75, -11, nowTime / (0.25f*dayTime));
        if(nowTime>=dayTime)
        {
            nowMinTime = 0;
            tempvec.x = -75;
        }
        transform.localRotation =Quaternion.Euler(tempvec);
    }

    // Update is called once per frame
    void Update()
    {
        checkDay();
        sunRotate();
        /*
         0 -75 
         6 -45
         12 0
         18 85
         24 185
         */
    }
}
