using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlace : MonoBehaviour
{
    int fireIndex = -1;

    [Header("Unity SetUp")]
    [SerializeField] Sprite woodSprite;
    [SerializeField] GameObject eft;
    [SerializeField] GameObject boxCollider;
    public void GetDamage()
    {
        fireIndex++;
        ChangeEffectSize();
        if(fireIndex == 3)
        {
            ItemDrop();
            eft.SetActive(false);
            gameObject.GetComponent<SpriteRenderer>().sprite = woodSprite;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            boxCollider.SetActive(false);
        }
    }

    private void ChangeEffectSize()
    {
        eft.transform.localScale = new Vector3(eft.transform.localScale.x - 0.2f, eft.transform.localScale.y - 0.2f, 0);
    }
    void ItemDrop()
    {
        int rd = Random.Range(0, 3);
        if (rd == 0)
        {
            GameObject it = Instantiate(ItemManager.instance.itemTable.ObjectBreak(), transform.position, Quaternion.identity) as GameObject;
        }

#if UNITY_EDITOR && TEST_MODE
        Debug.Log("dddd");
#endif
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.instance.GetDamage();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().GetDamage(1);
        }
    }
}
