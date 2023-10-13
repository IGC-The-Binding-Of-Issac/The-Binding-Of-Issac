using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footer : Top_Fly
{
    // 범위 안에 들어오면 추적 + 촣알 발싸
    // 범위 안에 없으면 랜덤 움직임
   
    enum FooterState 
    {
        // Idle
        PooterProwl,
        // 총 쏘는
        PooteShoot,
        //추적
        PooteTracking
    }
    [SerializeField] FooterState state;
    [SerializeField] GameObject enemyBullet; //총알 프리팹
    float currTime;
    float oriMoveSpeed;


    // 작은 범위 랜덤 움직임 + 플레이어가 안에 들어오면 추적
    void Start()
    {
        Fly_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        //Enemy
        animator = GetComponent<Animator>();
        hp = 8f;
        sight = 4f;
        moveSpeed = 1.5f;
        waitforSecond = 0.5f;
        attaackSpeed = 3f; // idle <-> Shoot

        //TopFly
        randRange = 1f;
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
                state = FooterState.PooterProwl;
                return;
            }

            //플레이어가 범위 안에 있을 때, 시간 별로 총 쏘고 , 이동하고
            else if (PlayerSearch()) 
            {
                currTime -= Time.deltaTime;
                if (currTime > 0)
                {
                    state = FooterState.PooteTracking;
                    Lookplayer();
                    moveSpeed = oriMoveSpeed;
                    return;
                }

                else if (currTime <= 0)
                {
                    state = FooterState.PooteShoot;
                    //moveSpeed = 0;
                    animator.SetTrigger("isShoot");
                    currTime = attaackSpeed;
                }
            }
        }


    }

    override public void Move()
    {
        switch (state) 
        {
            case FooterState.PooterProwl:
                Prwol();
                break;
            case FooterState.PooteShoot:
                PooterShoot();
                break;
            case FooterState.PooteTracking:
                Tracking(playerPos);
                break;
        }
        
    }

    // 공격
    void PooterShoot() 
    {
        GameObject bulletobj = Instantiate(enemyBullet , transform.position + new Vector3(0,-0.1f,0) , Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sight);
    }
}
