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
    /// ��ȡ�����Եλ�ã��������Hashset�����У�����Tilemap��ש�鲼��
    /// corner �����ש����Ҫ��Ѱ��ʽ
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
