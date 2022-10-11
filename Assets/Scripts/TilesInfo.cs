using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesInfo : MonoBehaviour
{
    public Tile RepaintTile;
    public Vector3Int SelectedTilePosition;
    public Vector3Int OldTilePosition;
    public TileBase OldTile;
    public TileBase currentTile;
  
    public void TilePreview()
    {
        SelectedTilePosition = MainEditor.Instance.MousePositionToCellPosition();
        currentTile = MainEditor.Instance.GetTilemap().GetTile(SelectedTilePosition);
        RepaintTile = MainEditor.Instance._RepaintTile;
        if (SelectedTilePosition != OldTilePosition)
        {
            if(OldTile != null)
            {
                MainEditor.Instance.GetTilemap().SetTile(OldTilePosition, OldTile);
            }
            OldTilePosition = SelectedTilePosition;
            OldTile = currentTile;
        }
        MainEditor.Instance.GetTilemap().SetTile(SelectedTilePosition, RepaintTile);
    }
    public void ResetOldTileData()
    {
        OldTile = null;
        OldTilePosition = SelectedTilePosition;
    }
}
