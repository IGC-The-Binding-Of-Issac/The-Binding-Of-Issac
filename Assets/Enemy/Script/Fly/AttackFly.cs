using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFly : Top_Fly
{
    // 플레이어 추적
    void Start()
    {
        Fly_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        // Enemy
        hp = 2f;
        sight = 5f;
        moveSpeed = 1.5f;
        waitforSecond = 0.5f;

        maxhp = hp;
    }

    private void Update()
    {
        justTrackingPlayerPosi = GameObject.FindWithTag("Player").transform;
        if (justTrackingPlayerPosi == null)
            return;

        if (playerInRoom)
            Move();
    }

    override public void Move()
    {
        Tracking(justTrackingPlayerPosi);
    }
}