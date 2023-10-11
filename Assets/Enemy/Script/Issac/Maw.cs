using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Maw : Top_IssacMonster
{
    // 범위 안에 들어오면 추적 + 촣알 발싸
    // 범위 안에 없으면 랜덤 움직임
    // Pooter와 코드 같음

    enum MawState
    {
        // Idle
        PooterProwl,
        // 총 쏘는
        PooteShoot,
        //추적
        PooteTracking
    }
    [SerializeField] MawState state;
    [SerializeField] GameObject enemyBullet; //총알 프리팹
    float currTime;
    float oriMoveSpeed;


    // 작은 범위 랜덤 움직임 + 플레이어가 안에 들어오면 추적
    void Start()
    {

        playerInRoom = false;
        dieParameter = "isDie";

        //Enemy
        animator = GetComponent<Animator>();
        hp = 5f;
        sight = 4f;
        moveSpeed = 1.5f;
        waitforSecond = 0.5f;
        attaackSpeed = 3f; // idle <-> Shoot

        //TopFly
        randRange = 0.5f;
        fTime = 0.5f;
        StartCoroutine(checkPosi(randRange));

        //Footer
        currTime = attaackSpeed;
        oriMoveSpeed = moveSpeed;

    }

    private void Update()
    {
        if (playerInRoom)
        {
            Move();
            //플레이어가 범위 안에 없을 때
            if (!PlayerSearch())
            {
                state = MawState.PooterProwl;
                return;
            }

            //플레이어가 범위 안에 있을 때, 시간 별로 총 쏘고 , 이동하고
            else if (PlayerSearch())
            {
                currTime -= Time.deltaTime;
                if (currTime > 0)
                {
                    state = MawState.PooteTracking;
                    Lookplayer();
                    moveSpeed = oriMoveSpeed;
                    return;
                }

                else if (currTime <= 0)
                {
                    state = MawState.PooteShoot;
                    //moveSpeed = 0;
                    //animator.SetTrigger("isShoot");
                    currTime = attaackSpeed;
                }
            }
        }


    }

    override public void Move()
    {
        switch (state)
        {
            case MawState.PooterProwl:
                Prwol();
                break;
            case MawState.PooteShoot:
                PooterShoot();
                break;
            case MawState.PooteTracking:
                Tracking(playerPos);
                break;
        }

    }

    // 공격
    void PooterShoot()
    {
        GameObject bulletobj = Instantiate(enemyBullet, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sight);
    }
}
