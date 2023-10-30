using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class ActiveInfo : MonoBehaviour
{
    public int activeItemCode;
    private bool canCollision;
    public bool activated;
    public int needEnergy;
    public int currentEnergy = 0;

    [SerializeField]
    public GameObject player;


    public void SetActiveItem(int code, int energy)
    {
        activeItemCode = code;
        needEnergy = energy;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canCollision)
        {
            gameObject.layer = 31;

            if (ItemManager.instance.ActiveItem == null)
            {
                ActiveGet(collision);
            }

            else if (ItemManager.instance.TrinketItem != null)
            {

                GameObject obj = ItemManager.instance.itemTable.TrinketChange(ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>().activeItemCode);
                Transform dropPosition = GameManager.instance.playerObject.GetComponent<PlayerController>().itemPosition;
                GameObject beforeTrinket = Instantiate(obj, new Vector3(dropPosition.position.x, (dropPosition.position.y - 1f), 0), Quaternion.identity) as GameObject;

                // ���� ����� ������ ����Ʈ�� ���.
                //GameManager.instance.roomGenerate.itemList.Add(beforeTrinket);

                Destroy(ItemManager.instance.ActiveItem);

                ActiveGet(collision);
            }
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
        //DisconnectTrinket();
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        gameObject.transform.SetParent(collision.gameObject.GetComponent<PlayerController>().itemPosition);
        gameObject.transform.localPosition = new Vector3(0, 0, 0);

        Destroy(gameObject.GetComponent<Rigidbody2D>());
        StartCoroutine(collision.gameObject.GetComponent<PlayerController>().GetActiveItem());

    }


    public virtual void UseActiveItem()
    {
        Debug.Log("������");
    }
    void SetDelay()
    {
        canCollision = true;
    }
    private void Update()
    {
        //Start�� �غôµ� �ȵż� Update�� �Űܼ� �ϴϱ� �ȴ�.
        player = GameObject.FindWithTag("Player");

        if (!canCollision)
            Invoke("SetDelay", 0.8f);

        //��Ƽ�� �������� �ְ� ���� �������� �ʿ��� ��������ŭ á�� �� �����̽��ٸ� ������ + currentEnergy == needEnergy ���ǿ� �߰������ ��!
        if (ItemManager.instance.ActiveItem != null && Input.GetKeyDown(KeyCode.Space))
        {
            UseActiveItem();
            ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>().activated = true;
            StartCoroutine(player.GetComponent<PlayerController>().UseActiveItem());
        }
    }
}