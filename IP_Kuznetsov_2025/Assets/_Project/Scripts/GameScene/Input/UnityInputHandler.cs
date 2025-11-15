using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _Project.Scripts.GameScene.Input
{
    public class UnityInputHandler : MonoBehaviour, IInputHandler
    {
        [Inject] private IInputController _inputController;

        public Task<bool> Init()
        {
            gameObject.SetActive(true);
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public void OnPointerPositionInputAction(InputAction.CallbackContext context)
        {
            var pointerPosition = context.ReadValue<Vector2>();
            _inputController.OnPointerPositionInput(pointerPosition);
        }
    }
}