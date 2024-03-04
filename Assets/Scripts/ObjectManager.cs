using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyBossPrefab;
    public GameObject enemyAPrefab;
    public GameObject enemyBPrefab;
    public GameObject enemyCPrefab;
    public GameObject itemCoinPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemBoomPrefab;
    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;
    public GameObject bulletFollowPrefab;
    public GameObject bulletBossAPrefab;
    public GameObject bulletBossBPrefab;
    public GameObject explosionPrefab;

    GameObject[] enemyA;
    GameObject[] enemyB;
    GameObject[] enemyC;
    GameObject[] enemyBoss;

    GameObject[] itemCoin;
    GameObject[] itemPower;
    GameObject[] itemBoom;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletFollow;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;

    GameObject[] targetPool;
    GameObject[] explosion;


    void Awake()
    {
        enemyA = new GameObject[20];
        enemyB = new GameObject[10];
        enemyC = new GameObject[10];
        enemyBoss = new GameObject[1];

        itemCoin = new GameObject[20];
        itemPower = new GameObject[10];
        itemBoom = new GameObject[10];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletEnemyA = new GameObject[100];
        bulletEnemyB = new GameObject[100];
        bulletFollow = new GameObject[100];
        bulletBossA = new GameObject[50];
        bulletBossB = new GameObject[1000];

        explosion = new GameObject[20];

        Generate();
    }

    void Generate()
    {
        //#1. Enemy
        for (int index = 0; index < enemyBoss.Length; index++)
        {
            enemyBoss[index] = Instantiate(enemyBossPrefab);
            enemyBoss[index].SetActive(false);
        }

        for (int index = 0;index < enemyC.Length; index++)
        {
            enemyC[index] = Instantiate(enemyCPrefab);
            enemyC[index].SetActive(false);
        }

        for (int index = 0; index < enemyA.Length; index++)
        {
            enemyA[index] = Instantiate(enemyAPrefab);
            enemyA[index].SetActive(false);
        }

        for (int index = 0; index < enemyB.Length; index++)
        {
            enemyB[index] = Instantiate(enemyBPrefab);
            enemyB[index].SetActive(false);
        }

        //#2. Item
        for (int index = 0; index < itemCoin.Length; index++)
        {
            itemCoin[index] = Instantiate(itemCoinPrefab);
            itemCoin[index].SetActive(false);
        }

        for (int index = 0; index < itemPower.Length; index++)
        {
            itemPower[index] = Instantiate(itemPowerPrefab);
            itemPower[index].SetActive(false);
        }

        for (int index = 0; index < itemBoom.Length; index++)
        {
            itemBoom[index] = Instantiate(itemBoomPrefab);
            itemBoom[index].SetActive(false);
        }

        //#3. Bullet
        for (int index = 0; index < bulletPlayerA.Length; index++)
        {
            bulletPlayerA[index] = Instantiate(bulletPlayerAPrefab);
            bulletPlayerA[index].SetActive(false);
        }

        for (int index = 0; index < bulletPlayerB.Length; index++)
        {
            bulletPlayerB[index] = Instantiate(bulletPlayerBPrefab);
            bulletPlayerB[index].SetActive(false);
        }

        for (int index = 0; index < bulletEnemyA.Length; index++)
        {
            bulletEnemyA[index] = Instantiate(bulletEnemyAPrefab);
            bulletEnemyA[index].SetActive(false);
        }

        for (int index = 0; index < bulletEnemyB.Length; index++)
        {
            bulletEnemyB[index] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyB[index].SetActive(false);
        }

        for (int index = 0; index < bulletFollow.Length; index++)
        {
            bulletFollow[index] = Instantiate(bulletFollowPrefab);
            bulletFollow[index].SetActive(false);
        }

        for (int index = 0; index < bulletBossA.Length; index++)
        {
            bulletBossA[index] = Instantiate(bulletBossAPrefab);
            bulletBossA[index].SetActive(false);
        }

        for (int index = 0; index < bulletBossB.Length; index++)
        {
            bulletBossB[index] = Instantiate(bulletBossBPrefab);
            bulletBossB[index].SetActive(false);
        }

        for (int index = 0; index < explosion.Length; index++)
        {
            explosion[index] = Instantiate(explosionPrefab);
            explosion[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "EnemyBoss":
                targetPool = enemyBoss; break;
            case "EnemyC":
                targetPool = enemyC; break;
            case "EnemyA":
                targetPool = enemyA; break;
            case "EnemyB":
                targetPool = enemyB; break;
            case "ItemCoin":
                targetPool = itemCoin; break;
            case "ItemPower":
                targetPool = itemPower; break;
            case "ItemBoom":
                targetPool = itemBoom; break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA; break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB; break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA; break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB; break;
            case "BulletFollower":
                targetPool = bulletFollow; break;
            case "BulletBossA":
                targetPool = bulletBossA; break;
            case "BulletBossB":
                targetPool = bulletBossB; break;
            case "Explosion":
                targetPool = explosion; break;
        }

        for(int index = 0;index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }    

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "EnemyBoss":
                targetPool = enemyBoss; break;
            case "EnemyC":
                targetPool = enemyC; break;
            case "EnemyA":
                targetPool = enemyA; break;
            case "EnemyB":
                targetPool = enemyB; break;
            case "ItemCoin":
                targetPool = itemCoin; break;
            case "ItemPower":
                targetPool = itemPower; break;
            case "ItemBoom":
                targetPool = itemBoom; break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA; break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB; break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA; break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB; break;
            case "BulletFollower":
                targetPool = bulletFollow; break;
            case "BulletBossA":
                targetPool = bulletBossA; break;
            case "BulletBossB":
                targetPool = bulletBossB; break;
            case "Explosion":
                targetPool = explosion; break;
        }

        return targetPool;
    }
}
