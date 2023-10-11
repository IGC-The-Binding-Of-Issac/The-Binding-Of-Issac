using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalChest : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] Sprite openChest;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) // 풀래아와 충돌시
        {
            for(int i = 0; i < 2; i++)
            {
                int rd = Random.Range(0, 4);
                if(rd == 0)
                {
                    int coin = Random.Range(0, 6);
                    for(int j = 0; j < coin; j++)
                    {
                        Instantiate(ItemManager.instance.itemTable.OpenNormalChest(rd), transform.position, Quaternion.identity);
                    }
                }
                else
                {
                    Instantiate(ItemManager.instance.itemTable.OpenNormalChest(rd), transform.position, Quaternion.identity);
                }
            }
        }
    }
}
