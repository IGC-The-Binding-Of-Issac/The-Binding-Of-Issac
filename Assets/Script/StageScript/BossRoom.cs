using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [Header("unity Setup")]
    [SerializeField] Transform bossSpawnPoint;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject nextStageDoor;

    private void Update()
    {
        if(gameObject.GetComponent<Room>().isClear)
        {
            openNextStage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 보스방 입장시
        if(collision.gameObject.CompareTag("Player"))
        {
            // 보스생성
            boss = GameObject.Find("EnemyGenerate").GetComponent<EnemyGenerate>().GetBoss();
            // 보스오브젝트를 보스방 자식오브젝트로 설정
            boss.transform.SetParent(gameObject.transform); 
            // 보스오브젝트를 보스방의 Room 스크립트의 enemis에 추가.
            gameObject.GetComponent<Room>().enemis.Add(boss);

            // 보스HP바 생성
        }
    }

    void openNextStage()
    {
        // 다음 스테이지문 열기
        nextStageDoor.SetActive(true);

        // 보스 HP바 삭제해주기
    }
}
