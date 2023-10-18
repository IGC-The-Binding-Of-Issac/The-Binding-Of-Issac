using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class TrinketInfo : MonoBehaviour
{
    public int trinketItemCode;

    public bool canCollision;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌 대상이 플레이어일때
        if(collision.gameObject.CompareTag("Player") && canCollision)
        {
            gameObject.layer = 31;

            // 1. 장신구 아이템을 장착하고 있지 않을 시
            if (ItemManager.instance.TrinketItem == null) TrinketChange(collision);
            
            // 2. 장신구 아이템을 장착하고 있을 시
            else if(ItemManager.instance.TrinketItem != null)
            {
                GameObject obj = ItemManager.instance.itemTable.TrinketChange(ItemManager.instance.TrinketItem.GetComponent<TrinketInfo>().trinketItemCode);
                Transform dropPosition = GameManager.instance.playerObject.GetComponent<PlayerController>().itemPosition;
                GameObject beforeTrinket = Instantiate(obj, new Vector3(dropPosition.position.x, (dropPosition.position.y - 1f), 0),Quaternion.identity) as GameObject;
                Destroy(ItemManager.instance.TrinketItem);
                
                TrinketChange(collision);
            }
        }
    }

    public void SetTrinketItemCode(int code)
    {
        trinketItemCode = code;
    }

    void SetDelay()
    { 
        canCollision = true;
    }
    public void KeepItem() 
    {
        transform.position = ItemManager.instance.itemStorage.position;
        transform.SetParent(ItemManager.instance.itemStorage);
    }

    public virtual void GetItem() { 
        Debug.Log("재정의 해줘!"); 
    }

    private void TrinketChange(Collision2D collision)
    {
        ItemManager.instance.TrinketItem = this.gameObject;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        gameObject.transform.SetParent(collision.gameObject.GetComponent<PlayerController>().itemPosition);
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        StartCoroutine(collision.gameObject.GetComponent<PlayerController>().GetAcTrItem());
    }

    private void Update()
    {
        if(!canCollision)
            Invoke("SetDelay", 1.2f);
    }
}
