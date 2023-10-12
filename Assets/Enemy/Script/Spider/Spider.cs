using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Top_Spider
{
    //  랜덤 움직임 + 플레이어 감지하면 플레이어한테 지그재그로 따라가기(?)

    void Start()
    {
        Spider_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        //최상위 enemy
        hp = 2f;
        sight = 3f;
        moveSpeed = 5f;
        waitforSecond = 0.5f;

        //상위 Top_Spider
        randRange = 10f;
        fTime = 0.5f; 
        StartCoroutine(checkPosi(randRange));
        
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

}
