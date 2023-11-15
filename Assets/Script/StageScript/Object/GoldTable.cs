using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTable : MonoBehaviour
{
    GameObject roomInfoObject;
    Room roomInfo;

    [Header("State")]
    [SerializeField] GameObject item;

    [Header("Unity Setup")]
    [SerializeField] Transform itemPosition;

    private void FixedUpdate()
    {

        if(roomInfo != null)
        {
            if(roomInfo.playerInRoom)
            {
                SpawnItem();
                Destroy(this);
            }
        }
    }

    public void SpawnItem(bool boss = false)
    {
        item = Instantiate(ItemManager.instance.itemTable.OpenGoldChest()) as GameObject;

        item.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        item.transform.SetParent(itemPosition);
        item.transform.localPosition = new Vector3(0, 0, 0);

        GameObject eff = Instantiate(ItemManager.instance.tableEffect);
        eff.transform.SetParent(gameObject.transform);
        eff.transform.localPosition = new Vector3(0, 1.5f, 0);
        if(boss)
        {
            StartCoroutine(BossReward());
        }
    }

    public void SetRoomInfo(GameObject obj)
    {
        roomInfoObject = obj;
        roomInfo = roomInfoObject.GetComponent<Room>();
    }

    public IEnumerator BossReward()
    {
        item.layer = 31;
        yield return new WaitForSeconds(0.7f);
        item.layer = 14;

        Destroy(this);
    } 
}
