using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungenCameraController : MonoBehaviour
{
    public BoxCollider2D cameraBounds;

    public bool isFollowing;


    private Transform player;

    private Vector2 min;
    private Vector2 max;

    // Start is called before the first frame update
    void Start()
    {
          player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        min = cameraBounds.bounds.min;
        max = cameraBounds.bounds.max;
        var x = transform.position.x;
        var y = transform.position.y;
        if (isFollowing)
        {
            x = player.position.x;
            y = player.position.y;

        }
       
       
        var cameraHaflWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);
        var cameraHaflHeight = cameraHaflWidth * ((float)Screen.height / Screen.width);
        x = Mathf.Clamp(x, min.x + cameraHaflWidth, max.x - cameraHaflWidth);
        y = Mathf.Clamp(y, min.y + cameraHaflHeight, max.y - cameraHaflHeight);
        transform.position = new Vector3(x, y, transform.position.z);
        //LeftBorder.transform.position = new Vector2(x - cameraHaflWidth, transform.position.y);
    }
}
