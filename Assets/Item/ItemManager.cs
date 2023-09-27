using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    #region singleton
    public static ItemManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    #endregion

    public int item_Activated_Count; //플레이어가 스페이스바를 누른 횟수, 1회당 아이템이 가진 능력치 증가 (액티브 아이템)
    public int inventory = 0; // 비트로 변환 필요. 
    public int coinCount = 0; // 코인 보유 개수 
    public int bombCount = 0; // 폭탄 보유 개수
    public int keyCount = 0; // 열쇠 보유 개수


    [Header("Unity Setup")]
    public GameObject bombPrefab;
}
