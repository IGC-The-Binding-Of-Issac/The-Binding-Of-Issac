using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GoatHoof : ItemInfo
{
    GoatHoof()
    {
        itemCode = 18;
        itemName = "Goat Hoof";
        item_MoveSpeed = 0.16f;
        item_AttackSpeed = -0.16f;
    }
}
