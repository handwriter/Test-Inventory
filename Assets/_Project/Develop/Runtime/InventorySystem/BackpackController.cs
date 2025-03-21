using _Project.Develop.Runtime.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VContainer;
using System.Linq;
using static _Project.Develop.Runtime.Utils.Types;
using System.Collections;
using _Project.Develop.Runtime.Managers;

namespace _Project.Develop.Runtime.InventorySystem
{
    public class BackpackController : MonoBehaviour
    {
        [System.Serializable]
        public class SnapPosition
        {
            public string Id;
            public Transform Target;
        }

        public UnityEvent<bool, InventoryItemController> OnChangeItemState
            = new UnityEvent<bool, InventoryItemController>();

        public UnityEvent<InventoryItemData> OnItemAdded = new UnityEvent<InventoryItemData>();
        public UnityEvent<InventoryItemData> OnItemRemoved = new UnityEvent<InventoryItemData>();
        public UnityEvent<bool> BackpackHoldState = new UnityEvent<bool>();

        [SerializeField] private List<InventoryItemController> _inventoryItems = new List<InventoryItemController>();
        [SerializeField] private SnapPosition[] _snapPositions;
        private LevelConfig _config;
        private InputManager _inputManager;
        private Coroutine _holdCoroutine;

        [Inject]
        private void Construct(LevelConfig config, InputManager inputManager, IObjectResolver container)
        {
            _config = config;
            _inputManager = inputManager;
            foreach (var item in _inventoryItems)
            {
                container.Inject(item);
            }
        }

        public void Initialize()
        {
            foreach (var item in _inventoryItems)
            {
                item.OnStateChanged.AddListener(OnItemStateChanged);
                OnItemAdded?.Invoke(item.GetData());
            }
        }

        private void OnItemStateChanged(bool state, InventoryItemController itemController)
        {
            OnChangeItemState.Invoke(state, itemController);
        }

        private Transform GetItemSnapPosition(InventoryItemController itemController)
        {
            var id = itemController.GetData().Id;
            return _snapPositions.Where((x) => x.Id == id).First().Target;
        }

        public void SnapItem(InventoryItemController itemController)
        {
            var snapPosition = GetItemSnapPosition(itemController);
            _inventoryItems.Add(itemController);
            OnItemAdded?.Invoke(itemController.GetData());
            itemController.SetItemState(ItemStates.Middle);
            itemController.SetPosition(snapPosition.position, () =>
            {
                itemController.SetItemState(ItemStates.OnBackpack);
            });
            itemController.SetRotation(snapPosition.rotation);
        }

        public void DetachItem(InventoryItemController itemController, ItemStates stateAfterDetach = ItemStates.OnMouse)
        {
            _inventoryItems.Remove(itemController);
            OnItemRemoved?.Invoke(itemController.GetData());
            Vector3 mousePosition = _inputManager.GetMousePosition();
            itemController.SetItemState(stateAfterDetach);
            itemController.SetPosition(mousePosition);
        }

        public void DetachItem(InventoryItemData itemData, ItemStates stateAfterDetach = ItemStates.OnMouse)
        {
            var itemController = FindItem(itemData);
            DetachItem(itemController, stateAfterDetach);
        }

        public InventoryItemController FindItem(InventoryItemData item)
        {
            return _inventoryItems
                    .Where((x) => x.GetData().Id == item.Id)
                    .First();
        }

        private void OnMouseDown()
        {
            _holdCoroutine = StartCoroutine(SendHoldEvent());
        }

        private void OnMouseUp()
        {
            StopCoroutine(_holdCoroutine);
            BackpackHoldState?.Invoke(false);
        }

        private IEnumerator SendHoldEvent()
        {
            yield return new WaitForSeconds(_config.BackpackHoldTime);
            BackpackHoldState?.Invoke(true);
        }
    }

}
