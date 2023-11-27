using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class CurseChest : Chest
{
    [Header("Chest State")]
    Room roomInfo;

    protected override void initialization()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = closeSprite;
    }

    public override void Returnobject()
    {
        GameManager.instance.roomGenerate.CurseChestPool.Push(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OpenChest();
        }
    }


    protected override void DropReward()
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
}
