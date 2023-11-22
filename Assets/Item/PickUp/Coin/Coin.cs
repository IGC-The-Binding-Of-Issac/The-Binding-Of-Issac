using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip pickupClip;
    [SerializeField] AudioClip dropClip;
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void DropCoin()
    {
        GetComponent<Animator>().SetTrigger("Drop");
        gameObject.layer = 31;
        
        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-1.0f, 1.0f);
        float randomForce = Random.Range(50f, 70f);

        GetComponent<Rigidbody2D>().AddForce(new Vector2(randomX, randomY) * randomForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.layer = 31;
            GetComponent<Animator>().SetTrigger("Get");
            ItemManager.instance.coinCount++;
        }
    }

    public void GetCoin()
    {
        transform.localPosition = Vector3.zero;
        StartCoroutine(CoinReturnDelay());
    }

    public void DropCoinEnd()
    {
        gameObject.layer = 14;
    }

    public void ResetObject()
    {
        // ���̾� �ʱ�ȭ 
        gameObject.layer = 14;
    }
    
    IEnumerator CoinReturnDelay()
    {
        // ȹ�� ���� �������� ���� ������ ���� ������Ʈ ����
        yield return new WaitForSeconds(0.8f);
        // ������Ʈ ����,
        gameObject.SetActive(false);
        // ������Ʈ ����
        ItemManager.instance.itemTable.ReturnDropItem(gameObject);
    }


    public void DropSound()
    {
        audioSource.volume = SoundManager.instance.GetSFXVolume();
        audioSource.clip = dropClip;
        audioSource.Play();
    }

    public void GetSound()
    {
        audioSource.volume = SoundManager.instance.GetSFXVolume();
        audioSource.clip = pickupClip;
        audioSource.Play();
    }
}