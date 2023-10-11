using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tride : Top_Spider
{
    // 방에 들어오면 추적,
    // 일정 시간 마다 점프 (범위 밖에 있으면 아얘 글로 멀리 점프)
    enum TrideState 
    {
        Tracking, //추적
        Jump // 점프
            
    }


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
    }

    private void Update()
    {
        if (playerInRoom) 
        { }
    }

    public override void Move()
    {
       
    }
}
