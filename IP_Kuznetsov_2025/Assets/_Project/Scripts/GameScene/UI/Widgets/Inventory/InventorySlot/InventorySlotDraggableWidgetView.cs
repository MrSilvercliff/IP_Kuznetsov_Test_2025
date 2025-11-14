using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.GameScene.UI.Widgets.Inventory.InventorySlot
{
    public class InventorySlotDraggableWidgetView : InventorySlotWidgetView, IDragHandler, IEndDragHandler, IDropHandler
    {
        [Header("DRAG STATE")]
        [SerializeField] private GameObject _dragFade;

        public void OnDrag(PointerEventData eventData)
        {
            _dragFade.SetActive(true);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _dragFade.SetActive(false);
        }

        public void OnDrop(PointerEventData eventData)
        {
            _dragFade.SetActive(false);
        }
    }
}