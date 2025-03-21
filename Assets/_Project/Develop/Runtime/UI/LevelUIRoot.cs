using UnityEngine;
using VContainer;
using _Project.Develop.Runtime.InventorySystem;
using _Project.Develop.Runtime.Managers;
using static _Project.Develop.Runtime.Utils.Types;

namespace _Project.Develop.Runtime.UI
{
    public class LevelUIRoot : MonoBehaviour
    {
        [SerializeField] private InventoryOverlayUI _inventoryOverlay;
        private BackpackController _backpack;
        private DragNDropManager _dragDropManager;

        [Inject]
        private void Construct(BackpackController backpack, DragNDropManager dragDropManager)
        {
            _backpack = backpack;
            _dragDropManager = dragDropManager;
        }

        public void Bind()
        {
            _backpack.OnItemAdded.AddListener(_inventoryOverlay.OnHandleItem);
            _backpack.OnItemRemoved.AddListener(_inventoryOverlay.OnDetachItem);
            _backpack.BackpackHoldState.AddListener(_inventoryOverlay.SetState);

            _inventoryOverlay.OnItemSelected.AddListener((x) => _backpack.DetachItem(x, ItemStates.Detached));
        }
    }
}