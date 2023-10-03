using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] GameObject[] bossPrefabs;


    // 랜덤하게 몬스터 반환
    public GameObject GetEnemy()
    {
        int rd = Random.Range(0, enemyPrefabs.Length);

        GameObject enemy;
        enemy = Instantiate(enemyPrefabs[rd]) as GameObject;
        return enemy;
    }

    public GameObject GetBoss()
    {
        // 스테이지별 보스를 생성후 리턴
        GameObject boss = Instantiate(bossPrefabs[GameManager.instance.stageLevel]) as GameObject;  
        return boss;
    }
}