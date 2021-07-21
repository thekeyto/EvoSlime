using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassPool : MonoBehaviour
{
    public GameObject grass;
    public static GrassPool instance;
    public List<GameObject> myGrassActive;
    public List<GameObject> myGrassNotActive;

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

    private void Start()
    {
        instance.myGrassActive = new List<GameObject>( GameObject.FindGameObjectsWithTag("grass"));
    }

    public static void refresh()
    {
        instance.myGrassActive = new List<GameObject>(GameObject.FindGameObjectsWithTag("grass"));
    }

    public GameObject insGrass(Vector3 position)
    {
        if(instance.myGrassNotActive.Count!=0)
        {
            instance.myGrassNotActive[0].SetActive(true);
            instance.myGrassActive.Add(instance.myGrassNotActive[0]);
            instance.myGrassNotActive.RemoveAt(0);
            return instance.myGrassActive[instance.myGrassActive.Count-1];
        }
        else
        {
            var temp= Instantiate(grass) as GameObject;
            temp.transform.position = position;
            instance.myGrassActive.Add(temp);
            return temp;
        }
    }
    
    public GameObject insGrass(Vector3 position,Transform parent)
    {
        if (instance.myGrassNotActive.Count != 0)
        {
            instance.myGrassNotActive[0].SetActive(true);
            instance.myGrassActive.Add(instance.myGrassNotActive[0]);
            instance.myGrassNotActive.RemoveAt(0);
            return instance.myGrassActive[instance.myGrassActive.Count - 1];
        }
        else
        {
            var temp = Instantiate(grass) as GameObject;
            temp.transform.position = position;
            temp.transform.SetParent(parent);
            instance.myGrassActive.Add(temp);
            return temp;
        }
    }

    public void desGrass(GameObject tarGrass)
    {
        instance.myGrassActive.Remove(tarGrass);
        instance.myGrassNotActive.Add(tarGrass);
        tarGrass.SetActive(false);
    }
}
