using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : Enemy
{
    [SerializeField] Transform hiveMovePosi;

    void Start()
    {
        hiveMovePosi = gameObject.transform;

        playerInRoom = false;
        dieParameter = "isDie";

        hp = 20f;
        sight = 3f;
        moveSpeed = 3f;
        waitforSecond = 0.5f;
        isInRange = false;
    }

    void Update()
    {
        if (playerInRoom)
            Move();
    }

    public override void Move()
    {

    }

    // 범위보기
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sight);

    }
}
