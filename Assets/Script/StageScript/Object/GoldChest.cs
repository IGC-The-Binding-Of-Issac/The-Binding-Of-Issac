using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldChest : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] Sprite openChestSprite;
    [SerializeField] Sprite closeChestSprite;

    private void Start()
    {
        closeChestSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void ResetObject()
    {
        //초기화
        gameObject.layer = 15;
        gameObject.GetComponent<SpriteRenderer>().sprite = closeChestSprite;

        // 오브젝트 끄기.
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ItemManager.instance.keyCount > 0) // 풀래아와 충돌시 , 열쇠가 1개 이상일때
        {
            ItemManager.instance.keyCount--; // 열쇠 사용

            gameObject.GetComponent<SpriteRenderer>().sprite = openChestSprite; // 열린상자 이미지로 변경
            OpenChest(); // 드랍 아이템 생성
            openChestSound();
            StartCoroutine(StopChest());
        }
    }

    IEnumerator StopChest()
    {
        gameObject.layer = 16;
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    void OpenChest()
    {
        GameObject it = Instantiate(ItemManager.instance.itemTable.OpenGoldChest(), transform.position, Quaternion.identity) as GameObject;
        GameManager.instance.roomGenerate.itemList.Add(it);
    }
    void openChestSound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
