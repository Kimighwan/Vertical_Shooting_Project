using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;
    public bool isHit;
    public bool isBoomTime;
    public bool isRespawnTime;
    public bool[] joyConrtol;
    public bool isControl;
    public bool isButtonA;
    public bool isButtonB;

    public int life;
    public int score;
    public int power;
    public int maxPower;
    public int boom;
    public int maxBoom;

    public float speed;
    public float maxShoutDelay;
    public float curShoutDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject boomEffect;
    public GameObject[] followers;

    public ObjectManager objectManager;
    public GameManager gameManager;

    Animator anim;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        Unbeatable();
        Invoke("Unbeatable", 2);
    }

    void Unbeatable()
    {
        isRespawnTime = !isRespawnTime;
        if (isRespawnTime)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);

            for(int index = 0; index < followers.Length; index++)
            {
                followers[index].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);

            for (int index = 0; index < followers.Length; index++)
            {
                followers[index].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    void Update()
    {
        Fire();
        Move();
        Boom();
        Reload();
    }

    public void JoyPanel(int type)
    {
        for(int index = 0;index < 9;index++) {
            joyConrtol[index] = index == type;
        }
    }

    public void JoyDown()
    {
        isControl = true;
    }

    public void JoyUp()
    {
        isControl = false;
    }

    void Move()
    {
        //KeyBoard Control
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //Joy Control
        if (joyConrtol[0]) { h = -1; v = 1; }
        if (joyConrtol[1]) { h = 0; v = 1; }
        if (joyConrtol[2]) { h = 1; v = 1; }
        if (joyConrtol[3]) { h = -1; v = 0; }
        if (joyConrtol[4]) { h = 0; v = 0; }
        if (joyConrtol[5]) { h = 1; v = 0; }
        if (joyConrtol[6]) { h = -1; v = -1; }
        if (joyConrtol[7]) { h = 0; v = -1; }
        if (joyConrtol[8]) { h = 1; v = -1; }

        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1) || !isControl)
            h = 0;
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1) || !isControl)
            v = 0;
        Vector3 curPos = transform.position; // 현재 위치 가져오기
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime; // 이동할 위치

        transform.position = curPos + nextPos;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
    }

    public void ButtonADown()
    {
        isButtonA = true;
    }

    public void ButtonAUp()
    {
        isButtonA = false;
    }
    public void ButtonBDwon()
    {
        isButtonB = true;
    }

    void Fire()
    {
        //if (!Input.GetButton("Fire1"))
        //    return;

        if (!isButtonA)
            return;

        if (curShoutDelay < maxShoutDelay)
            return;

        switch (power)
        {
            case 1:
                //Power One
                GameObject bulletA = objectManager.MakeObj("BulletPlayerA");
                bulletA.transform.position = transform.position;

                Rigidbody2D rigidA = bulletA.GetComponent<Rigidbody2D>();
                rigidA.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletAR = objectManager.MakeObj("BulletPlayerA");
                bulletAR.transform.position = transform.position + Vector3.right * 0.15f;

                GameObject bulletAL = objectManager.MakeObj("BulletPlayerA");
                bulletAL.transform.position = transform.position + Vector3.left * 0.15f;


                Rigidbody2D rigidAR = bulletAR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidAL = bulletAL.GetComponent<Rigidbody2D>();
                rigidAR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidAL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletB = objectManager.MakeObj("BulletPlayerB");
                bulletB.transform.position = transform.position;

                Rigidbody2D rigidB = bulletB.GetComponent<Rigidbody2D>();
                rigidB.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            default:
                GameObject bulletBR = objectManager.MakeObj("BulletPlayerB");
                bulletBR.transform.position = transform.position + Vector3.right * 0.25f;

                GameObject bulletBL = objectManager.MakeObj("BulletPlayerB");
                bulletBL.transform.position = transform.position + Vector3.left * 0.25f;

                Rigidbody2D rigidBR = bulletBR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidBL = bulletBL.GetComponent<Rigidbody2D>();
                rigidBR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidBL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

        }
        

        curShoutDelay = 0;
    }

    void Reload()
    {
        curShoutDelay += Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {

            if (isRespawnTime)
                return;

            if (isHit)
                return;
            isHit = true;
            life--;
            gameManager.UpdateLifeIcon(life);
            gameManager.CallExplosion(transform.position, "P");

            if (life == 0)
            {
                gameManager.GameOver();
            }
            else
            {
                gameManager.RespawnPlayer();
            }

            gameObject.SetActive(false);
            if(collision.gameObject.tag == "Enemy")
            {
                Enemy enemyLogic = collision.gameObject.GetComponent<Enemy>();
                if (enemyLogic.enemyname == "Boss")
                    return;
                else
                    collision.gameObject.SetActive(false);
            }
        }
        else if (collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Coin":
                    score += 1000;
                    break;
                case "Power":
                    if (maxPower == power)
                        score += 500;
                    else
                    {
                        power++;
                        AddFollower();
                    }
                    break;
                case "Boom":
                    if (boom == maxBoom)
                        score += 500;
                    else
                    {
                        boom++;
                        gameManager.UpdateBoomIcon(boom);
                    }
                    break;
            }
            collision.gameObject.SetActive(false);
        }

    }

    void AddFollower()
    {
        if (power == 4)
            followers[0].SetActive(true);
        else if (power == 5)
            followers[1].SetActive(true);
        else if (power == 6)
            followers[2].SetActive(true);
    }
    void Boom()
    {
        /*if (!Input.GetButton("Fire2"))
            return;*/

        if (!isButtonB)
            return;

        if (isBoomTime)
            return;

        if (boom == 0)
            return;

        isBoomTime = true;
        boom--;
        gameManager.UpdateBoomIcon(boom);

        //Boom Effect
        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 4f);

        //Remove Enemy
        GameObject[] enemiesA = objectManager.GetPool("EnemyA");
        GameObject[] enemiesB = objectManager.GetPool("EnemyB");
        GameObject[] enemiesC = objectManager.GetPool("EnemyC");
        GameObject[] enemiesBoss = objectManager.GetPool("EnemyBoss");

        for (int index = 0; index < enemiesA.Length; index++)
        {
            if (enemiesA[index].activeSelf)
            {
                Enemy enemylogic = enemiesA[index].GetComponent<Enemy>();
                enemylogic.OnHit(1000);
            }
        }

        for (int index = 0; index < enemiesB.Length; index++)
        {
            if (enemiesB[index].activeSelf)
            {
                Enemy enemylogic = enemiesB[index].GetComponent<Enemy>();
                enemylogic.OnHit(1000);
            }
        }

        for (int index = 0; index < enemiesC.Length; index++)
        {
            if (enemiesC[index].activeSelf)
            {
                Enemy enemylogic = enemiesC[index].GetComponent<Enemy>();
                enemylogic.OnHit(1000);
            }
        }

        for (int index = 0; index < enemiesBoss.Length; index++)
        {
            if (enemiesBoss[index].activeSelf)
            {
                Enemy enemylogic = enemiesBoss[index].GetComponent<Enemy>();
                enemylogic.OnHit(1000);
            }
        }

        //Remove bullet
        GameObject[] bulletsA = objectManager.GetPool("BulletEnemyA");
        GameObject[] bulletsB = objectManager.GetPool("BulletEnemyB");

        for (int index = 0; index < bulletsA.Length; index++)
        {
            if (bulletsA[index].activeSelf)
                bulletsA[index].SetActive(false);
        }

        for (int index = 0; index < bulletsB.Length; index++)
        {
            if (bulletsB[index].activeSelf)
                bulletsB[index].SetActive(false);
        }
    }

    void OffBoomEffect()
    {
        boomEffect.SetActive(false);
        isBoomTime = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
    
    
}
