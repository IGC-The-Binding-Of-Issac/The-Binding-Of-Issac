using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class ActiveInfo : MonoBehaviour
{
    public int activeItemCode;
    private bool canCollision;
    public int needEnergy;
    public int currentEnergy;
    public string itemTitle; //아이템 이름
    public string itemDescription; //아이템 요약 설명 [습득 시 중앙 UI 밑에 텍스트 한줄]
    public string itemInformation; // 아이템 설명 [습득 전 왼쪽 UI에 설명들]
    public bool canUse;
    [SerializeField]
    public GameObject player;


    public void SetActiveItem(int code, int energy)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canCollision
            && GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem)
        {
            gameObject.layer = 31;

            if (ItemManager.instance.ActiveItem == null)
            {
                canUse = false;
                GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
                ActiveGet(collision);
            }

            else if (ItemManager.instance.ActiveItem != null)
            {
                    canUse = false;
                    GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
                    GameObject obj = ItemManager.instance.itemTable.ActiveChange(ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>().activeItemCode);
                    Transform dropPosition = GameManager.instance.playerObject.GetComponent<PlayerController>().itemPosition;
                    GameObject beforeActive = Instantiate(obj, new Vector3(dropPosition.position.x, (dropPosition.position.y - 1f), 0), Quaternion.identity) as GameObject;

                    int curEnergy = ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>().currentEnergy;

                    beforeActive.GetComponent<ActiveInfo>().currentEnergy = curEnergy;

                    // 현재 드랍된 아이템 리스트에 등록.
                    GameManager.instance.roomGenerate.itemList.Add(beforeActive);
                    // 기존 아이템 삭제
                    Destroy(ItemManager.instance.ActiveItem);
                    ActiveGet(collision);
             }
            Invoke("SetCanChangeItem", 1f);
        }
    }
    public void KeepItem()
    {
        transform.position = ItemManager.instance.itemStorage.position;
        transform.SetParent(ItemManager.instance.itemStorage);
    }
    private void ActiveGet(Collision2D collision)
    {
        ItemManager.instance.ActiveItem = this.gameObject;
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
        //Debug.Log("액티브 사용 시 재정의");
    }

    public virtual void CheckedItem()
    {
        //Debug.Log("눈물이 남아 있는 지 재정의");
    }

    void SetDelay()
    {
        canCollision = true;
    }

    void SetCanUse()
    {
        canUse = true;
    }

    void SetCanChangeItem()
    {
        GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = true;
    }

    private void Update()
    {
        if (!canCollision)
        {
            Invoke("SetDelay", 0.8f);
        }
    }

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
    public void GetEnergy()
    {
        if (currentEnergy >= needEnergy)
            return;

        currentEnergy++;
    }
}
