using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public int itemCode;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // 아이템을 가지고있지않을때.
            if(!ItemManager.instance.PassiveItems[itemCode])
            {
                ItemManager.instance.PassiveItems[itemCode] = true; // 미보유 -> 보유 로 변경 
                UseItem();

                gameObject.transform.SetParent(collision.gameObject.GetComponent<PlayerController>().itemPosition);
                gameObject.transform.localPosition = new Vector3(0, 0, 0);
                Destroy(gameObject.GetComponent<Rigidbody2D>());

                StartCoroutine(collision.gameObject.GetComponent<PlayerController>().GetItemTime());
            }
        }
    }

    public virtual void UseItem() { Debug.Log("재정의 해줘!"); }

    public void SetItemCode(int code)
    {
        itemCode = code;
    }
}
