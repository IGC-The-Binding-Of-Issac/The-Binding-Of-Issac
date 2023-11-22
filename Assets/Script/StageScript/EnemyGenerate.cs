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
        //attackFlyPool.Push(attackFly);
        //moterPool.Push(moter);
        //pooterPool.Push(pooter);
        //mawPool.Push(maw);
        //pacerPool.Push(pacer);
        //spiderPool.Push(spider);    
        //tridePool.Push(tride);
        #endregion

        #region 오브젝트 off

        #endregion
    }


    //public GameObject P_GetEnemy()
    //{
    //    int rd = Random.Range(0,enemyPrefabs.Length);

    //}

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