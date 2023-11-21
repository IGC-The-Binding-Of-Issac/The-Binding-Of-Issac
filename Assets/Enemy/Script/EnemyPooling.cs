using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    public static EnemyPooling Instance;

    [SerializeField]
    private GameObject straightBullet;
    private GameObject followBullet;

    Queue<EnemyBullet> poolingObject = new Queue<EnemyBullet>();

    private void Awake()
    {
        Instance = this;

        Initialize(10);
    }

    // 초기화 ,  queue에 미리 initCount 만큼 넣어둠
    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++) 
        {
            poolingObject.Enqueue(CreateNewObject());
        }
    }

    // straight bullet 생성
    private EnemyBullet CreateNewObject() 
    {
        EnemyBullet newObj = Instantiate(straightBullet).GetComponent<EnemyBullet>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;

    }

}
