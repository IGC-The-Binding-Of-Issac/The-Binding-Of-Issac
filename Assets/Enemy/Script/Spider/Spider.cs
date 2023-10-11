using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Top_Spider
{
    //  랜덤 움직임 + 플레이어 감지하면 플레이어한테 지그재그로 따라가기(?)
    enum SpiderState
    {
        iDle , //가만히
        Move , // 움직임
        Tracking // 추적

    }
    [SerializeField] SpiderState state;
    float currTime;

    void Start()
    {
        Spider_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        //최상위 enemy
        hp = 20f;
        sight = 3f;
        moveSpeed = 10f;
        waitforSecond = 0.5f;

        //상위 Top_Spider
        randRange = 2f;
        fTime = 0.5f; 
        StartCoroutine(checkPosi(randRange));
        
        currTime = fTime;
    }

    void Update()
    {
        if (playerInRoom)
        {
            Move();


        }


    }

    public override void Move()
    {
        Prwol();
    }



    // 범위보기
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sight);

    }
}
