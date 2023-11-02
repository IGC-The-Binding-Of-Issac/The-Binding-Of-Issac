using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [Header("Room info")]
    [SerializeField] GameObject boss;

    [Header("unity Setup")]
    [SerializeField] Transform bossSpawnPoint;
    [SerializeField] GameObject nextStageDoor;

    [SerializeField] bool spawnBoss = true;
    private void Update()
    {
        if (gameObject.GetComponent<Room>().isClear)
        {
            nextStageDoor.SetActive(true);
        }
        else 
        {
            nextStageDoor.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 보스방 입장시
        if(collision.gameObject.CompareTag("Player"))
        {
            if(spawnBoss)
            {
                // 보스 재생성 방지
                spawnBoss = false;

                // 보스생성
                boss = GameObject.Find("EnemyGenerate").GetComponent<EnemyGenerate>().GetBoss();

                // 보스오브젝트를 보스방 자식오브젝트로 설정
                boss.transform.SetParent(gameObject.transform);

                // 보스 오브젝트 위치를 0 0 0 으로 초기화
                boss.transform.localPosition = new Vector3(0, 0, 0);

                // 보스오브젝트를 보스방의 Room 스크립트의 enemis에 추가.
                gameObject.GetComponent<Room>().enemis.Add(boss);

                gameObject.GetComponent<Room>().isClear = false;
                // 보스HP바 생성
                // 구현 필요.
            }
        }
    }
}
