using _Project.Scripts.GameScene.Inventory;
using _Project.Scripts.GameScene.Services.Inventory;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.GameScene.UI.Views.PlayerInventory
{
    public class PlayerInventoryViewTestButtonsWidget : MonoBehaviour
    {
        [SerializeField] private Button _buttonFillRandom;
        [SerializeField] private Button _buttonClear;

        [Inject] private IInventoryService _inventoryService;

        private IInventoryController _inventoryController;

        private void Awake()
        {
            _buttonFillRandom.onClick.AddListener(OnButtonFillRandomClick);
            _buttonClear.onClick.AddListener(OnButtonClearClick);
        }

        public void Setup(IInventoryController inventoryController)
        { 
            _inventoryController = inventoryController;
        }

        private void OnButtonFillRandomClick()
        {
        }

        private void OnButtonClearClick()
        {
        }
    }
}