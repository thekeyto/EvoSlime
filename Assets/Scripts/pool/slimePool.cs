using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimePool : MonoBehaviour
{
    public GameObject slime;
    public static slimePool instance;
    public List<GameObject> mySlimeActive;
    public List<GameObject> mySlimeNotActive;

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

    private void Start()
    {
        instance.mySlimeActive = new List<GameObject>(GameObject.FindGameObjectsWithTag("slime"));
    }

    public static void refresh()
    {
        instance.mySlimeActive = new List<GameObject>(GameObject.FindGameObjectsWithTag("slime"));
    }

    public GameObject insSlime(Vector3 position)
    {
        if (instance.mySlimeNotActive.Count != 0)
        {
            instance.mySlimeNotActive[0].SetActive(true);
            instance.mySlimeActive.Add(instance.mySlimeNotActive[0]);
            instance.mySlimeNotActive.RemoveAt(0);
            return instance.mySlimeActive[instance.mySlimeActive.Count - 1];
        }
        else
        {
            var temp = Instantiate(slime) as GameObject;
            temp.transform.position = position;
            instance.mySlimeActive.Add(temp);
            return temp;
        }
    }

    public GameObject insSlime(Vector3 position, Transform parent)
    {
        if (instance.mySlimeNotActive.Count != 0)
        {
            instance.mySlimeNotActive[0].SetActive(true);
            instance.mySlimeActive.Add(instance.mySlimeNotActive[0]);
            instance.mySlimeNotActive.RemoveAt(0);
            return instance.mySlimeActive[instance.mySlimeActive.Count - 1];
        }
        else
        {
            var temp = Instantiate(slime) as GameObject;
            temp.transform.position = position;
            temp.transform.SetParent(parent);
            instance.mySlimeActive.Add(temp);
            return temp;
        }
    }

    public void desSlime(GameObject tarslime)
    {
        instance.mySlimeActive.Remove(tarslime);
        instance.mySlimeNotActive.Add(tarslime);
        tarslime.SetActive(false);
    }
}