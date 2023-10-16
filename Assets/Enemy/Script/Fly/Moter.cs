using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moter : Top_Fly
{
    [SerializeField ]GameObject attackFly;

    void Start()
    {
        Fly_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        //Enemy
        hp = 8f;
        sight = 5f;
        moveSpeed = 0.5f;
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
    void OnDestroy()
    {
        GenerateAttackFly();
        GenerateAttackFly();
    }

    override public void Move()
    {
        Tracking(justTrackingPlayerPosi);
    }

    void GenerateAttackFly() 
    {
        GameObject obj = Instantiate(attackFly, transform.position, Quaternion.identity) as GameObject;
        roomInfo.GetComponent<Room>().enemis.Add(obj);
    }

}
