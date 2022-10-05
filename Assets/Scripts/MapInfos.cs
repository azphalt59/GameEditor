using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapInfos : MonoBehaviour
{
    private int MapSize;
    public List<TileBase> Tiles;
    private TileBase TileBasePrefab;
    // Start is called before the first frame update
    void Start()
    {
        MapSize = MainEditor.Instance._MapSize;
    }

    public void SaveData()
    {
        
        Vector3Int[] positions = new Vector3Int[MapSize * MapSize];
        for (int i=0; i< (MapSize * MapSize); i++)
        {
            positions[i] = new Vector3Int(i % MapSize, i / MapSize, 0);
            Tiles.Add(TileBasePrefab);
            Tiles[i] = MainEditor.Instance.GetTilemap().GetTile(positions[i]);
        }
    }
    public void InstantiateSavedMap()
    {
        Vector3Int[] positions = new Vector3Int[MapSize * MapSize];
        for (int i = 0; i < (MapSize * MapSize); i++)
        {
            positions[i] = new Vector3Int(i % MapSize, i / MapSize, 0);
            MainEditor.Instance.GetTilemap().SetTile(positions[i], Tiles[i]);
        }
    }


}
