using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlace : Obstacle
{
    AudioSource sfxAudio;
    [Header("Unity SetUp")]
    [SerializeField] Sprite woodSprite;
    [SerializeField] GameObject eft;
    [SerializeField] GameObject boxCollider;
    [SerializeField] AudioClip destoryClip;
    [SerializeField] AudioClip fireClip;

    Sprite defaultSprite;
    Vector3 defaultScale;
    protected override void initialization()
    {
        objectLayer = 0;

        sfxAudio = GetComponent<AudioSource>();
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
        defaultScale = eft.transform.localScale;
    }

    public override void ResetObject()
    {
        //초기화
        spriteIndex = 0;
        FireSound();
        eft.SetActive(true);
        boxCollider.SetActive(true);
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
        gameObject.layer = objectLayer;
        eft.transform.localScale = defaultScale;

        //오브젝트 끄기
        gameObject.SetActive(false);
    }

    public override void Returnobject()
    {
        // 해당 오브젝트의 풀 stack에 넣어주기
    }


    public override void GetDamage()
    {
        spriteIndex++;
        ChangeObjectSPrite();
        if (spriteIndex >= 3)
        {
            DestorySound();

            DropItem();
            eft.SetActive(false);
            gameObject.GetComponent<SpriteRenderer>().sprite = woodSprite;
            gameObject.layer = noCollisionLayer;
            boxCollider.SetActive(false);
        }
    }

    protected override void ChangeObjectSPrite()
    {
        eft.transform.localScale = new Vector3(eft.transform.localScale.x - 0.2f, eft.transform.localScale.y - 0.2f, 0);
    }

    protected override void DestorySound()
    {
        AudioSource objectAudio = gameObject.GetComponent<AudioSource>();
        objectAudio.Stop();
        objectAudio.playOnAwake = false;
        objectAudio.loop = false;
        objectAudio.clip = destoryClip;
        objectAudio.Play();
    }

    protected override void DropItem()
    {
        int rd = Random.Range(0, 3);
        if (rd == 0)
        {
            ItemManager.instance.itemTable.Dropitem(transform.position, rd);
        }
    }


    void FireSound()
    {
        AudioSource objectAudio = gameObject.GetComponent<AudioSource>();
        objectAudio.playOnAwake = true;
        objectAudio.loop = true;
        objectAudio.clip = fireClip;
        objectAudio.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.instance.GetDamage();
        }
    }
}
