using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class ActiveInfo : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    [Header("int")]
    public int activeItemCode;     //아이템 고유 번호
    public int needEnergy;         //사용에 필요한 에너지
    public int currentEnergy;      //현재 에너지

    [Header("string")]
    public string itemTitle;       //아이템 이름
    public string itemDescription; //아이템 요약 설명 [습득 시 중앙 UI 밑에 텍스트 한줄]
    public string itemInformation; //아이템 설명 [습득 전 왼쪽 UI에 설명들]

    [Header("bool")]
    public bool canUse;            //아이템 사용 가능 여부
    public bool canGet;            //아이템 습득 가능 여부 [최초 드랍 후 바로 습득 방지]

    public void SetActiveItem(int code, int energy) //습득 시 아이템 설정
    {
        player = GameManager.instance.playerObject;
        activeItemCode = code;
        needEnergy = energy;
        currentEnergy = needEnergy;
    }

    public void SetActiveString(string title, string description, string information)
    {
        itemTitle = title;
        itemDescription = description;
        itemInformation = information;
    }

    public virtual void Start()
    {
        Invoke("SetDelay", 0.8f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canGet
            && GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem) //아이템 습득 가능 조건
        {

            gameObject.layer = 31; //NoCollision으로 Layer 변경

            if (ItemManager.instance.ActiveItem == null) //액티브 아이템 최초 습득 시
            {
                UIManager.instance.ItemBanner(itemTitle, itemDescription);
                canUse = false;
                GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
                ActiveGet(collision);
            }

            else if (ItemManager.instance.ActiveItem != null) //이후 액티브 아이템 습득 시
            {
                UIManager.instance.ItemBanner(itemTitle, itemDescription);
                canUse = false;
                GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
                //이전 아이템 Object 불러오기
                GameObject obj = ItemManager.instance.itemTable.ActiveChange(ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>().activeItemCode);
                Transform dropPosition = GameManager.instance.playerObject.GetComponent<PlayerController>().itemPosition;
                //이전 아이템 Object 복제 후 드랍
                GameObject beforeActive = Instantiate(obj, new Vector3(dropPosition.position.x, (dropPosition.position.y - 1f), 0), Quaternion.identity) as GameObject;

                //아이템 사용 후 교체했을 때 당시 CurrentEnergy 정보 저장
                int curEnergy = ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>().currentEnergy;
                beforeActive.GetComponent<ActiveInfo>().currentEnergy = curEnergy;

                // 현재 드랍된 아이템 리스트에 등록.
                GameManager.instance.roomGenerate.itemList.Add(beforeActive);
                // 기존 아이템 삭제
                Destroy(ItemManager.instance.ActiveItem);
                ActiveGet(collision);
            }
            Invoke("SetCanChangeItem", 1f);
            UIManager.instance.UpdateActiveEnergy();
        }
    }
    //아이템 저장 (대충 창고 역할)
    public void KeepItem()
    {
        transform.position = ItemManager.instance.itemStorage.position;
        transform.SetParent(ItemManager.instance.itemStorage);
    }

    //아이템 습득
    private void ActiveGet(Collision2D collision)
    {
        ItemManager.instance.ActiveItem = this.gameObject;
        //아이템 중복 드랍 방지
        DisconnectActive();
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        gameObject.transform.SetParent(collision.gameObject.GetComponent<PlayerController>().itemPosition);
        gameObject.transform.localPosition = new Vector3(0, 0, 0);

        Destroy(gameObject.GetComponent<Rigidbody2D>());
        StartCoroutine(collision.gameObject.GetComponent<PlayerController>().GetActiveItem());
        Invoke("SetCanUse", 1f);
        Invoke("SetCanChangeItem", 1f);
    }

    public virtual void afterActiveAttack()
    {
        //Debug.Log("액티브 사용 후 재정의");
    }

    public virtual void UseActive()
    {
        PlayerManager.instance.CheckedStatus();
        //Debug.Log("액티브 사용 시 재정의");
    }

    public virtual void CheckedItem()
    {
        //Debug.Log("눈물이 남아 있는 지 재정의");
    }

    private void SetDelay()
    {
        canGet = true;
    }
    void SetCanUse()
    {
        canUse = true;
    }

    void SetCanChangeItem()
    {
        GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = true;
    }

    //아이템 드랍 List에서 제외
    void DisconnectActive()
    {
        for (int i = 0; i < GameManager.instance.roomGenerate.itemList.Count; i++)
        {
            if (ItemManager.instance.ActiveItem.Equals(GameManager.instance.roomGenerate.itemList[i]))
            {
                GameManager.instance.roomGenerate.itemList.RemoveAt(i);
            }
        }
    }

    //방 클리어 시 currentEnergy 증가
    public void GetEnergy()
    {
        //현재 보유 에너지가 필요한 에너지보다 많을 시
        if (currentEnergy >= needEnergy)
            return;
        //장신구 중 AAA Battery를 소유하고 있을 시
        if (ItemManager.instance.TrinketItem != null && ItemManager.instance.TrinketItem.GetComponent<TrinketInfo>().trinketItemCode == 4)
        {
            if (needEnergy <= 1) currentEnergy++;
            else currentEnergy += 2;
        }
        else 
            currentEnergy++;
    }
}
