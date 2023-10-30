using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(GameManager.instance.stageLevel >= GameManager.instance.maxStage) // 현재 스테이지가 4일때 ( 마지막 스테이지일때 )
            {
                SceneManager.LoadScene("03_Outro"); // 아웃트로 재생.
            }
            else
            {
                GameManager.instance.NextStage();
            }
        }
    }
}
