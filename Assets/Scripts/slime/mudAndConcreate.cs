using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class mudAndConcreate : MonoBehaviour
{
    public Grid grid;
    public Tilemap tilemap;
    public List<TileBase> tiles = new List<TileBase>();
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

    // Update is called once per frame
    void Update()
    {
        Vector3Int cellPos = grid.WorldToCell(cellTransform.position);
        TileBase tempTile = tilemap.GetTile(cellPos);
        if (tempTile!=null)
        {
            int temp = getInt(tempTile.name);
            if (myTypeEnum == typeEnum.mudToConcrete && temp >= 1 && temp <= 4)
                tilemap.SetTile(cellPos, tiles[temp + 3]);
            else if (myTypeEnum == typeEnum.concreteToMud && temp <= 9 && temp >= 5)
                tilemap.SetTile(cellPos, tiles[getInt(tempTile.name) - 6]);
        }
    }
}
