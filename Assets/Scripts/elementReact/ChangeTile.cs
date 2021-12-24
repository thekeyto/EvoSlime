using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class ChangeTile : MonoBehaviour
{
    public Grid grid;
    public Tilemap tilemap;
    public List<TileBase> tiles = new List<TileBase>();
    public GameObject changePoint;
    // Start is called before the first frame update

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
            Vector3Int cellPos = grid.WorldToCell(changePoint .transform .position );
            TileBase tempTile = tilemap.GetTile(cellPos);
            if (myTypeEnum==typeEnum.mudToConcrete&&getInt(tempTile.name)<=4)
            tilemap.SetTile(cellPos, tiles[getInt(tempTile.name)+4]);
            else if(myTypeEnum == typeEnum.concreteToMud && getInt(tempTile.name) >4)
            tilemap.SetTile(cellPos, tiles[getInt(tempTile.name) -4]);
    }

}
