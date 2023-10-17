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
    public int coinCount = 0; // 코인 보유 개수 
    public int bombCount = 0; // 폭탄 보유 개수
    public int keyCount = 0; // 열쇠 보유 개수

    [Header("Passive Item State")]
    public bool[] PassiveItems; // 패시브 아이템 보유 현황
    /*
     * 3. 보유 현황에 따른 시너지 효과를 작성해줘야함.
     * 4. 보유 현황에 따른 플레이어의 이미지 리소스를 변경해줘야함.(보류)
    */

    [Header("unique Item State")]
    public GameObject ActiveItem; // 현재 보유중인 액티브 아이템
    public GameObject TrinketItem; // 현재 보유중인 장신구 아이템
        
    [Header("Unity Setup")]
    public GameObject bombPrefab; // 설치하는 폭탄 오브젝트 프리팹
    public ItemTable itemTable; // 아이템 드랍관련 스크립트
    public Transform itemStorage; //아이템 오브젝트를 보관할 창고 위치 (Active / Trinket 전용)

    private void Start()
    {
        PassiveItems = new bool[100];
    }
    /*
     * 하나의 delegate로 여러 함수들에 접근해 실행
     * 각 아이템의 기능 함수는 여기서 작성
     * 획득한 아이템의 해당 기능 함수들을 PlayerManager 스크립트에서 참조
     */
}
