using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : PoolableObject
{
    public bool IsGoodGate;
    
    public bool GetGateStatus()
    {
        return IsGoodGate;
    }
}
