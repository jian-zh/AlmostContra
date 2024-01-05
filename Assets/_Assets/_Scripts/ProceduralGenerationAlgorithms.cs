using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>(); 
        path.Add(startPosition);
        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }



    /// <summary>
    /// 二叉空间划分算法
    /// 将AABB包围盒划随机水平垂直切分成两部分
    /// 达到不可划分的条件即添加入 room中
    /// 仍可划分则添加入queue中等待下次划分
    /// </summary>
    /// <param name="spaceToSplit"></param>
    /// <param name="minWidth"></param>
    /// <param name="minHeight"></param>
    /// <returns></returns>
    internal static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if (room.size.x > minWidth && room.size.y > minHeight)
            {
                //room can be split
                if (Random.value > 0.5f)
                {
                    if (room.size.x > 2 * minWidth)
                    {
                        SplitVertically(minWidth, minHeight, roomsQueue, room);
                    }
                    else if (room.size.y > 2 * minHeight)
                    {
                        SplitHorizontally(minWidth, minHeight, roomsQueue, room);
                    }
                    else
                    {
                        roomList.Add(room);
                    }

                }
                else
                {
                    if (room.size.y > 2 * minHeight)
                    {
                        SplitHorizontally(minWidth, minHeight, roomsQueue, room);
                    }
                    else if (room.size.x > 2 * minWidth)
                    {
                        SplitVertically(minWidth, minHeight, roomsQueue, room);
                    }
                    else
                    {
                        roomList.Add(room);
                    }
                }

            }
        }
        return roomList;

    }
    /// <summary>
    /// minWidth,minHeight未使用
    /// </summary>
    /// <param name="minWidth"></param>
    /// <param name="minHeight"></param>
    /// <param name="roomsQueue"></param>
    /// <param name="room"></param>
    private static void SplitVertically(int minWidth, int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        int xSplit = Random.Range(1, room.size.x - 1);
        BoundsInt bound1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt bound2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(bound1);
        roomsQueue.Enqueue(bound2);
    }

    private static void SplitHorizontally(int minWidth, int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        int ySplit = Random.Range(1, room.size.y - 1);
        BoundsInt bound1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt bound2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(bound1);
        roomsQueue.Enqueue(bound2);

    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0) //LEFT
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
  
}