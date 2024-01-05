using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minimap : MonoBehaviour
{
    //小地图大小
   static Vector2Int mapsize = new Vector2Int(100,100);
    private Image mapImage;

    private Texture2D tex;
    //玩家探索过的区域
    private int[,] visit;
    //地图
    private int[,] map;

    [SerializeField]
    public Color color;
    public HashSet<Vector2Int> floor;
    public Transform player;
    private void Awake()
    {
        map = new int[100, 100];
        mapImage = GetComponent<Image>();
        tex = new Texture2D(100, 100);
    }
    public void  LateUpdate()
    {
        if (GameObject.Find("Generator").GetComponent<RoomFirstDungonGenerator>().floor != null)
            floor = GameObject.Find("Generator").GetComponent<RoomFirstDungonGenerator>().floor;
        else
            floor = new HashSet<Vector2Int>();
        // player = GameObject.Find("Player").GetComponent<Transform>();
        //  ShowMinimap();
        ShowMinimap1();
    }
   
    /// <summary>
    /// tex.SetPixel(int x,int y,Color color);
    /// </summary>
    public void ShowMinimap()
    {
        tex = new Texture2D(100,100);
        int x = (int)(player.position.x);
        int y = (int)(player.position.y);
        for (int i = -5; i < 5; i++)
        {
            for(int j = -5; j < 5; j++)
            {
                if ((x + i) >= 0 && (x + i) < mapsize.x && (y + j) >= 0 && (y + j) < mapsize.y)
                {
                    visit[(x + i), (y + j)] = 1;
                }
            }
        }
        foreach(var flor in floor)
        {
            map[flor.x, flor.y] = 1;
        }
      for(int i = 0; i < mapsize.x; i++)
        {
            for(int j = 0; j < mapsize.y; j++)
            {
                if (i == 0 || j == 0 || i == mapsize.x - 1 || j == mapsize.y) 
                {
                    tex.SetPixel(i, j, Color.white);
                    continue;
                }
                 x = (int)(player.position.x)  - mapsize.x / 2 + i;
                 y = (int)(player.position.y) - mapsize.y / 2 + j;
                //x[-50,150]
                if (x > 0 && x < mapsize.x - 1 && y > 0 && y < mapsize.y - 1)  {
                    if (visit[x, y] == 1)
                    {
                        if (map[x, y] == 1)
                        {
                            tex.SetPixel(i, j, Color.blue);
                        }
                        else
                        {
                            tex.SetPixel(i, j, new Color(0.5f, 0.5f, 0.5f, 0.5f));
                        }

                    }
                    else
                    {
                        tex.SetPixel(i, j, new Color(0.2f, 0.2f, 0.2f, 0.5f));
                    }
                }
                else
                {
                    tex.SetPixel(i, j, new Color(0.2f, 0.2f, 0.2f, 0.5f));
                }
            }
        }
        
        tex.Apply();
        //t2d为待转换的Texture2D对象
        Sprite s = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        mapImage.sprite = s;

    }
    public void ShowMinimap1()
    {
        foreach(var flor in floor)
        {
            tex.SetPixel(flor.x, flor.y, Color.blue);
        }
        tex.Apply();
        //t2d为待转换的Texture2D对象
        Sprite s = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        mapImage.sprite = s;
    }
}
