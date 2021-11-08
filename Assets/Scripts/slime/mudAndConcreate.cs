using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text.RegularExpressions;
public class mudAndConcreate : MonoBehaviour
{
    public bool isplayer;
    public Grid grid;
    public Tilemap tilemap;
    public Transform cellTransform;

    public enum typeEnum
    {
        neither,
        mudToConcrete,
        concreteToMud,
    }

    public typeEnum myTypeEnum = typeEnum.neither;

    int getInt(string a)
    {
        int x = 0;
        for (int i = 0; i < a.Length; i++)
            if ('0' <= a[i] && a[i] <= '9') x = x * 10 + a[i] - 48;
        return x;
    }

    public void changeTile1()
    {
        Vector3Int cellPos = grid.WorldToCell(cellTransform.position);
        TileBase tempTile = tilemap.GetTile(cellPos);
        int temp = getInt(tempTile.name);
        if (myTypeEnum == typeEnum.mudToConcrete)
        {
            Debug.Log(tempTile.name);
            Debug.Log(Regex.IsMatch("concrete1", "concrete"));
            if (Regex.IsMatch(tempTile.name, "concrete"))
            {
                tilemap.SetTile(cellPos, elementTileManager.instance.concreteTiles[
                    temp == elementTileManager.instance.concreteTiles.Count-1 ? 0 : temp + 1]);
                Debug.Log("cc");
            }
            else
            if (Regex.IsMatch(tempTile.name, "mud"))
                tilemap.SetTile(cellPos, elementTileManager.instance.concreteTiles[temp]);
        }
        else if (myTypeEnum == typeEnum.concreteToMud)
        {
            if (Regex.IsMatch(tempTile.name, "mud"))
                tilemap.SetTile(cellPos, elementTileManager.instance.mudTiles[
                    temp == elementTileManager.instance.concreteTiles.Count-1 ? 0 : temp + 1]);
            else
            if (Regex.IsMatch(tempTile.name, "concrete"))
                tilemap.SetTile(cellPos, elementTileManager.instance.mudTiles[temp]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int cellPos = grid.WorldToCell(cellTransform.position);
        TileBase tempTile = tilemap.GetTile(cellPos);
        if (tempTile!=null&&isplayer==false)
        {
            int temp = getInt(tempTile.name);
            if (myTypeEnum == typeEnum.mudToConcrete && Regex.IsMatch(tempTile.name,"mudTile"))
                tilemap.SetTile(cellPos, elementTileManager.instance.concreteTiles[temp]);
            else if (myTypeEnum == typeEnum.concreteToMud && Regex.IsMatch(tempTile.name, "concreteTile"))
                tilemap.SetTile(cellPos, elementTileManager.instance.mudTiles[temp]);
        }
    }
}
