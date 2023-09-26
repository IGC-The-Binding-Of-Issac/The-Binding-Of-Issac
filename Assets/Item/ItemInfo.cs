using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public int itemCode;
    public string itemName;

    public float item_Damage; //공격력 증가 (+, -)
    public float item_DamageMulti; //공격력 증가 (*, /)
    public float item_Hp; //체력 증가 (+, -)
    public float item_MaxHP; //최대 체력 증가 (+, -)
    public float item_AttackSpeed; //공격속도 증가 (+, -)
    public float item_Range; //사거리 증가 (+, -)
    public float item_MoveSpeed; //이동속도 증가 (+, -)
    public float item_ShotSpeed; //눈물속도 증가 (+, -)
    public float item_Luck; //행운 증가 (+, -)
}
