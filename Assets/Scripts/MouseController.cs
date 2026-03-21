using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class MouseController : MonoBehaviour
    {
        [SerializeField] private InputAction mouseClickAction;
        private void OnEnable()
        {
            mouseClickAction.Enable();
            mouseClickAction.performed += ctx => Debug.Log("Mouse Clicked");
        }
    }
}