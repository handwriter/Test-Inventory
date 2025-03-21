using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using VContainer;
using static _Project.Develop.Runtime.Utils.Types;
using _Project.Develop.Runtime.Data;

namespace _Project.Develop.Runtime.InventorySystem
{
    public class InventoryItemController : MonoBehaviour
    {
        public UnityEvent<bool, InventoryItemController> OnStateChanged = new UnityEvent<bool, InventoryItemController>();

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ItemStates State;
        [SerializeField] private InventoryItemData _data;
        private LevelConfig _levelConfig;

        [Inject]
        private void Construct(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }

        private void OnMouseDown()
        {
            OnStateChanged?.Invoke(true, this);
        }

        private void OnMouseUp()
        {
            OnStateChanged?.Invoke(false, this);
        }

        public void SetPosition(Vector3 pos, Action callback = null)
        {
            transform.DOMove(pos, _levelConfig.SnapSmoothtime).OnComplete(() => callback?.Invoke());
        }

        public void SetRotation(Quaternion rot)
        {
            transform.DORotateQuaternion(rot, _levelConfig.SnapSmoothtime);
        }

        public InventoryItemData GetData() => _data;

        public ItemStates GetItemState() => State;

        public void SetItemState(ItemStates state)
        {
            State = state;
            switch (State)
            {
                case ItemStates.Detached:
                    _rigidbody.isKinematic = false;
                    break;
                default:
                    _rigidbody.isKinematic = true;
                    break;
            }
        }
    }
}