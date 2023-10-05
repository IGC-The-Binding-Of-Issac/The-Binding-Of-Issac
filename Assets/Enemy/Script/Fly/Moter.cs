using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moter : Fly
{
    void Start()
    {
        InitialIze();

        hp = 5f;
        sight = 5f;
        moveSpeed = 1.5f;
        waitforSecond = 0.5f;
    }

    private void Update()
    {
        justTrackingPlayerPosi = GameObject.FindWithTag("Player").transform;
        if (justTrackingPlayerPosi == null)
            return;

        if (playerInRoom)
        {
            Move();
        }
    }

    override public void Move()
    {
        Tracking(justTrackingPlayerPosi);
    }

}
