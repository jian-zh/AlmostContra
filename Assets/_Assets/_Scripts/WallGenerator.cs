using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator 
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions,TilemapVisualizer tilemapVisualizer) {

        var basicWallPositions = FindWallsInDirections(floorPositions,Direction2D.cardinalDirectionsList);
        foreach(Vector2Int wallPosition in basicWallPositions)
        {
            tilemapVisualizer.PaintSingleBasicWall(wallPosition);
        }
    }
    /// <summary>
    /// 获取房间边缘位置，将其加入Hashset集合中，用于Tilemap的砖块布置
    /// corner 角落的砖块需要另寻方式
    /// </summary>
    /// <param name="floorPositions"></param>
    /// <param name="cardinalDirectionsList"></param>
    /// <returns></returns>
    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> cardinalDirectionsList)
    {
        HashSet<Vector2Int> wallPosition = new HashSet<Vector2Int>();
        foreach (var floorPosition in floorPositions) {
            foreach (var direction in cardinalDirectionsList)
            {
                Vector2Int wallDirection = floorPosition + direction;
                if(floorPositions.Contains(wallDirection)==false)
                {
                    wallPosition.Add(wallDirection);
                }
            }

        }
        return wallPosition;
    }

}
