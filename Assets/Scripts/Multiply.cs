using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiply : MonoBehaviour
{
    Item parent1;
    Item parent2;
    public Inventory playerbag;

    public Workbench workBenchL;
    public Workbench workBenchR;



    public int xLength, yLength;

    int[,] GamateCombine(int[,] gamate1, int[,] gamate2)
    {
        int count1 = 0, count2 = 0;
        int count = 0;
        int[,] child = new int[yLength, xLength];
        for (int j = 0; j < yLength; j++)
        {
            if (gamate1[j, 0] != 0 || gamate1[j, 1] != 0)
                count1++;
            if (gamate2[j, 0] != 0 || gamate2[j, 1] != 0)
                count2++;
        }
        for (int i = 0; i <= count1; i++)
        {
            for (int j = 0; j <= count2; j++)
            {
                if ((gamate2[j, 0] == gamate1[i, 0] && gamate2[j, 1] == gamate1[i, 1]) && (gamate2[j, 0] != 0 || gamate2[j, 1] != 0))
                {
                    child[count, 0] = gamate1[i, 0];
                    child[count, 1] = gamate1[i, 1];
                    gamate1[i, 0] = 0;
                    gamate1[i, 1] = 0;
                    gamate2[j, 0] = 0;
                    gamate2[j, 1] = 0;
                    for (int k = 2; k < 10; k += 2)
                    {
                        child[count, k] = gamate1[i, (k + 2) / 2];
                        child[count, k + 1] = gamate2[i, (k + 2) / 2];
                    }
                    count++;
                }
            }
        }
        for (int i = 0; i <= count1; i++)
        {
            if (gamate1[i, 0] != 0 && gamate1[i, 1] != 0)
            {
                child[count, 0] = gamate1[i, 0];
                child[count, 1] = gamate1[i, 1];
                for (int k = 2; k < 10; k += 2)
                {
                    child[count, k] = gamate1[i, (k + 2) / 2];
                }
                count++;
            }
        }
        for (int j = 0; j <= count2; j++)
        {
            if (gamate2[j, 0] != 0 && gamate2[j, 1] != 0)
            {
                child[count, 0] = gamate2[j, 0];
                child[count, 1] = gamate2[j, 1];
                for (int k = 2; k < 10; k += 2)
                {
                    child[count, k] = gamate2[j, (k + 2) / 2];
                }
                count++;
            }
        }
        return child;
    }

    int[,] GameteProduce(int[,] genes)//获得基因，生成配子
    {
        int[,] gamate = new int[yLength, xLength / 2 + 1];
        for (int j = 0; j < genes.GetLength(1); j++)
        {
            gamate[j, 0] = genes[j, 0];
            gamate[j, 1] = genes[j, 1];
            for (int i = 2; i < 10; i += 2)
            {
                if (genes[j, i] == genes[j, i + 1])
                {
                    gamate[j, (i + 2) / 2] = genes[j, i];//i/2是为了防止数组越界
                }
                else
                {
                    gamate[j, (i + 2) / 2] = Random.Range(0, 99999) % 2;//取0，1 后续可以改成unity的随机函数
                }
            }
            if (genes[j, 0] == 0 && genes[j, 1] == 0)
                break;
        }
        return gamate;
    }

    string BuildGenesString(int[,] GenesArray)
    {
        string parent = string.Empty;
        for (int j = 0; j < 20; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                parent += GenesArray[j, i].ToString();
            }
            if (GenesArray[j + 1, 0] == 0 && GenesArray[j + 1, 1] == 0)
            {
                parent += "\n";
                break;
            }
        }
        return parent;
    }

    int[,] BuildGenesArray(string GenesCode)
    {
        int[,] parent = new int[yLength, xLength];
        for (int temp = 0; temp < GenesCode.Length; temp++)
        {
            parent[temp / 10, temp % 10] = (int)(GenesCode[temp]) - 48;//Convert后续可以改成unity的函数
        }
        return parent;
    }
    string Mutiply(string ParentGenes1, string ParentGenes2)
    {
        int[,] parent1 = BuildGenesArray(ParentGenes1);
        int[,] parent2 = BuildGenesArray(ParentGenes2);
        int[,] gamate1 = GameteProduce(parent1);
        int[,] gamate2 = GameteProduce(parent2);
        int[,] childGenesArray = GamateCombine(gamate1, gamate2);
        string childGenesString = BuildGenesString(childGenesArray);
        return childGenesString;
    }

    void resetParent()
    {
        workBenchL.parent = null;
        workBenchR.parent = null;
    }

    public void Generate()
    {
        if (workBenchL.parent != null && workBenchR.parent != null)
        {
            parent1 = workBenchL.parent;
            parent2 = workBenchR.parent;
            string tempchild = Mutiply(parent1.Genes, parent2.Genes);
            bool flag = false;
            for (int i = 0; i < playerbag.itemList.Count; i++)
                if(playerbag.itemList[i]!=null)
                if (playerbag.itemList[i].Genes == tempchild)
                {
                    playerbag.itemList[i].itemNumber++; 
                    flag = true;
                    break;
                }
            Debug.Log(tempchild);
            if (flag == false)
            {
                Item child = ScriptableObject.CreateInstance<Item>();
                child.itemName ="slime"+ (playerbag.itemList.Count + 1).ToString();
                child.Genes = tempchild;
                child.itemNumber = 1;
                child.itemImage = parent1.itemImage;
                if(child!=null)
                playerbag.itemList.Add(child);
            }

            resetParent();
            InventoryManager.RefreshItem();
        }
    }
}
