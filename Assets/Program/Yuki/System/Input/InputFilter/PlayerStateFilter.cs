using System;
using UnityEngine;

[Serializable]
public class PlayerStateFilter : IInputFilter
{
    [SerializeField]
    private int test;
    
    public bool WhenCanInput()
    {
        return true;
    }
}
