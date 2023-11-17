using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Enemy_Jump : TEnemy_FSM<TEnemy>
{
    [SerializeField] TEnemy e_Owner;                          // 주인 변수

    float playerToDis;
    float jumpAiPlayTime;
    Vector3 myPosi;

    public Enemy_Jump(TEnemy _ower)                       // 생성자 초기화
    {
        e_Owner = _ower;
    }

    public override void Enter()
    {
        e_Owner.eCurState = TENEMY_STATE.Jump;              // 현재 상태를 TENEMY_STATE의 Tracking으로 
        e_Owner.e_findPlayer();                             // 플레이어의 위치를 1회 받아옴

        e_Owner.e_moveInialize();                           // 내 위치 1회 받아오기
        e_Owner.e_findPlayer();                             // 플레이어 위치 1회 받아오기
        myPosi = new Vector3(e_Owner.getMyx, e_Owner.getMyy, 0); 
        playerToDis = Vector3.Distance(e_Owner.playerPosi.transform.position, myPosi);
        jumpAiPlayTime = playerToDis / e_Owner.getJumpSpeed;    // (점프)시간 = 플레이어와 tride거리 / 점프 속도
    }

    public override void Excute()
    {
        if (e_Owner.e_isDead())                                     // 몬스터가 죽으면 
        {
            e_Owner.ChageFSM(TENEMY_STATE.Die);                     // Die로 상태변화 
        }
    }

    public override void Exit()
    {
        e_Owner.ePreState = TENEMY_STATE.Jump;
    }

}
