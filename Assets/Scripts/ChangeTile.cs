using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class ChangeTile : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile;
    public GameObject changePoint;
    
    public  Grid grid;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
            Vector3Int cellPos = grid.WorldToCell(changePoint .transform .position );
            tilemap.SetTile(cellPos, tile);
    }

}
