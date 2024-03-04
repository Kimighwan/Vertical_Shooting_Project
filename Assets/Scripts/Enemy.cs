using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyname;
    public float speed;
    public int health;
    public int enemyScore;

    public int patternIndex;
    public int curpatternCount;
    public int[] maxPatternCount;

    public Sprite[] sprites;

    public float maxShoutDelay;
    public float curShoutDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject player;
    public GameObject itemCoin;
    public GameObject itemBoom;
    public GameObject itemPower;
    public GameManager gameManager;

    public ObjectManager objectManager;

    SpriteRenderer spriteRenderer;

    Animator anim;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(enemyname == "Boss")
            anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        switch(enemyname)
        {
            case "C":
                health =15; 
                break;
            case "B":
                health = 7; 
                break;
            case "A":
                health = 4;
                break;
            case "Boss":
                health = 200;
                Invoke("Stop", 2);
                break;
        }
    }

    void Stop()
    {
        if (!gameObject.activeSelf)
            return;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Think", 2);
    }
    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curpatternCount = 0;

        switch (patternIndex)
        {
            case 0:
                FireFoward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;

        }
    }

    void FireFoward()
    {
        if(health <= 0)
            return;
        //fire
        GameObject bulletR = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.5f;
        GameObject bulletRR = objectManager.MakeObj("BulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 0.9f;
        GameObject bulletL = objectManager.MakeObj("BulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.5f;
        GameObject bulletLL = objectManager.MakeObj("BulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 0.9f;

        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

        rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);

        //Patern
        curpatternCount++;
        if(curpatternCount < maxPatternCount[patternIndex])
            Invoke("FireFoward", 2);
        else
        Invoke("Think", 3);
    }

    void FireShot()
    {
        if (health <= 0)
            return;

        for (int index = 0;index< 5;index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletBossB");
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }

        curpatternCount++;
        if (curpatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 3.5f);
        else
            Invoke("Think", 3);
    }

    void FireArc()
    {
        if (health <= 0)
            return;

        //Fire Arc Continue Fire
        GameObject bullet = objectManager.MakeObj("BulletEnemyA"); 
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 10 * curpatternCount/ maxPatternCount[patternIndex]), -1);
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

        //Pattern Count
        curpatternCount++;

        if (curpatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.15f);
        else
            Invoke("Think", 3);
    }

    void FireAround()
    {
        if (health <= 0)
            return;

        int roundNumA = 50;
        int roundNumB = 40;
        int roundNum = curpatternCount%2 ==0 ? roundNumA : roundNumB;
        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletBossB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum), Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

            Vector3 rotVec  = Vector3.forward * 360 * index / roundNum + Vector3.forward*90;
            bullet.transform.Rotate(rotVec);
        }

        curpatternCount++;
        if (curpatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 3);
    }

    void Update()
    {
        if (enemyname == "Boss")
            return;
        Fire();
        Reload();
    }

     public void OnHit(int dmg)
    {
        if (health <= 0)
            return;
        health -= dmg;

        if(enemyname == "Boss")
        {
            anim.SetTrigger("OnHit");
        }
        else
        {
            spriteRenderer.sprite = sprites[1]; //Æò¼Ò´Â 0 ÇÇ°Ý½Ã 1
            Invoke("ReturnSprite", 0.1f);
        }

        if(health <= 0)
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;

            //#. ¾ÆÀÌÅÛÀ» ¶³±¼²«Áö ¸»²«Áö // Random Ratio Item Drop
            int ran = enemyname =="Boss" ? 0 : Random.Range(0, 10);
            if (ran < 5)
            {
                GameObject itemPower = objectManager.MakeObj("ItemPower");
                itemPower.transform.position = transform.position;
            }
            else if (ran < 8) // Coin
            {
                GameObject itemCoin = objectManager.MakeObj("ItemCoin");
                itemCoin.transform.position = transform.position;
            }
            else if(ran < 9) // Power
            {
                Debug.Log("Not Item");
            }
            else if(ran < 10) // Boom
            {
                GameObject itemBoom = objectManager.MakeObj("ItemBoom");
                itemBoom.transform.position = transform.position;
            }
            gameObject.SetActive(false);
            CancelInvoke();
            transform.rotation = Quaternion.identity;

            gameManager.CallExplosion(transform.position, enemyname);

            //Boss Kill
            if (enemyname == "Boss")
            {
                CancelInvoke();
                gameManager.StageEnd();
            }
        }

    }

    void ReturnSprite() 
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet" && enemyname != "Boss")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            collision.gameObject.SetActive(false);
        }
            
    }

    void Fire()
    {
        if (curShoutDelay < maxShoutDelay)
            return;

        if(enemyname == "A")
        {
            GameObject bulletA = objectManager.MakeObj("BulletEnemyA");
            bulletA.transform.position = transform.position;

            Rigidbody2D rigidA = bulletA.GetComponent<Rigidbody2D>();
            Vector3 dirVecA = player.transform.position - transform.position;
            rigidA.AddForce(dirVecA.normalized * 3, ForceMode2D.Impulse);
        }
        else if (enemyname == "C")
        {
            GameObject bulletCR = objectManager.MakeObj("BulletEnemyB");
            bulletCR.transform.position = transform.position + Vector3.right * 0.3f;

            GameObject bulletCL = objectManager.MakeObj("BulletEnemyB");
            bulletCL.transform.position = transform.position + Vector3.left * 0.3f;

            Rigidbody2D rigidCR = bulletCR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidCL = bulletCL.GetComponent<Rigidbody2D>();

            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            rigidCR.AddForce(dirVecR.normalized * 4, ForceMode2D.Impulse);
            rigidCL.AddForce(dirVecL.normalized * 4, ForceMode2D.Impulse);
        }

        curShoutDelay = 0;
    }

    void Reload()
    {
        curShoutDelay += Time.deltaTime;
    }
}
