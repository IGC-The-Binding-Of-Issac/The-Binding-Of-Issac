using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSymbol : MonoBehaviour
{
    Room room;
    Sprite joinedSymbol;
    private void Start()
    {
        room = transform.parent.GetComponent<Room>();

        joinedSymbol = UIManager.instance.JoinedSymbol;
    }

    private void Update()
    {
        if(room.playerInRoom && joinedSymbol != null)
        {
            GetComponent<SpriteRenderer>().sprite = joinedSymbol;
            Destroy(this);
        }
    }
}
