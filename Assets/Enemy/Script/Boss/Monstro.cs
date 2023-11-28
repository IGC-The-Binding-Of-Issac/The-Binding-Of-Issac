using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Monstro : TEnemy
    {/// <summary>
     /// 1. 가만히 있음
     /// 2. 점프함
     /// 3. 여러갈래로 한번만 발사함
     /// </summary>
     /// 
    [Header("TEnemy")]
    [SerializeField] Animator MonstroAni;
    [SerializeField] GameObject bulletPosition;

    [SerializeField] float stateTime;
    [SerializeField] int stateNum;
    [SerializeField] float currTime;                // 현재 상태의 시간
    // Start is called before the first frame update
    void Start()
    {
        isFlipped = true;
        playerInRoom = false;
        dieParameter = "isDie";
        // Enemy
        hp = 100f;
        waitforSecond = 1f;   // 죽기전 시간
        attaackSpeed = 1.5f; // 총알 발사 하는 시간 
        bulletSpeed = 5f;

        maxhp = hp;

        //Monstro
        randTime();
        currTime = stateTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRoom)
        {
            Move();
        }

        e_Lookplayer();

    }
    void Move()
    {
        if (e_isDead())
        {
            e_destroyEnemy();
        }
        e_findPlayer();

        currTime -= Time.deltaTime;
        if (currTime > 0)
        {

        }
        else if (currTime <= 0)
        {

            // 초기화
            randNum();
            randTime();
            currTime = stateTime;

            if (stateNum == 1)
                MonstroSpit();
            else if (stateNum == 2)
                MonstroJump();

        }

    }
    void MonstroSpit()
    {
        MonstroAni.SetTrigger("SpitTrigger");
        for (int i = 0; i < 10; i++)
        {
            float randGravityScale;
            float randBulletSpeed;
            float randBulletSpeedY;
            randGravityScale = Random.Range(0f, 1f);
            randBulletSpeed = Random.Range(3f, 5f);
            randBulletSpeedY = Random.Range(2f, 4f);
            GameObject bulletobj = EnemyPooling.Instance.GetStraightBullet(bulletPosition); // 풀링 총알 오브젝트 가져오기
            if (GetComponent<SpriteRenderer>().flipX)
            {
                bulletobj.GetComponent<Rigidbody2D>().velocity = new Vector3(-randBulletSpeed, randBulletSpeedY, 0);
            }
            else
            {
                bulletobj.GetComponent<Rigidbody2D>().velocity = new Vector3(randBulletSpeed, randBulletSpeedY, 0);
            }
            bulletobj.GetComponent<Rigidbody2D>().gravityScale += randGravityScale;
        }
    }
    void MonstroJump()
    {
        float jumpPower = 3f;
        int numOfJump = 1;
        float duration = 1.5f;
        MonstroAni.SetTrigger("JumpTrigger");
        transform.DOJump(playerPosi.position, jumpPower, numOfJump, duration);
    }
    public void OffCrash()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    public void OnCrash()
    {
        GetComponent<Collider2D>().enabled = true;
    }
    void randTime()
    {
        //1f ~ 10f 사이에서 시간
        stateTime = Random.Range(1f, 3f);
    }

    void randNum()
    {
        stateNum = Random.Range(1, 3); // 1~2중
    }
}
