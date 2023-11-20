using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator keyAni;

    public AudioSource audioSource;

    public AudioClip pickupClip;
    public AudioClip dropClip;

    Sprite defaultSprite;

    // 상자에서 폭탄이 나옴과 동시에 획득하는 문제가 있어서
    // 드랍이후 획득까지 딜레이를 주기위한 변수입니다.
    bool getDelay;
    public void Start()
    {
        audioSource = GetComponent<AudioSource>(); // 분명 초기화도 해줬는데
        keyAni = GetComponent<Animator>();

        getDelay = false;
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void DropKey()
    {
        DropSound();
        getDelay = false;
        StartCoroutine(GetDelay());

        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-1.0f, 1.0f);
        float randomForce = Random.Range(50f, 70f);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(randomX, randomY) * randomForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && getDelay)
        {
            getDelay = false;
            GetSound();

            gameObject.layer = 31;
            keyAni.SetTrigger("GetKey");
        }
    }

    public void GetKey()
    {
        ItemManager.instance.keyCount++;
        transform.localPosition = Vector3.zero;
        StartCoroutine(Delay());
    }

    public void ResetObject()
    {
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
        getDelay = false;
        gameObject.layer = 14;
    }

    IEnumerator GetDelay()
    {
        yield return new WaitForSeconds(0.3f);
        getDelay = true;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        ItemManager.instance.itemTable.ReturnDropItem(gameObject);
    }

    void DropSound()
    {
        if(audioSource == null) // 왜 없다고 뜨는지 모르겠어
        {
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.volume = SoundManager.instance.GetSFXVolume();
        audioSource.clip = dropClip;
        audioSource.Play();
    }

    void GetSound()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        audioSource.volume = SoundManager.instance.GetSFXVolume();
        audioSource.clip = pickupClip;
        audioSource.Play();
    }

}
