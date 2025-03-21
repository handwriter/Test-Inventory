using VContainer;
using VContainer.Unity;
using UnityEngine;
using _Project.Develop.Runtime.Data;
using _Project.Develop.Runtime.InventorySystem;
using _Project.Develop.Runtime.Managers;
using _Project.Develop.Runtime.UI;

namespace _Project.Develop.Runtime.LifetimeScopes
{
    public class LevelLifetimeScope : LifetimeScope
    {
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private DragNDropManager _dragDropManager;
        [SerializeField] private BackpackController _backpackController;
        [SerializeField] private LevelUIRoot _levelUIRoot;
        [SerializeField] private NetworkManager _networkManager;

        protected override void Configure(IContainerBuilder builder)
        {
            BindLevelConfig(builder);
            BindInputManager(builder);
            BindBackpackController(builder);
            BindLevelRootUI(builder);
            BindDragDropManager(builder);
            BindNetworkManager(builder);

            builder.RegisterEntryPoint<Bootstrap>();
        }

        private void BindLevelConfig(IContainerBuilder builder) => builder.RegisterInstance(_levelConfig);
        private void BindInputManager(IContainerBuilder builder) => builder.RegisterInstance(_inputManager);
        private void BindBackpackController(IContainerBuilder builder) => builder.RegisterInstance(_backpackController);
        private void BindLevelRootUI(IContainerBuilder builder) => builder.RegisterInstance(_levelUIRoot);
        private void BindDragDropManager(IContainerBuilder builder) => builder.RegisterInstance(_dragDropManager);
        private void BindNetworkManager(IContainerBuilder builder) => builder.RegisterInstance(_networkManager);
    }
}