using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFSM
{
    abstract public class FSM<T>
    {
        abstract public void Begin();
        abstract public void Run();
        abstract public void Exit();
    }
}


