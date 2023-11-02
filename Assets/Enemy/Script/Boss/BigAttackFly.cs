using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public enum bigAttackFlyState 
{
    Tracking,
    Shoot
}

public class BigAttackFly : Top_Fly
{
    /// <summary>
    /// 
    /// 1. 매우 천천히 플레이어 추적
    /// 2. 일정 시간마다 한바퀴 돌면서 직선으로 나가는 총알 뿜뿜
    /// 
    /// </summary>

    [Header("BigAttackFly")]
    [SerializeField] bigAttackFlyState state;
    [SerializeField] float currTime; //현재 상태의 시간
    [SerializeField] bool chageState; // 상태변환 
    [SerializeField]  float rotSpeed;
    [SerializeField] GameObject shootSpace;
    [SerializeField] GameObject bigShootBullet;

    void Start()
    {
        Fly_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        // Enemy
        hp = 30f;
        sight = 5f;
        moveSpeed = 1f;
        waitforSecond = 0.5f;
        attaackSpeed = 2f;
        bulletSpeed = 3f;

        maxhp = hp;

        //BigAttackFly
        chageState = true;
        currTime = attaackSpeed;
        rotSpeed = 100f;
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

        currTime -= Time.deltaTime;
        if (currTime > 0)
        {
            Tracking(justTrackingPlayerPosi);
        }

        else if (currTime <= 0)
        {
            bigAttackFlyShoot();
            currTime = attaackSpeed;
        }
    }

    public void bigAttackFlyShoot()
    {
        for (int i = 0; i < 10; i++) 
        {
            GameObject bu = Instantiate(bigShootBullet, shootSpace.transform.position, Quaternion.identity);
            bu.GetComponent<Rigidbody2D>().velocity = bu.transform.forward * bulletSpeed;
            shootSpace.transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
        }

    }

}
