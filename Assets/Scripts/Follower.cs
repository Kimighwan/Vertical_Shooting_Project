using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public int followDelay;

    public float maxShoutDelay;
    public float curShoutDelay;

    public ObjectManager objectManager;

    public Vector3 followPos;
    public Transform parent;

    public Queue<Vector3> parentPos;

    void Awake()
    {
        parentPos = new Queue<Vector3>();
    }
    void Update()
    {
        Watch();
        Fire();
        Follow();
        Reload();
    }
    void Watch()
    {
        //Input Position
        if(!parentPos.Contains(parent.position))
            parentPos.Enqueue(parent.position);

        //Output Pos
        if (parentPos.Count > followDelay)
            followPos = parentPos.Dequeue();
        else if (parentPos.Count < followDelay)
            followPos = parent.position;
    }

    public void Follow()
    {
        transform.position = followPos;
    }

    void Fire()
    {
        if (!Input.GetButton("Fire1"))
            return;

        if (curShoutDelay < maxShoutDelay)
            return;

        //Power One
        GameObject bullet = objectManager.MakeObj("BulletFollower");
        bullet.transform.position = transform.position;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        curShoutDelay = 0;
    }

    void Reload()
    {
        curShoutDelay += Time.deltaTime;
    }
}
