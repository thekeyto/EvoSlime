using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class csvController : MonoBehaviour
{
    static csvController csv;
    public List<string[]> arrayData;

    private csvController()   //单例，构造方法为私有
    {
        arrayData = new List<string[]>();
    }

    public static csvController GetInstance()   //单例方法获取对象
    {
        if (csv == null)
        {
            csv = new csvController();
        }
        return csv;
    }

    public void loadFile(string path, string fileName)
    {
        arrayData.Clear();
        StreamReader sr = null;
        
        try
        {
            string file_url = path + "//" + fileName;    //根据路径打开文件
            sr = File.OpenText(file_url);
            Debug.Log("File Find in " + file_url);
        }
        catch
        {
            Debug.Log(path+"  "+fileName);
            Debug.Log("File cannot find ! ");
            return;
        }

        string line;
        while ((line = sr.ReadLine()) != null)   //按行读取
        {
            arrayData.Add(line.Split(','));   //每行逗号分隔,split()方法返回 string[]
        }
        sr.Close();
        sr.Dispose();/*
        for(int i=0;i<=arrayData.Count;i++)
        {
            for (int j = 0; j < 4; j++)
                Debug.Log(i.ToString()+" "+j.ToString()+" "+arrayData[i][j]);
        }*/
    }

    public int getListSize()
    {
        return arrayData.Count;
    }

    public string getString(int row, int col)
    {
        //Debug.Log(row.ToString() + "string " + col.ToString());
        return arrayData[row][col];
    }
    public int getInt(int row, int col)
    {
        //Debug.Log(row.ToString()+" int "+ col.ToString());
        return int.Parse(arrayData[row][col]);
    }

    public float getFloat(int row,int col)
    {
        //Debug.Log(row.ToString() + " float " + col.ToString());
        return float.Parse(arrayData[row][col]);
    }
}
