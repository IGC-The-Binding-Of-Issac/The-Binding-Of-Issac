using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

enum MawState
{
    // Idle
    MawProwl,
    // 총 쏘는
    MawShoot,
    //추적
    MawTracking
}

public class Maw : Top_IssacMonster
{
    // 범위 안에 들어오면 추적 + 촣알 발싸
    // 범위 안에 없으면 랜덤 움직임
    // Pooter와 코드 같음


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
        waitforSecond = 1f;
        attaackSpeed = 3f; // idle <-> Shoot

        maxhp = hp;
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
                state = MawState.MawProwl;
                return;
            }

            //플레이어가 범위 안에 있을 때, 시간 별로 총 쏘고 , 이동하고
            else if (PlayerSearch())
            {
                currTime -= Time.deltaTime;
                if (currTime > 0)
                {
                    state = MawState.MawTracking;
                    Lookplayer();
                    //moveSpeed = oriMoveSpeed;
                    return;
                }

                else if (currTime <= 0)
                {
                    state = MawState.MawShoot;
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
            case MawState.MawProwl:
                Prwol();
                break;
            case MawState.MawShoot:
                MawShoot();
                break;
            case MawState.MawTracking:
                Tracking(playerPos);
                break;
        }

    }

    // 공격
    void MawShoot()
    {
        GameObject bulletobj = Instantiate(enemyBullet, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sight);
    }
}
