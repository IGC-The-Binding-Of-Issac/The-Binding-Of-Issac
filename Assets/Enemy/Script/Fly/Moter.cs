using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moter : TEnemy
{
    // 죽을 때 파리 두마리 생성

    [SerializeField] GameObject attackFly;

    public override void En_setState()
    {
        playerInRoom     = false;
        dieParameter     = "isDie";

        hp               = 2f;
        sight            = 5f;
        moveSpeed        = 1.5f;
        waitforSecond    = 0.5f;

        maxhp            = hp;
    }

    public override void En_kindOfEnemy()
    {
        isTracking      = true;
        isProwl         = false;
        isDetective     = false;
        isShoot         = false;
    }

    private void Start()
    {
        // 하위 몬스터 state 설정
        En_setState();              // 스탯 설정
        En_kindOfEnemy();           // enemy의 행동 조건

        En_Start();                  // 초기세팅
    }

    private void Update()
    {
        E_Excute();                 // 상태 실행
    }

    private void OnDestroy()        // 하위 파리 두마리 생성
    {
        if (hp <= 0.1f) 
        {
            GenerateAttackFly();
            GenerateAttackFly();
        }
    }

    void GenerateAttackFly()
    {
        GameObject obj = Instantiate(attackFly, transform.position, Quaternion.identity) as GameObject;

        // SoundManage의 sfxObject로 추가.
        if (obj.GetComponent<AudioSource>() != null)
        {
            SoundManager.instance.sfxObjects.Add(obj.GetComponent<AudioSource>());
            obj.GetComponent<AudioSource>().volume = SoundManager.instance.GetSFXVolume();
        }    

        roomInfo.GetComponent<Room>().enemis.Add(obj);
    }
}
