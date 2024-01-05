using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFirstDungonGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    private int minWidth=10, minHeight=5;
    [SerializeField]
    private int dungeonWidth = 100, dungeonHeight = 100;
    [SerializeField]
    private bool randomWalkDungeon;
    //·¿¼ä¼ä¸ô
    [Range(0, 10)][SerializeField]
    private int offset = 1;
    public  HashSet<Vector2Int> floor;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            RunProceduralGeneration();
            Debug.Log("pcg");
        }
    }

    protected override void RunProceduralGeneration() {
        tilemapVisualizer.Clear();
        CreatRooms();
    }
    private void CreatRooms()
    {
        List<BoundsInt> roomList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(
            new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minWidth, minHeight);
        floor = new HashSet<Vector2Int>();
        floor = CreateSimpleRoom(roomList);
        List<Vector2Int> roomCenters=new List<Vector2Int>();
        foreach(var room in roomList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }
        var corridors=ConnectRooms(roomCenters);
        floor.UnionWith(corridors);
        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateSimpleRoom(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        //BoundsInt min.x min.y
        //size.x size.y
        
        foreach(var room in roomList)
        {
            for(int i = offset;i< room.size.x - offset; i++)
            {
                for(int j = offset; j < room.size.y - offset; j++)
                {
                    Vector2Int position = (Vector2Int)room.position + new Vector2Int(i, j);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters) {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        Vector2Int currentRoomCenter = roomCenters[UnityEngine.Random.Range(0,roomCenters.Count)];
        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            roomCenters.Remove(closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        while (position.y!= destination.y)
        {
            if (position.y > destination.y)
            {
                position += Vector2Int.down;
            }
            else if (position.y < destination.y)
            {
                position += Vector2Int.up;
            }
            
                corridor.Add(position);
                corridor.Add(position + Vector2Int.left);
                corridor.Add(position + Vector2Int.right);
        }
        while (position.x != destination.x) 
        {
            if (position.x > destination.x)
            {
                position += Vector2Int.left;
            }
            else if (position.x < destination.x)
            {
                position += Vector2Int.right;
            }
            corridor.Add(position);
            corridor.Add(position + Vector2Int.up);
            corridor.Add(position + Vector2Int.down);
        }
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach(var room in roomCenters)
        {
            if (distance > Vector2Int.Distance(currentRoomCenter, room))
            { 
                distance = Vector2Int.Distance(currentRoomCenter, room);
                closest = room;
            } 
        }
        return closest;
        
    }
}
