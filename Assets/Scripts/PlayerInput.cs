using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public event Action FirePressed;

    protected void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            FirePressed?.Invoke();  
        }

        if (Keyboard.current.lKey.wasReleasedThisFrame)
        {
            foreach (var VARIABLE in Registery<IDamageable>.All)
            {
                Debug.Log(VARIABLE);
            }
        }
    }
}
