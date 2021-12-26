using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class elementTileManager : MonoBehaviour
{
    public List<TileBase> mudTiles = new List<TileBase>();
    public List<TileBase> dryMudTiles = new List<TileBase>();
    public List<TileBase> concreteTiles = new List<TileBase>();
    public List<TileBase> waterConcreteTiles = new List<TileBase>();
    
    // Start is called before the first frame update
    public static elementTileManager instance;
    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }
}
