using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    /// <summary>
    /// 
    ///  - 총알 pooling -
    /// pooling을 하나 만들어 놓고 , 각자 몬스터가 pooling에 접근
    /// 
    /// </summary>

    public static EnemyPooling Instance;

    [Header("pooling")]
    [SerializeField] Transform[] childArr;
    // index 0 : 본인
    // index 1 : straightBullet 을 담을 공간
    // index 2 : followBullet ``

    [SerializeField] Transform straightPooling_parent;  // 총알이 담겨 있을 부모 (빈 오브젝트)
    [SerializeField] Transform followPooling_Parent;    // ``

    [Header("pooling 오브젝트")]
    [SerializeField] private GameObject straightBullet;
    [SerializeField] private GameObject followBullet;

    [Header("pooliong queue")]
    Queue<GameObject> poolingStraightBullet     = new Queue<GameObject>();
    Queue<GameObject> poolingFollowBullet       = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;

        // 나중에 playermanager...? 에서 초기화하기
        EnemyBullet_Initialize(10);
    }

    // 초기화 ,  queue에 미리 initCount 만큼 넣어둠
    private void EnemyBullet_Initialize(int initCount)
    {
        childArr = gameObject.GetComponentsInChildren<Transform>();         // 자식오브젝트를 배열로
        straightPooling_parent  = childArr[1];                       // 배열의 1
        followPooling_Parent    = childArr[2];                       // 배열의 2 

        for (int i = 0; i < initCount; i++) 
        {
            poolingStraightBullet.Enqueue(createStraightBullet());  //queue 에 값 추가
            poolingFollowBullet.Enqueue(createFollowBullet());
        }
    }

    // straight bullet 생성
    private GameObject createStraightBullet() 
    {
        GameObject newObj = Instantiate(straightBullet) as GameObject;
        newObj.gameObject.SetActive(false);                         // queue에 있을 때는 안 보이게 
        newObj.transform.SetParent(straightPooling_parent);         // 부모설정
        newObj.transform.localPosition = Vector3.zero;

        return newObj;
    }

    // follow Bullet 생성
    private GameObject createFollowBullet()
    {
        GameObject newObj = Instantiate(followBullet) as GameObject;
        newObj.gameObject.SetActive(false);                        // queue에 있을 때는 안 보이게 
        newObj.transform.SetParent(followPooling_Parent);          // 부모설정
        newObj.transform.localPosition = Vector3.zero;

        return newObj;
    }

    // 다른 스크립트에서 straightBullet 생성할 때 사용하는
    public GameObject GetStraightBullet(GameObject shootPosi) 
    {
        GameObject obj;

        // straight pool에 오브젝트가 담겨져 있으면
        if (Instance.poolingStraightBullet.Count > 0)
        {
            obj = Instance.poolingStraightBullet.Dequeue(); // queue에서 맨 앞에 있는거 삭제
        }
        // straight pool에 오브젝트가 담겨져 있지 않으면
        else
        {
            obj = Instance.createStraightBullet();      // 새로운 총알 만들기
            poolingStraightBullet.Enqueue(obj);  //queue 에 값 추가
        }

        obj.transform.position = shootPosi.transform.position;  // 스크립트 실행하는 위치로
        // 지정된 오브젝트를 return
        obj.gameObject.SetActive(true);                         // pooling 배열에서 setActive(false)로 해놓은 걸 꺼내면 보이게 만듬
        //obj.transform.SetParent(null);                          // 부모를 해제함
        return obj;
    }

    // 다른 스크립트에서 straightBullet 생성할 때 사용하는
    public GameObject GetFollowBullet(GameObject shootPosi)
    {
        GameObject obj;

        // straight pool에 오브젝트가 담겨져 있으면
        if (Instance.poolingStraightBullet.Count > 0)
        {
            obj = Instance.poolingFollowBullet.Dequeue(); // queue에서 맨 앞에 있는거 삭제
        }
        // straight pool에 오브젝트가 담겨져 있지 않으면
        else
        {
            obj = Instance.createFollowBullet();      // 새로운 총알 만들기
            poolingFollowBullet.Enqueue(obj);  //queue 에 값 추가
        }

        obj.transform.position = shootPosi.transform.position;  // 스크립트 실행하는 위치로
        // 지정된 오브젝트를 return
        obj.gameObject.SetActive(true);                         // pooling 배열에서 setActive(false)로 해놓은 걸 꺼내면 보이게 만듬
        //obj.transform.SetParent(null);                          // 부모를 해제함
        return obj;
    }


    // 다른 오브젝트에서 총알을 생성하고 파괴될때, 
    // return으로 pooling배열에 넣어줌
    // straight 총알 return
    public void returnBullet(GameObject obj) 
    {
        // straight 인지 follow 인지 검사해서 
        // 각자 해당하는 queue에 넣어줘야함

        if (obj.GetComponent<EnemyStraightBullet>() != null)        // straight 스크립트를 들고있으면?
        {
            // 부모의 아래 (0,0,0)으로
            obj.transform.localPosition = Vector3.zero;
            
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(straightPooling_parent);
            poolingStraightBullet.Enqueue(obj);            // 되돌아오면 다시 pooling 배열에 넣어줌
        }

        else if (obj.GetComponent<EnemyFollowBullet>() != null)     // follow 스크립트를 들고 있으면?
        {
            // 부모의 아래 (0,0,0)으로
            obj.transform.localPosition = Vector3.zero;

            // 따라가는 총알 초기화
            obj.GetComponent<EnemyFollowBullet>().setBulletDesti = Vector3.zero;
            obj.GetComponent<EnemyFollowBullet>().setPlayerPosi = null;

            obj.gameObject.SetActive(false);
            obj.transform.SetParent(followPooling_Parent);
            poolingFollowBullet.Enqueue(obj);              // 되돌아오면 다시 pooling 배열에 넣어줌
        }
      
    }

    

  
}
