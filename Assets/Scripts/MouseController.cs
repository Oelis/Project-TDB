using System;
using Units;
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
            mouseClickAction.performed += Select;
        }

        private void OnDisable()
        {
            mouseClickAction.Disable();
            mouseClickAction.performed -= Select;
        }

        private void Select(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("Selecting Unit");
        }
    }
}