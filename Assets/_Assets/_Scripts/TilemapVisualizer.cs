using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap,wallTilemap;
    [SerializeField]
    private TileBase floorTile,wallTop;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }
    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, wallTop, position);
        create2DCollider(position);
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);

    }

    private void create2DCollider(Vector2Int position)
    {
        GameObject gameObject = new GameObject();
        BoxCollider2D boxCollider2D= gameObject.AddComponent<BoxCollider2D>();
        boxCollider2D.offset=new Vector2(0.5f, 0.5f);
        gameObject.layer = 9;
        //oneway层实现站在上面
        gameObject.transform.position=new Vector3Int(position.x,position.y,0);
        //标签设置
        gameObject.tag = "Wall";
        
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
