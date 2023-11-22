using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] GameObject[] bossPrefabs;

    #region pooling 적용중
    [Header("Pooling")]
    public Transform enemyPool_Transform;

    List<Stack<GameObject>> enemyPool;
    // {0,attackFly}
    // {1,mooter}
    // {2,pooter}
    // {3,maw}
    // {4,pacer}
    // {5,spider}
    // {6,tride}


    public void SetPoolingObject()
    {
        #region Stack 할당
        enemyPool = new List<Stack<GameObject>>();
        for(int i = 0; i < 7; i++)
            enemyPool.Add(new Stack<GameObject>());
        #endregion

        #region 오브젝트 생성
        // 생성
        GameObject attackFly = Instantiate(enemyPrefabs[0],enemyPool_Transform.position, Quaternion.identity);
        GameObject moter = Instantiate(enemyPrefabs[1], enemyPool_Transform.position, Quaternion.identity);
        GameObject pooter = Instantiate(enemyPrefabs[2], enemyPool_Transform.position, Quaternion.identity);
        GameObject maw = Instantiate(enemyPrefabs[3], enemyPool_Transform.position, Quaternion.identity);
        GameObject pacer = Instantiate(enemyPrefabs[4], enemyPool_Transform.position, Quaternion.identity);
        GameObject spider = Instantiate(enemyPrefabs[5], enemyPool_Transform.position, Quaternion.identity);
        GameObject tride = Instantiate(enemyPrefabs[6], enemyPool_Transform.position, Quaternion.identity);

        // 부모설정
        attackFly.transform.SetParent(enemyPool_Transform);
        moter.transform.SetParent(enemyPool_Transform);
        pooter.transform.SetParent(enemyPool_Transform);
        maw.transform.SetParent(enemyPool_Transform);
        pacer.transform.SetParent(enemyPool_Transform);
        spider.transform.SetParent(enemyPool_Transform);
        tride.transform.SetParent(enemyPool_Transform);

        // 오브젝트 off
        attackFly.SetActive(false);
        moter.SetActive(false);
        pooter.SetActive(false);
        maw.SetActive(false);
        pacer.SetActive(false);
        spider.SetActive(false);
        tride.SetActive(false);
        #endregion

        #region 오브젝트 저장
        enemyPool[0].Push(attackFly);
        enemyPool[1].Push(moter);
        enemyPool[2].Push(pooter);
        enemyPool[3].Push(maw);
        enemyPool[4].Push(pacer);
        enemyPool[5].Push(spider);
        enemyPool[6].Push(tride);
        #endregion
    }


    public GameObject P_GetEnemy(int enemyIndex = -1)
    {
        // 매개변수로 받은값이 없으면 랜덤 몬스터
        if (enemyIndex == -1 )
            enemyIndex = Random.Range(0, enemyPrefabs.Length);

        // 풀에 오브젝트가 없을때
        if (enemyPool[enemyIndex].Count == 0)
        {
            GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], enemyPool_Transform.position, Quaternion.identity);
            enemy.transform.SetParent(enemyPool_Transform);
            enemy.SetActive(false);
        }
        
        GameObject returnEnemy = enemyPool[enemyIndex].Pop();
        returnEnemy.SetActive(true);
        return returnEnemy;
    }

    public void P_ReturnEnemy(GameObject enemy, int enemyIndex)
    {
        enemyPool[enemyIndex].Push(enemy);
    }

    public void P_AllReturnEnemy()
    {
        for(int i = 0; i < enemyPool_Transform.childCount; i++)
        {
            GameObject enemyObject = enemyPool_Transform.GetChild(i).gameObject;

            enemyObject.GetComponent<TEnemy>().e_ReturnEnemy();
        }
    }
    #endregion


    // 랜덤하게 몬스터 반환
    public GameObject GetEnemy()
    {
        int rd = Random.Range(0, enemyPrefabs.Length);

        GameObject enemy;
        enemy = Instantiate(enemyPrefabs[rd]) as GameObject;
        return enemy;
    }

    public GameObject GetEnemy(int index)
    {
        GameObject enemy;
        enemy = Instantiate(enemyPrefabs[index]) as GameObject;
        return enemy;
    }


    public GameObject GetBoss()
    {
        // 스테이지별 보스를 생성후 리턴
        GameObject boss = Instantiate(bossPrefabs[GameManager.instance.stageLevel-1]) as GameObject;  
        return boss;
    }
}