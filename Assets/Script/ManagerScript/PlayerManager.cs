using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region singleton
    public static PlayerManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    #endregion

    [Header("Player Stat")]
    public int playerHp = 6; // 현재 체력
    public int playerMaxHp = 6; // 최대체력
    public float playerMoveSpeed = 5f; // 이동속도
    public float playerTearSpeed = 6f; // 투사체속도
    public float playerShotDelay = 0.5f; // 공격딜레이
    public float playerDamage = 1f; // 데미지
    public float playerRange = 5f; // 사거리


    bool CanGetDamage = true; // 데미지받을수있는지 확인.
    float hitDelay = 1f; // 피격 딜레이
    public void GetDamage()
    {
        if(CanGetDamage)
        {
            playerHp--;
            CanGetDamage = false;
            StartCoroutine(HitDelay());
            
            if(playerHp <= 0) // 데미지를 받았을때 HP가 0이하가 되면 사망함수 실행.
            {
                Dead();
            }
        }
    }
    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(hitDelay);
        CanGetDamage = true;
    }

    // 사망 함수
    void Dead()
    {
        // 사망애니메이션 작성
        // 사망애니메이션 이후 사망아웃트로 씬으로 이동 작성
    }
}
