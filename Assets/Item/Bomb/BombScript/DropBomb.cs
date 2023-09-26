using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    private void Start()
    {
        // 최초 드랍 애니메이션 필요.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GetBomb();
            ItemManager.instance.bombCount++;
            Destroy(gameObject);
        }
    }


    void GetBomb()
    {
        // 먹었을때 폭탄 애니메이션 작성.
    }
}
