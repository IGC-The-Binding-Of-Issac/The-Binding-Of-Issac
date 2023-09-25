using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public int playerHp = 24;

    public int playerMaxHp = 24;

    public float playerMoveSpeed = 5f;

    public float playerTearSpeed = 6f;

    public float playerShotDelay = 0.5f;

    public float playerDamage = 1f;

    public float playerRange = 5f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    //힐 했을 때 최대 체력을 넘지 못하게 함
    public void HealPlayer(int healAmount)
    {
        playerHp = Mathf.Min(playerMaxHp, playerHp + healAmount);
    }
    
}