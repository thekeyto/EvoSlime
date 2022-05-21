using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainMenuUI : MonoBehaviour
{
    int[] gene = new int[100];
    Color elementColor;
    bool if_bag;
    public List<GameObject> elements;
    public bool if_UIOpen;
    public GameObject elementSlots;
    public GameObject elementWayUI;

    public GameObject selectUI;
    public GameObject researchUI;
    public GameObject blogUI;
    public GameObject exploreUI;
    public GameObject paperUI;
    public Item slimeGene;
    public static mainMenuUI instance;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
        if_UIOpen = false;
    }

    private void Start()
    {
        researchUI.SetActive(false);
        paperUI.SetActive(false);
        blogUI.SetActive(false);
        exploreUI.SetActive(false);
        selectUI.SetActive(true);
        if_bag = false;
        if_UIOpen = false;
        
        elements = new List<GameObject>();
        for (int i = 0; i < elementSlots.transform.childCount; i++)
        {
            elements.Add(elementSlots.transform.GetChild(i).gameObject);
            elements[i].transform.position = new Vector3(0, 0, 0);
        }
    }

    void refresh()
    {

    }

    public void researchCanvas()
    {
        if(researchUI.active==false)
        {
            selectUI.SetActive(false);
            researchUI.SetActive(true);
        }
        else
        {
            selectUI.SetActive(true);
            researchUI.SetActive(false);
        }
    }

    public void blogCanvas()
    {
        if (blogUI.active == false)
        {
            selectUI.SetActive(false);
            blogUI.SetActive(true);
        }
        else
        {
            selectUI.SetActive(true);
            blogUI.SetActive(false);
        }
    }

    public void paperCanvas()
    {
        if (paperUI.active == false)
        {
            selectUI.SetActive(false);
            paperUI.SetActive(true);
        }
        else
        {
            selectUI.SetActive(true);
            paperUI.SetActive(false);
        }
    }

    public void exploreCanvas()
    {
        if (exploreUI.active == false)
        {
            selectUI.SetActive(false);
            exploreUI.SetActive(true);
        }
        else
        {
            selectUI.SetActive(true);
            exploreUI.SetActive(false);
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

    static int min(int a, int b)
    {
        return a < b ? a : b;
    }

    public void elementWay()
    {
        if ((slimeGene.myClassic == Item.classic.gel || slimeGene.myClassic == Item.classic.slime))
        {
            elementWayUI.SetActive(true);
            for (int i = 10; i < 35; i++)
            {
                Debug.Log(elementColor);
                Debug.Log(slimeGene.Genes[i]);
                if (slimeGene.Genes[i] == '1') elements[i - 10].GetComponent<Image>().color = elementColor;
                else elements[i - 10].GetComponent<Image>().color = Color.white;
            }
        }

    }

    public void elementWayUIExit()
    {
        elementWayUI.SetActive(false);
    }
}
