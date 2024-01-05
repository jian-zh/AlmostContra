using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour
{
    [Header("��������")]
    public int numRoomTries;
    [Header("�������ߴ�")]
    public int roomExtraSize;
    //Ŀǰ��������
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
            //  ���ɷ��䣬��ȷ����
            //      �ٷ���ߴ�Ϊ����
            //      �ڷ�����״�����������
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
            //  ���������㷨����һ���·���
            Rect room = new Rect(x, y, w, h);
            //  �ж��·����Ƿ����Ѵ��ڵķ����ص�
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
            //  ���·�����뷿���б�
            rooms.Add(room);
            //  ���·���д���ͼ
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
