using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facing : MonoBehaviour
{
    List<Transform> things = new List<Transform>();
    Transform[] childs;
    void Start()
    {
        childs = new Transform[transform.childCount];
        for(int i=0;i<transform.childCount;i++)
        {
            childs[i] = transform.GetChild(i);
        }
        for (int i = 0; i < childs.Length; i++)
            if (childs[i].transform.childCount != 0 && childs[i].tag != "player")
                for (int j = 0; j < childs[i].transform.childCount; j++)
                {
                    things.Add(childs[i].transform.GetChild(j));
                }
            else things.Add(childs[i].transform);
        for (int i = 0; i < things.Count; i++)
        {
            things[i].localRotation = Camera.main.transform.rotation;
            things[i].localPosition = new Vector3(things[i].localPosition.x,things[i].localPosition.y,0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        childs = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i);
        }
        for (int i = 0; i < childs.Length; i++)
            if (childs[i].transform.childCount != 0 && childs[i].tag != "player")
                for (int j = 0; j < childs[i].transform.childCount; j++)
                {
                    things.Add(childs[i].transform.GetChild(j));
                }
            else things.Add(childs[i].transform);
        for (int i = 0; i < things.Count; i++)
        {
            things[i].localRotation = Camera.main.transform.rotation;
        }
    }
}
