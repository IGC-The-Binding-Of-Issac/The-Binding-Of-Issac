using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool isClear = false;
    public bool playerInRoom = false;
    public Transform[] roomObjects;
    public List<GameObject> enemis = new List<GameObject>();

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

        CheckRoom();
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerInRoom = true;
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
