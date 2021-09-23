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
        geneDecryp();
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

    public static int geneBiToInt(int start,int length,int[] tempgene)
    {
        int temp = 0;
        for(int i=start;i<start+length;i++)
        {
            temp = temp * 2 + tempgene[i];
        }
        return temp;
    }

    public static Color setColor(int color)
    {
        Color tempColor=new Color(0,0,0);
        if (color == 0) return Color.red;
        else if (color == 1) return Color.green;
        else if (color == 2) return Color.blue;
        return tempColor;
    }

    void geneDecryp()
    {
        figure = (gene[0] * 2 + gene[1] + 1) * 0.5f;
        color = geneBiToInt(2,4,gene);
        element = geneBiToInt(6, 4,gene);
        Color slimeColor=new Color(0,0,0);

        gameObject.transform.localScale *= figure;

        slimeColor = setColor(color);

        gameObject.GetComponent<SpriteRenderer>().color = slimeColor;
    }
}