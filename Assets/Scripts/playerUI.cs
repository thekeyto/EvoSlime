using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerUI : MonoBehaviour
{
    int[] gene = new int[100];
    Color elementColor;
    bool if_bag;
    bool if_workBench;
    public List<GameObject> elements;
    public bool if_UIOpen;
    public GameObject elementSlots;
    public GameObject playerbag;
    public GameObject workBench;
    public GameObject geneGun;
    public GameObject elementWayUI;
    public GameObject handbook;
    public Item slimeGene;
    public static playerUI instance;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
        if_UIOpen = false;
    }

    private void Start()
    {
        workBench.SetActive(false);
        geneGun.SetActive(false);
        playerbag.SetActive(false);
        elementWayUI.SetActive(false);
        if_bag = false;
        if_workBench = false;
        if_UIOpen = false;
        elements = new List<GameObject>();
        for (int i = 0; i < elementSlots.transform.childCount; i++)
            elements.Add(elementSlots.transform.GetChild(i).gameObject);
    }

    void refresh()
    {

    }

    public void handbookUI()
    {
        if (handbook.active == false)
        {
            if_UIOpen = true;
            handbook.active = true;
        }
        else
        {
            if_UIOpen = false;
            handbook.active = false;
        }
    }

    public void bagUI()
    {

        if (playerbag.active == false)
        {
            if_UIOpen = true;
            geneGun.SetActive(true);
            playerbag.SetActive(true);
        }
        else
        {
            if_UIOpen = false;
            workBench.SetActive(false);
            geneGun.SetActive(false);
            playerbag.SetActive(false);
        }
    }

    public void workBenchUI()
    {
        if (playerbag.active == false) playerbag.SetActive(true);
        if (workBench.active == false)
        {
            geneGun.SetActive(false);
            workBench.SetActive(true);
        }
        else
        {
            geneGun.SetActive(true);
            workBench.SetActive(false);
        }
    }

    public void set_elementGene(Item gelgene)
    {
        slimeGene = gelgene;
        Debug.Log(gelgene.Genes);
        for (int i = 0; i < gelgene.Genes.Length; i++)
        {
            if (gelgene.Genes[i] == '0')
                gene[i] = 0;
            else gene[i] = 1;
        }
        int temp = slime.geneBiToInt(2, 4, gene);
        elementColor = slime.setColor(temp);
    }

    static int min(int a,int b)
    {
        return a < b ? a : b;
    }

    public void elementWay()
    {
        if ((slimeGene.classic == "gel" || slimeGene.classic == "slime") && elementWayUI.active == false)
        {
            elementWayUI.SetActive(true);
            for (int i = 10; i < 35; i++)
            {
                Debug.Log(elementColor);
                Debug.Log(slimeGene.Genes[i]);
                if (slimeGene.Genes[i] == '1') elements[i-10].GetComponent<Image>().color = elementColor;
                else elements[i-10].GetComponent<Image>().color = Color.white;
            }
           }
            
    }

    public void elementWayUIExit()
    {
        elementWayUI.SetActive(false);
    }
}
