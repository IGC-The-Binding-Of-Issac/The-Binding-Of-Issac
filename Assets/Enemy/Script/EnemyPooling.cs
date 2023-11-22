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

    [SerializeField]
    private GameObject straightBullet;
    private GameObject followBullet;

    Queue<EnemyBullet> poolingStraightBullet = new Queue<EnemyBullet>();
    Queue<EnemyBullet> poolingFollowBullet = new Queue<EnemyBullet>();

    private void Awake()
    {
        Instance = this;

        // 나중에 playermanager...? 에서 초기화하기
        EnemyBullet_Initialize(10);
    }

    // 초기화 ,  queue에 미리 initCount 만큼 넣어둠
    private void EnemyBullet_Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++) 
        {
            poolingStraightBullet.Enqueue(createStraightBullet());  //queue 에 값 추가
            poolingFollowBullet.Enqueue(createFollowBullet());
        }
    }

    // straight bullet 생성
    private EnemyBullet createStraightBullet() 
    {
        EnemyBullet newObj = Instantiate(straightBullet).GetComponent<EnemyBullet>();
        newObj.gameObject.SetActive(false);             // queue에 있을 때는 안 보이게 
        newObj.transform.SetParent(transform);          // 현재 이 스크립트가 들어있는 빈 오브젝트를 부모로
        return newObj;
    }

    // follow Bullet 생성
    private EnemyBullet createFollowBullet()
    {
        EnemyBullet newObj = Instantiate(followBullet).GetComponent<EnemyBullet>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);          // 현재 이 스크립트가 들어있는 빈 오브젝트를 부모로
        return newObj;
    }

    // 다른 스크립트에서 straightBullet 생성할 때 사용하는
    public static EnemyBullet GetStraightBullet() 
    {
        EnemyBullet obj;

        // straight pool에 오브젝트가 담겨져 있으면
        if (Instance.poolingStraightBullet.Count > 0)
        {
            obj = Instance.poolingStraightBullet.Dequeue(); // queue에서 맨 앞에 있는거 삭제
        }
        // straight pool에 오브젝트가 담겨져 있지 않으면
        else
        {
            obj = Instance.createStraightBullet();      // 새로운 총알 만들기
            // return 할 때 queue로 되돌려줌 , 여기서 queue 에 안넣어줘도됨
        }

        // 지정된 오브젝트를 return
        obj.gameObject.SetActive(true);                         // pooling 배열에서 setActive(false)로 해놓은 걸 꺼내면 보이게 만듬
        obj.transform.SetParent(null);                          // 부모를 해제함
        return obj;
    }

    // 다른 스크립트에서 straightBullet 생성할 때 사용하는
    public static EnemyBullet GetFollowBullet()
    {
        EnemyBullet obj;

        // straight pool에 오브젝트가 담겨져 있으면
        if (Instance.poolingStraightBullet.Count > 0)
        {
            obj = Instance.poolingFollowBullet.Dequeue(); // queue에서 맨 앞에 있는거 삭제
        }
        // straight pool에 오브젝트가 담겨져 있지 않으면
        else
        {
            obj = Instance.createFollowBullet();      // 새로운 총알 만들기
            // 어차피 return 할 때 queue로 되돌려줌 , 여기서 queue 에 안넣어줘도됨
        }

        // 지정된 오브젝트를 return
        obj.gameObject.SetActive(true);                         // pooling 배열에서 setActive(false)로 해놓은 걸 꺼내면 보이게 만듬
        obj.transform.SetParent(null);                          // 부모를 해제함
        return obj;
    }

    // 다른 오브젝트에서 총알을 생성하고 파괴될때, 
    // return으로 pooling배열에 넣어줌
    // straight 총알 return
    public static void returnStrightBullet(EnemyBullet obj) 
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingStraightBullet.Enqueue(obj); 
    }

    // Follow 총알 return
    public static void returnFollowBullet(EnemyBullet obj) 
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent (Instance.transform);
        Instance.poolingFollowBullet.Enqueue(obj);
    }


    

}
