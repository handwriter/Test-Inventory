using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;
using static _Project.Develop.Runtime.Utils.Types;
using _Project.Develop.Runtime.Data;

namespace _Project.Develop.Runtime.InventorySystem
{
    public class InventorySectionUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string _sectionTitle;
        [SerializeField] private TMP_Text _sectionLabelTXT;
        [SerializeField] private TMP_Text _itemTitleTXT;
        [SerializeField] private ItemTypes SectionType;
        [SerializeField] private Image _backgroundIMG;
        [SerializeField] private Color _backDefaultColor;
        [SerializeField] private Color _backHoveredColor;
        private InventoryItemData _currentItem;
        private LevelConfig _config;
        private bool _isSelected;

        [Inject]
        private void Construct(LevelConfig config) => _config = config;

        private void Start()
        {
            _sectionLabelTXT.text = _sectionTitle;
        }

        public ItemTypes GetSectionType() => SectionType;

        public void SetItem(InventoryItemData data)
        {
            _itemTitleTXT.text = data == null ? "" : data.Name;
            _currentItem = data;
        }

        public InventoryItemData GetCurrentItem() => _currentItem;

        public bool IsSelected() => _isSelected;

        private void SetHoveredState(bool state)
        {
            _backgroundIMG.DOColor(state ? _backHoveredColor : _backDefaultColor, _config.UISmoothtime);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_currentItem == null) return;
            SetHoveredState(true);
            _isSelected = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetHoveredState(false);
            _isSelected = false;
        }
    }
}