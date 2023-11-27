using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MapObject
{
    protected int spriteIndex;
    protected int objectLayer;
    protected abstract override void initialization();                                      // 오브젝트 최초 생성시 초기화
    public abstract override void ResetObject();                                            // 오브젝트 초기화 ( 풀링 )
    public abstract override void Returnobject();                                           // 오브젝트 초기화 ( 풀링 )

    public virtual void GetDamage() { }                                                     // 오브젝트 데미지
    protected virtual void ChangeObjectSPrite() { }                                         // 오브젝트의 변화 ( 데미지를 받았을때 / 파괴되었을때 스프라이트 및 오브젝트 변경 )
    protected virtual void DestorySound() { gameObject.GetComponent<AudioSource>().Play(); } // 오브젝트 파괴시 사운드        
    protected virtual void DropItem() { }                                                   // 오브젝트 파괴시 아이템 드랍
}   
