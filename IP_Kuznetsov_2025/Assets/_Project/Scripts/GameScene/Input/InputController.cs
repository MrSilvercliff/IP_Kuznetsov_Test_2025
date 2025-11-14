using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Input
{
    public interface IInputController : IProjectService, IPointerInputListener
    {
        Vector2 PointerPosition { get; }

        void Subscribe(IPointerInputListener pointerInputListener);
        void UnSubscribe(IPointerInputListener pointerInputListener);
    }

    public class InputController : IInputController
    {
        public Vector2 PointerPosition { get; private set; }

        private List<IPointerInputListener> _pointerInputListeners;

        public InputController()
        {
            _pointerInputListeners = new();
        }

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        #region Input_Listener_Subscribes

        public void Subscribe(IPointerInputListener pointerInputListener)
        {
            if (_pointerInputListeners.Contains(pointerInputListener))
                return;

            _pointerInputListeners.Add(pointerInputListener);
        }

        public void UnSubscribe(IPointerInputListener pointerInputListener)
        {
            _pointerInputListeners.Remove(pointerInputListener);
        }

        #endregion Input_Listener_Subscribes



        #region Pointer_Input

        public void OnPointerPositionInput(Vector2 pointerPosition)
        {
            PointerPosition = pointerPosition;

            for (int i = 0; i < _pointerInputListeners.Count; i++)
                _pointerInputListeners[i].OnPointerPositionInput(PointerPosition);
        }

        #endregion Pointer_Input
    }
}
