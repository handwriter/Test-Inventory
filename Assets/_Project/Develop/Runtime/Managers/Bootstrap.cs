using VContainer;
using VContainer.Unity;
using _Project.Develop.Runtime.UI;
using _Project.Develop.Runtime.InventorySystem;

namespace _Project.Develop.Runtime.Managers
{
    public class Bootstrap : IStartable
    {
        private LevelUIRoot _levelUIRoot;
        private BackpackController _backpack;
        private NetworkManager _networkManager;
        private DragNDropManager _dragDropManager;

        [Inject]
        private void Construct(LevelUIRoot levelUIRoot, BackpackController backpack, NetworkManager networkManager, DragNDropManager dragDropManager)
        {
            _levelUIRoot = levelUIRoot;
            _backpack = backpack;
            _networkManager = networkManager;
            _dragDropManager = dragDropManager;
        }

        // Game entry point
        public void Start()
        {
            _levelUIRoot.Bind();

            _backpack.OnItemAdded.AddListener(_networkManager.OnItemAttachedToBackpack);
            _backpack.OnItemRemoved.AddListener(_networkManager.OnItemDetachedFromBackpack);
            _backpack.OnChangeItemState.AddListener(_dragDropManager.BackpackItemChangeState);

            _backpack.Initialize();
        }
    }
}