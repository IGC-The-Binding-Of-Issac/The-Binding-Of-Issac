using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] Sprite destoryRock;
    Sprite defaultSprite;
    // 폭탄에 피격이 DestoryRock()을 호출.

    private void Start()
    {
        defaultSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    public void DestroyRock()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = destoryRock;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        DestorySound();
    }

    void DestorySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void ResetObject()
    {
        // 초기화
        gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

        // 오브젝트 끄기.
        gameObject.SetActive(false);
    }
}
