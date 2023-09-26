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
    public int playerHp = 24; // 현재 체력
    public int playerMaxHp = 24; // 최대체력
    public float playerMoveSpeed = 5f; // 이동속도
    public float playerTearSpeed = 6f; // 투사체속도
    public float playerShotDelay = 0.5f; // 공격딜레이
    public float playerDamage = 1f; // 데미지
    public float playerRange = 5f; // 사거리



    
    //힐 했을 때 최대 체력을 넘지 못하게 함
    public void HealPlayer(int healAmount)
    {
        playerHp = Mathf.Min(playerMaxHp, playerHp + healAmount);
    }
}
