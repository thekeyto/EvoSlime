using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slime : MonoBehaviour
{
    public bool isHau;
    public bool isHan;
    public bool active;
    public Animator slimeAni;
    public Item slimeProperty;

    int[] gene=new int[50];
    float figure;
    int color;
    Sprite slimeSprite;
    int element;
    private void Awake()
    {
        slimeAni = GetComponent<Animator>();
    }

    private void Start()
    {
        if (slimeProperty == null) return;
        for(int i=0;i<slimeProperty.Genes.Length;i++)
        {
            if (slimeProperty.Genes[i] == '0')
                gene[i] = 0;
            else gene[i] = 1;
        }
    }
    public void setDirect(int direct)
    {
        Vector3 temp = transform.localScale;
        if (temp.x < 0 && direct > 0) temp.x *= -1;
        if (temp.x > 0 && direct < 0) temp.x *= -1;
        transform.localScale = temp;
    }

    int geneBiToInt(int start,int length)
    {
        int temp = 0;
        for(int i=start;i<start+length;i++)
        {
            temp = temp * 2 + gene[i];
        }
        return temp;
    }

    void geneDecryp()
    {
        figure = (gene[0] * 2 + gene[1] + 1) * 0.5f;
        color = geneBiToInt(2,4);
        element = geneBiToInt(6, 4);

    }
}