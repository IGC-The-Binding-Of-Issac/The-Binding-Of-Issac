using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] Sprite destoryRock;
    // 폭탄에 피격이 DestoryRock()을 호출.
    public void DestroyRock()
    {
        dropItem();
        gameObject.GetComponent<SpriteRenderer>().sprite = destoryRock;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    void dropItem()
    {
        // ItemManage에서 구현후 실행 고민해봅시다.
        //코인,체력 등 드랍아이템 확률드랍. 구현 필요.
    }
}
