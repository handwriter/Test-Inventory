using UnityEngine;
using VContainer;
using static _Project.Develop.Runtime.Utils.Types;
using _Project.Develop.Runtime.Data;
using _Project.Develop.Runtime.InventorySystem;

namespace _Project.Develop.Runtime.Managers
{
    public class DragNDropManager : MonoBehaviour
    {
        private InventoryItemController _attachedItem;
        private InputManager _inputManager;
        private BackpackController _backpackController;
        private LevelConfig _levelConfig;

        [Inject]
        private void Construct(InputManager inputManager, BackpackController backpackController, LevelConfig levelConfig)
        {
            _inputManager = inputManager;
            _backpackController = backpackController;
            _levelConfig = levelConfig;
        }

        public void BackpackItemChangeState(bool state, InventoryItemController inventoryItem)
        {
            if (state) AttachItem(inventoryItem);
            else DetachItem();
        }

        public void AttachItem(InventoryItemController item)
        {
            if (item.GetItemState() == ItemStates.Detached)
            {
                item.SetItemState(ItemStates.OnMouse);
            }
            _attachedItem = item;
        }

        public void AttachItem(InventoryItemData item)
        {
            var itemController = _backpackController.FindItem(item);
            AttachItem(itemController);
        }

        public void DetachItem()
        {
            if (_attachedItem.GetItemState() == ItemStates.OnMouse)
            {
                _attachedItem.SetItemState(ItemStates.Detached);
            }
            _attachedItem = null;
        }

        private void Update()
        {
            if (_attachedItem != null)
            {
                float backpackDistance = Vector3.Distance(_backpackController.transform.position, _inputManager.GetMousePosition());
                bool isSnapDistance = backpackDistance < _levelConfig.SnapDistance;
                switch (_attachedItem.GetItemState())
                {
                    case ItemStates.OnMouse:
                        _attachedItem.SetPosition(_inputManager.GetMousePosition());
                        if (isSnapDistance) _backpackController.SnapItem(_attachedItem);
                        break;
                    case ItemStates.OnBackpack:
                        if (!isSnapDistance) _backpackController.DetachItem(_attachedItem);
                        break;
                    case ItemStates.Detached:
                        break;
                }
            }
        }
    }

}