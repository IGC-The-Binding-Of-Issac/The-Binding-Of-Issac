using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Obstacle
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.instance.GetDamage();
        }
    }

    protected override void initialization() 
    {
        objectLayer = 9;
    }

    public override void ResetObject() 
    { 
        gameObject.SetActive(false); 
    }

    public override void Returnobject()
    {
        // 해당 오브젝트의 풀 stack에 넣어주기
    }
}
