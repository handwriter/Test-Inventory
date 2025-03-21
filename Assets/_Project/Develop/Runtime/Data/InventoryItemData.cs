using UnityEngine;
using static _Project.Develop.Runtime.Utils.Types;

namespace _Project.Develop.Runtime.Data
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
    public class InventoryItemData : ScriptableObject
    {
        public string Id;
        public string Name;
        public float Weight;
        public ItemTypes Type;
    }
}
