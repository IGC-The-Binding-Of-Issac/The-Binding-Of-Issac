using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class BigAttackFly : TEnemy
{
    /// <summary>
    /// 
    /// 1. 플레이어 추적
    /// 2. 일정 시간마다 한바퀴 돌면서 직선으로 나가는 총알 뿜뿜
    /// 
    /// </summary>

    [Header("BigAttackFly")]
    [SerializeField] float currTime;                // 현재 상태의 시간
    [SerializeField] bool chageState;               // 상태변환 
    [SerializeField] float rotSpeed;                // 회전 속도
    [SerializeField] GameObject bigShootBullet;     // 총알

    float z                     = 0;
    bool coruState;
    Coroutine runningCoroutine  = null;

    void Start()
    {         
        animator = GetComponent<Animator>();    

        playerInRoom = false;
        dieParameter = "isBigFlyDie";

        // Enemy
        hp              = 30f;
        sight           = 5f;
        moveSpeed       = 1f;
        waitforSecond   = 0.4f;
        attaackSpeed    = 4f;
        bulletSpeed     = 6f;

        maxhp           = hp;

        //BigAttackFly
        chageState      = true;
        currTime        = attaackSpeed;
        rotSpeed        = 600f;
        chageState      = true;
        coruState       = true;
    }

    private void Update()
    {
        if (playerInRoom)
            Move();
    }

    public void Move()
    {
        if (e_isDead())
            e_destroyEnemy();        
        e_findPlayer();                     // player 감지

        currTime -= Time.deltaTime;
        if (currTime > 0 && chageState)
        {
            e_Tracking(moveSpeed);                   // 추적
        }
        else if (currTime <= 0) 
        {
            chageState = false;
            bigAttackFlyRotation();
        }
    }

    public void bigAttackFlyRotation()
    {
        if (coruState)
        {
            // StopCoroutine이 안돌아가요 ->
            // 코루틴 변수 runnungCorutine을 만들어서 실행과 동시에 저장 
            runningCoroutine = StartCoroutine(ShootBullets());
            coruState = false;
        }

        //회전
        // 회전값이 음수가 돼요 -> 유니티에서  일정 회전값이 지나면 음수가됨
        // 오일러 방식 : 짐벌락 현상때문 -> 각도는 쿼터니언 방식으로 (출력하면 소숫점)
        // 오일러 방식으로 출력하고싶어요 -> transform.rotation.eulerAngles

        z += rotSpeed * Time.deltaTime; //일정 시간 (Time.deltaTime) 마다 z축을 더한다
        transform.rotation = Quaternion.Euler(0, 0, z);
        //총알발싸



        if(transform.rotation.eulerAngles.z >= 350)  //각도가 360이 되면 초기화
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);// 회전 초기화
            if (runningCoroutine != null) //실행중인 코루틴이 있으면
            {
                // 멈추기
                StopCoroutine(runningCoroutine);
            }

            // 초기화
            z = 0; // 각도 초기화
            coruState = true;
            chageState = true;
            currTime = attaackSpeed;
        }
    }

    IEnumerator ShootBullets()
    {
        while (true) 
        {
            bool isbullet = true;
            if (isbullet) 
            {
                GameObject bu = Instantiate(bigShootBullet, transform.position, transform.rotation);
                bu.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
                isbullet = false;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }


}
