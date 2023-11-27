using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapObject : MonoBehaviour
{
    protected int noCollisionLayer = 31;

    private void Start()
    {
        initialization();
    }

    protected abstract void initialization();          // 오브젝트 최초 생성시 초기화
    public abstract void ResetObject();                // 오브젝트 초기화 ( 풀링 
    public abstract void Returnobject();               // 오브젝트 초기화 ( 풀링
}
