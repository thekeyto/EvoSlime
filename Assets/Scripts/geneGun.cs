using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class geneGun : MonoBehaviour
{
    public Item gel;
    public Image gelImage;
    public Color gelColor;
    int[] gene = new int[50];
    public void getItem(Item tempItem)
    {
        gel = tempItem;
        for (int i = 0; i < gel.Genes.Length; i++)
        {
            if (gel.Genes[i] == '0')
                gene[i] = 0;
            else gene[i] = 1;
        }
        int color=slime.geneBiToInt(2, 4, gene);
        Debug.Log(color);
        gelColor = slime.setColor(color);
        Debug.Log(gelColor);
    }

    private void Update()
    {
        if (gel != null)
            gelImage.sprite = gel.itemImage;
        else
            gelImage.sprite = null;
    }
}
