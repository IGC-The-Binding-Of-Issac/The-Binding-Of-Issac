using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossRoom : MonoBehaviour
{
    [Header("Room info")]
    [SerializeField] TEnemy bossComponent;
    [SerializeField] bool spawnBoss = true;

    [Header("unity Setup")]
    [SerializeField] Transform bossSpawnPoint;
    [SerializeField] GameObject nextStageDoor;
    [SerializeField] GameObject bossHpUI;
    [SerializeField] Image bossHP;
    private void Update()
    {
        // 보스가 생성된 이후
        if(!spawnBoss)
        {
            // 방이 클리어되면.
            if (gameObject.GetComponent<Room>().isClear)
            {
                nextStageDoor.SetActive(true);
                bossHpUI.SetActive(false);

                // 보스방 보상 생성
                GameObject reward = Instantiate(ItemManager.instance.goldTable) as GameObject;
                reward.transform.SetParent(gameObject.transform);
                reward.transform.localPosition = new Vector3(0, -1.3f, 0);

                reward.GetComponent<GoldTable>().SpawnItem(true);

                //보스방 클리어 BGM 재생
                SoundManager.instance.aft(1);
                Destroy(this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 보스방 입장시
        if(collision.gameObject.CompareTag("Player"))
        {
            if(spawnBoss)
            {
                //보스방 BGM 재생
                SoundManager.instance.OnBossBGM(0);

                gameObject.GetComponent<Room>().isClear = false;

                // 보스 재생성 방지
                spawnBoss = false;

                GameObject boss;
                // 보스생성
                boss = GameManager.instance.roomGenerate.enemyGenerate.GetBoss();
                GameManager.instance.roomGenerate.SetSFXDestoryObject(boss);

                // 보스오브젝트를 보스방 자식오브젝트로 설정
                boss.transform.SetParent(gameObject.transform);

                // 보스 오브젝트 위치를 0 0 0 으로 초기화
                boss.transform.localPosition = new Vector3(0, 0, 0);

                // 보스오브젝트를 보스방의 Room 스크립트의 enemis에 추가.
                gameObject.GetComponent<Room>().enemis.Add(boss);
                boss.GetComponent<TEnemy>().roomInfo = gameObject;
                bossComponent = boss.GetComponent<TEnemy>();
                bossComponent.hpBarSlider = bossHP;


                // 문닫기 / 보스 체력바 생성
                nextStageDoor.SetActive(false);
                bossHpUI.SetActive(true);
            }
        }
    }
}
