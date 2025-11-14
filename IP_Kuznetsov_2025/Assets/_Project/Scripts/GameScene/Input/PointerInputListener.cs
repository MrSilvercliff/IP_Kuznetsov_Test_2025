using UnityEngine;

namespace _Project.Scripts.GameScene.Input
{
    public interface IPointerInputListener
    {
        void OnPointerPositionInput(Vector2 pointerPosition);
    }
}