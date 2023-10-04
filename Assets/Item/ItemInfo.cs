using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public int itemCode;
    public string itemName;

    public float item_Damage; //공격력 증가 (+, -)
    public float item_DamageMulti; //공격력 증가 (*, /)
    public float item_Hp; //체력 증가 (+, -)
    public float item_MaxHP; //최대 체력 증가 (+, -)
    public float item_AttackSpeed; //공격속도 증가 (+, -)
    public float item_Range; //사거리 증가 (+, -)
    public float item_MoveSpeed; //이동속도 증가 (+, -)
    public float item_ShotSpeed; //눈물속도 증가 (+, -)
    public float item_Luck; //행운 증가 (+, -)

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
}
