using _Project.Scripts.GameScene.DragAndDrop;
using _Project.Scripts.GameScene.Input;
using _Project.Scripts.GameScene.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _Project.Scripts.GameScene.UI.Widgets.Inventory.DragAndDrop
{
    public class InventoryDragAndDropHandleWidget : MonoBehaviour, IInventoryDragAndDropHandleListener, IDropHandler
    {
        [SerializeField] private bool _dropIsSafe;

        [Inject] private IDragAndDropController _dragAndDropController;

        private void Awake()
        {
            _dragAndDropController.Subscribe(this);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _dragAndDropController.UnSubscribe(this);
        }

        public void OnDragStart()
        {
            gameObject.SetActive(true);
        }

        public void OnDragEnd()
        {
            gameObject.SetActive(false);
        }

        public void OnDrop(PointerEventData eventData)
        {
            _dragAndDropController.OnDrop(_dropIsSafe);
        }
    }
}