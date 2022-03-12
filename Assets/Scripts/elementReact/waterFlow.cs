using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text.RegularExpressions;
public class waterFlow : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int leftBottom;
    public Vector3Int rightTop;
    public float coolTime;
    bool[,] vis;
    float nowtime;
    int xWidth,yWidth;

    static waterFlow instance;

    static public waterFlow getInstance()
    {
        if (instance == null) instance = new waterFlow();
        return instance;
    }
    private void Awake()
    {

        vis = new bool[rightTop.x - leftBottom.x+10, rightTop.y - leftBottom.y+10];
        xWidth = rightTop.x - leftBottom.x;
        yWidth = rightTop.y - leftBottom.y;
    }
    int getInt(string a)
    {
        int x = 0;
        for (int i = 0; i < a.Length; i++)
            if ('0' <= a[i] && a[i] <= '9') x = x * 10 + a[i] - 48;
        return x;
    }
    void dfs(int xpos,int ypos,bool flag)
    {
        if (xpos < leftBottom.x || xpos > rightTop.x) return;
        if (ypos < leftBottom.y || ypos > rightTop.y) return;
        if (vis[xpos - leftBottom.x, ypos - leftBottom.y] == true) return;
        TileBase tempTile = tilemap.GetTile(new Vector3Int(xpos, ypos, 0));
        if (tempTile == null) return;
        int id = getInt(tempTile.name);
        if(flag==true)
        {
            //Debug.Log(xpos.ToString() + "," + ypos.ToString());
            //Debug.Log(Regex.IsMatch(tempTile.name, "concrete").ToString() + id.ToString());
            if(Regex.IsMatch(tempTile.name, "concrete")&&id<4)
            GetComponent<mudAndConcreate>().mySetWaterTile(xpos, ypos,id);
            if(Regex.IsMatch(tempTile.name, "dry"))
            GetComponent<mudAndConcreate>().mySetWetMudTile(xpos, ypos, id);
            tempTile = tilemap.GetTile(new Vector3Int(xpos, ypos, 0));
        }
        if (flag == false && Regex.IsMatch(tempTile.name, "water") == false) return;
        vis[xpos - leftBottom.x, ypos - leftBottom.y] = true;
        //Debug.Log(Regex.IsMatch(tempTile.name, "water").ToString()+id.ToString());
        if (Regex.IsMatch(tempTile.name, "water"))
        {
            if (id == 3 || id == 1|| id==0) 
            {
                dfs(xpos - 1, ypos, true);
                //Debug.Log(xpos.ToString() + "," + ypos.ToString());
                dfs(xpos + 1, ypos, true);
            }
            if (id == 2 || id == 3|| id==0)
            {
                dfs(xpos, ypos - 1, true);
                dfs(xpos, ypos + 1, true);
            }
        }
    }
    IEnumerator waitDfs(float t)
    {
        yield return new WaitForSeconds(t);

    }
    void refresh()
    {
        for (int i = 0; i < xWidth; i++)
            for (int j = 0; j < yWidth; j++)
                vis[i, j] = false;
        for(int xpos=leftBottom.x;xpos<=rightTop.x;xpos++)
            for(int ypos = leftBottom.y; ypos <= rightTop.y; ypos++)
            if(vis[xpos-leftBottom.x,ypos-leftBottom.y]==false)
            {
                    //Debug.Log(xpos.ToString() + "," + ypos.ToString());
                    //StartCoroutine(waitDfs(0.5f));
                   dfs(xpos, ypos,false);
            }
    }
    private void Start()
    {
        refresh();
    }
    private void Update()
    {

    }
}
