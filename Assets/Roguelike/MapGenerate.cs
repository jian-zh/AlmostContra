using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour
{
    [Header("房间数量")]
    public int numRoomTries;
    [Header("房间最大尺寸")]
    public int roomExtraSize;
    //目前房间数量
    private int currentRegionIndex;

    private int width;
    private int height;
    // Start is called before the first frame update
    void Start()
    {
        width = 207;
        height = 207;
        currentRegionIndex = 0;
         AddRooms();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    int AddRooms()
    {
        List<Rect> rooms = new List<Rect>();

        for (int i = 0; i < numRoomTries; ++i)
        {
            //  生成房间，并确保：
            //      ①房间尺寸为奇数
            //      ②房间形状不会过于狭长
            int size = UnityEngine.Random.Range(1, 3 + roomExtraSize) * 2 + 1;
            int rectangularity = UnityEngine.Random.Range(0, 1 + size / 2) * 2;
            int w = size;
            int h = size;
            if (0 == UnityEngine.Random.Range(0, 100) % 2)
            {
                w += rectangularity;
            }
            else
            {
                h += rectangularity;
            }

            int x = UnityEngine.Random.Range((-width + w) / 2, (width - w) / 2) * 2 + 1;
            int y = UnityEngine.Random.Range((-height + h) / 2, (height - h) / 2) * 2 + 1;
            //  根据以上算法生成一个新房间
            Rect room = new Rect(x, y, w, h);
            //  判断新房间是否与已存在的房间重叠
            bool overlaps = false;
            foreach (Rect other in rooms)
            {
                if (IsOverlap(room, other))
                {
                    overlaps = true;
                    break;
                }
            }
            if (overlaps)
            {
                continue;
            }
            //  将新房间加入房间列表
            rooms.Add(room);
            //  将新房间写入地图
            StartRegion();
            for (int k = x; k < x + w; ++k)
            {
                for (int j = y; j < y + h; ++j)
                {
                     // Carve(new Vector2(k, j));
                   
                }
            }
        }

        return 0;
    }
    void StartRegion()
    {
        ++currentRegionIndex;
       
    }
    bool IsOverlap(Rect rect, Rect other) {
        float max_x = rect.x+rect.width;
        float min_x = rect.x;
        float max_y = rect.y + rect.height;
        float min_y = rect.y;


        return false;
    }
}
