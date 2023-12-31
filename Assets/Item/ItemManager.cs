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

    [Header("Drop Item State")]
    public int coinCount = 0;        // 코인 보유 개수 
    public int bombCount = 0;        // 폭탄 보유 개수
    public int keyCount = 0;         // 열쇠 보유 개수

    [Header("Passive Item State")]
    public bool[] PassiveItems;      // 패시브 아이템 보유 현황
    public bool[] TrinketItems;      // 장신구 아이템 획득 현황
    public bool[] ActiveItems;       // 액티브 아이템 획득 현황


    [Header("unique Item State")]
    public GameObject ActiveItem;    // 현재 보유중인 액티브 아이템
    public GameObject TrinketItem;   // 현재 보유중인 장신구 아이템
    
    [Header("Unity Setup")]
    public GameObject bombPrefab;    // 설치하는 폭탄 오브젝트 프리팹
    public ItemTable itemTable;      // 아이템 드랍 관련 스크립트
    public Transform itemStorage;    // 아이템 보관 장소 ( 장신구 / 액티브 )
    public GameObject goldTable;     // 아이템 테이블 프리팹
    
    [Header("Prefabs")]
    public GameObject tableEffect;   // 아이템 생성 이펙트

    private void Start()
    {
        PassiveItems = new bool[100];
        TrinketItems = new bool[100];
        ActiveItems = new bool[100];
    }
}
