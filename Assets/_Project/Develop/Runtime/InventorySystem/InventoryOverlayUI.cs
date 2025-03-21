using UnityEngine;
using System.Linq;
using static _Project.Develop.Runtime.Utils.Types;
using VContainer;
using DG.Tweening;
using UnityEngine.Events;
using _Project.Develop.Runtime.Data;

namespace _Project.Develop.Runtime.InventorySystem
{
    public class InventoryOverlayUI : MonoBehaviour
    {
        [System.Serializable]
        public class SectionData
        {
            public ItemTypes Type;
            public InventorySectionUI SectionUI;
        }

        public UnityEvent<InventoryItemData> OnItemSelected = new UnityEvent<InventoryItemData>();
        [SerializeField] private SectionData[] _sections;
        [SerializeField] private CanvasGroup _canvasGroup;
        private LevelConfig _levelConfig;

        [Inject]
        private void Construct(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnHandleItem(InventoryItemData item)
        {
            _sections
                .Where((x) => x.Type == item.Type)
                .First()
                .SectionUI
                .SetItem(item);
        }

        public void OnDetachItem(InventoryItemData item)
        {
            var query = _sections.Where((x) => x.SectionUI.GetCurrentItem() == item);
            if (!query.Any()) return;
            query
                .First()
                .SectionUI
                .SetItem(null);
        }

        private void SetShowState(bool state)
        {
            _canvasGroup.DOFade(state ? 1 : 0, _levelConfig.UISmoothtime).OnComplete(() =>
            {
                _canvasGroup.interactable = state;
                _canvasGroup.blocksRaycasts = state;
            });
        }

        public void SetState(bool state)
        {
            if (!state)
            {
                var query = _sections.Where((x) => x.SectionUI.IsSelected());
                if (query.Any()) OnItemSelected?
                        .Invoke(query
                                .First()
                                .SectionUI
                                .GetCurrentItem());
            }

            SetShowState(state);
        }
    }
}