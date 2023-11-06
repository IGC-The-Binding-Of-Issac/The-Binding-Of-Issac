using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool isClear = false;
    public bool playerInRoom = false;
    public Transform[] roomObjects;
    public List<GameObject> enemis = new List<GameObject>();
    public List<GameObject> soundObjects = new List<GameObject>();

    [Header("Unity Setup")] 
    public Transform roomGrid;
    public Transform cameraPosition;
    public Transform[] doorPosition;
    public Transform[] movePosition;

    private void Update()
    {
        if(playerInRoom)
        {
            CameraSetting();
        }

        if(!isClear)
        {
            CheckRoom();
        }
    }
    void CheckRoom()
    {
        bool flag = true;   
        for(int i = 0; i < enemis.Count; i++)
        {
            if (enemis[i] != null)
            {
                flag = false;
                enemis[i].GetComponent<Enemy>().playerInRoom = playerInRoom;
            }
        }
        isClear = flag;

        //방이 클리어됐을때 플레이어가 방에 있으면.
        if(isClear && playerInRoom) // 방이 클리어 + 방에 플레이어 존재
        {
            if(ItemManager.instance.ActiveItem != null) // 보유한 액티브 아이템이 존재할때.
            {
                ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>().GetEnergy();
            }

            if (ItemManager.instance.PassiveItems[6])
            {
                PlayerManager.instance.CanBlockDamage++;
            }

        }
    }

    void CameraSetting()
    {
        float cameraMoveSpeed = 0.3f;
        GameManager.instance.myCamera.transform.SetParent(cameraPosition);
        GameManager.instance.myCamera.transform.localPosition = Vector3.MoveTowards(GameManager.instance.myCamera.transform.localPosition, new Vector3(0, 0, 0), cameraMoveSpeed);
    }

    public void SetGrid()
    {
        roomObjects = new Transform[roomGrid.transform.childCount]; // grid 개수만큼  배열 선언.
        for(int i = 0; i < roomObjects.Length; i++)
        {
            roomObjects[i] = roomGrid.GetChild(i);
        }
    }

    public void SoundMute()
    {
        for (int i = 0; i < soundObjects.Count; i++)
        {
            if (soundObjects[i] != null)
            {
                if (soundObjects[i].GetComponent<AudioSource>() != null)
                    soundObjects[i].GetComponent<AudioSource>().mute = true;
            }
        }
        for (int i = 0; i < enemis.Count; i++)
        {
            if (enemis[i] != null)
            {
                if (enemis[i].GetComponent<AudioSource>())
                    enemis[i].GetComponent<AudioSource>().mute = true;
            }
        }
    }

    public void SoundUnMute()
    {
        for (int i = 0; i < soundObjects.Count; i++)
        {
            if (soundObjects[i] != null)
            {
                if (soundObjects[i].GetComponent<AudioSource>() != null)
                    soundObjects[i].GetComponent<AudioSource>().mute = false;
            }
        }
        for (int i = 0; i < enemis.Count; i++)
        {
            if(enemis[i] != null)
            {
                if (enemis[i].GetComponent<AudioSource>())
                    enemis[i].GetComponent<AudioSource>().mute = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerInRoom = true;
            SoundUnMute();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRoom = false;
            SoundMute();
        }
    }
}
