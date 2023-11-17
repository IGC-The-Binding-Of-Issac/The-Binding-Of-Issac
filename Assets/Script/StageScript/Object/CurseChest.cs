using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class CurseChest : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] Sprite openChestSprite;
    [SerializeField] Sprite closeChestSprite;
    Room roomInfo;

    private void Start()
    {
        closeChestSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void ResetObject()
    {
        // 초기화
        gameObject.GetComponent<SpriteRenderer>().sprite = closeChestSprite;
        gameObject.layer = 15;

        // 오브젝트 끄기.
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 풀래아와 충돌시
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = openChestSprite; // 열린상자 이미지로 변경
            OpenChest(); // 몬스터 또는 패시브 아이템 드랍
            openChestSound(); // 상자 오픈 사운드 실행
            StartCoroutine(StopChest()); // 상자 밀림 멈춰!
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
        int rd = Random.Range(0, 10000);
        if(rd % 2 == 0)
        {
            GameObject it = Instantiate(ItemManager.instance.itemTable.DropPassive(), transform.position, Quaternion.identity) as GameObject;
            GameManager.instance.roomGenerate.itemList.Add(it);
            return;
        }

        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        // 파리 또는 거미중 랜덤한 몬스터 1종 생성
        int rd = Random.Range(0, 1000);
        int randomEnemyIndex;
        if (rd % 2 == 0)
            randomEnemyIndex = 0;
        else
            randomEnemyIndex = 5;

        // 파리 또는 거미를 리턴 받음.
        GameObject enemy = GameManager.instance.roomGenerate.enemyGenerate.GetEnemy(randomEnemyIndex);
        enemy.transform.SetParent(roomInfo.transform);
        enemy.transform.position = gameObject.transform.position;
        enemy.GetComponent<TEnemy>().roomInfo = roomInfo.gameObject;

        // 해당 방의 몬스터리스트에 추가
        roomInfo.GetComponent<Room>().enemis.Add(enemy);

        // 몬스터 생성으로 해당방에 몬스터가 존재하기때문에
        // 해당 방의 클리어 여부를 false로 변경
        roomInfo.isClear = false; 

        // sfx 사운드 조절을 위한 오브젝트 저장
        GameManager.instance.roomGenerate.SetSFXDestoryObject(enemy);
    }

    public void SetRoomInfo(GameObject room)
    {
        roomInfo = room.GetComponent<Room>();
    }

    void openChestSound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
