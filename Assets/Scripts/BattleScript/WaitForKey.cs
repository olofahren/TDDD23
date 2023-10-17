using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForKey : CustomYieldInstruction
{
    private KeyCode _key;

    public override bool keepWaiting
    {
        get
        {
            return !Input.GetKey(_key);
        }
    }

    public WaitForKey(KeyCode key)
    {
        _key = key;
    }
}
