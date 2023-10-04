using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.SetStage(++GameManager.instance.stageLevel);
            GameManager.instance.StageStart();
        }
    }
}
