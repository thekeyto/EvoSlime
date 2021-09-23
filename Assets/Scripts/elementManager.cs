using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elementManager : MonoBehaviour
{
    static string soilToMud = "1010101010000000101010101";
    static string mudToSoil = "0101010101111111010101010";
    public static string elementRect(string gene)
    {
        int[] tempgenes = new int[50];
        for (int i = 0; i < gene.Length; i++)
            tempgenes[i] = gene[i] == '0' ? 0 : 1;
        int element = slime.geneBiToInt(2, 5, tempgenes);
        if(element==0)
        {
            int flag = 0;
            for (int i = 0; i < 25; i++) if (gene[i + 10] != soilToMud[i]) flag = 1;
            if (flag == 0) return "soilToMud";
            flag = 0;
            for (int i = 0; i < 25; i++) if (gene[i + 10] != mudToSoil[i]) flag = 1;
            if (flag == 0) return "mudToSoil";
        }
        return "null";
    }
}