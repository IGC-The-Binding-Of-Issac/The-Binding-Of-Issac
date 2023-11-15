using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool isClear = false;
    public bool playerInRoom = false;
    public Transform[] roomObjects;
    public List<GameObject> enemis = new List<GameObject>();
    AudioSource roomAudio;

    [Header("Unity Setup")] 
    public Transform roomGrid;
    public Transform cameraPosition;
    public Transform[] doorPosition;
    public Transform[] movePosition;

    private void Start()
    {
        roomAudio = GetComponent<AudioSource>();
    }

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
                enemis[i].GetComponent<TEnemy>().playerInRoom = playerInRoom;
            }
        }
        isClear = flag;

        //방이 클리어됐을때 플레이어가 방에 있으면.
        if(isClear && playerInRoom) // 방이 클리어 + 방에 플레이어 존재
        {
            // 액티브 아이템 에너지 증가.
            if(ItemManager.instance.ActiveItem != null) // 보유한 액티브 아이템이 존재할때.
            {
                ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>().GetEnergy();
            }

            // 쉴드 추가.
            if (ItemManager.instance.PassiveItems[6])
            {
                PlayerManager.instance.CanBlockDamage++;
            }

            //방 클리어 보상
            GameObject obj = Instantiate(ItemManager.instance.itemTable.OpenNormalChest(Random.Range(0,1000) % 4), transform.position, Quaternion.identity) as GameObject;
            GameManager.instance.roomGenerate.itemList.Add(obj);

            //door open sound
            DoorSound(1);
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

    public void DoorSound(int mode)
    {

        AudioClip doorAudio =  SoundManager.instance.GetDoorClip(mode); // 오디오클립 받아오기.
        roomAudio.clip = doorAudio; // 오디오클립 적용
        roomAudio.Play(); // 재생
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerInRoom = true;
            
            // 방에 플레이어가 입장했을때
            // 클리어 되어있지않으면
            // 문 닫히는 사운드 
            if(!isClear)
            {
                DoorSound(0); // close Sound
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRoom = false;
        }
    }
}
