using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Gurdy : TEnemy
{
    [Header("BigAttackFly")]
    [SerializeField] float currTime; //현재 상태의 시간
    [SerializeField] bool chageState; // 상태변환 

    [SerializeField] GameObject bigShootBullet;
    float stateTime;
    [SerializeField]int stateNum; // 1은 총알 뿜뿜, 2는 파리 생성
    int shootCount;
    Coroutine runningCoroutine = null;


    void Start()
    {

        playerInRoom = false;

        // Enemy
        hp = 100f;
        waitforSecond = 0.4f;
        attaackSpeed = 1.5f; // 총알 발사 하는 시간 
        bulletSpeed = 3f;

        maxhp = hp;

        //Gurdy
        randTime(); 
        currTime = stateTime; //초기 시간 설정
        randNum();

        chageState = true;
        shootCount = 5; // 생성하는 총알의 갯수
    }

    private void Update()
    {
            
    }


    private void gurdyReset()
    {

    }

    void gurdyShoot() 
    {

    }
    
    void gurdyGeneFly() 
    {

    }

    void randTime()
    {
        //1f ~ 10f 사이에서 시간
        stateTime = Random.Range(2f, 5f);
    }

    void randNum()
    {
        // state 1.up , 2. down , 3.left , 4. right 상태 중에 하나를 랜덤으로 고름
        stateNum = Random.Range(1, 3); // 1~2중
    }



}
