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
        GameManager.instance.roomGenerate.SpikePool.Push(gameObject);
    }
}
