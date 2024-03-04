using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    float speed = 1;
    float viewHeight;
    Transform[] child;

    public int startIndex;
    public int endIndex; // 1번 배경을 보고 밑으로 내려간 곳

    public GameObject[] sprites;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2f;
        child = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        Move();
        Scrolling();
    }

    void Move()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        sprites[0].transform.Translate( nextPos * 4);
        sprites[1].transform.Translate( nextPos * 2);
        sprites[2].transform.Translate( nextPos * 1);
        
    }

    void Scrolling()
    {

        if (child[1].position.y < -10 )
        {
            child[1].transform.position = new Vector3(0, 20, 0);
        }
        if (child[2].position.y < -10)
        {
            child[2].transform.position = new Vector3(0, 20, 0);
        }
        if (child[3].position.y < -10)
        {
            child[3].transform.position = new Vector3(0, 20, 0);
        }

        //Sprtie Re Use
        /*if (sprites[endIndex].position.y < viewHeight * (-1))
        {
            Vector3 backSpritePos = sprites[startIndex].localPosition;
            Vector3 frontSpritePos = sprites[endIndex].localPosition;
            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.up * viewHeight;

            //Cursor Index Change
            int startIndexSave = startIndex;
            startIndex = endIndex;
            endIndex = startIndexSave - 1 == -1 ? sprites.Length - 1 : startIndexSave - 1;
        }*/
    }
}
