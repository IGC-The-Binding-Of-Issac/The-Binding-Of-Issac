using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class TrinketInfo : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ItemManager.instance.TrinketItem == null)
        // 1. 장신구 아이템을 장착하고 있지 않을 시
        {
            ItemManager.instance.TrinketItem = this.gameObject;
            gameObject.transform.SetParent(collision.gameObject.GetComponent<PlayerController>().itemPosition);
            gameObject.transform.localPosition = new Vector3(0, 0, 0);
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            StartCoroutine(collision.gameObject.GetComponent<PlayerController>().GetAcTrItem());
        }


        //else if (collision.gameObject.CompareTag("Player") && ItemManager.instance.TrinketItem != null)
        //// 2. 장신구 아이템을 장착하고 있을 시
        //{

        //}

    }

    public void KeepItem()
    {
        transform.position = ItemManager.instance.itemStorage.position;
        transform.SetParent(ItemManager.instance.itemStorage);
    }
    public virtual void GetItem() { 
        Debug.Log("재정의 해줘!"); 
    }      
}
