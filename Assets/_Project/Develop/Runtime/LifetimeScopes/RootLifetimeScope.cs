using VContainer;
using VContainer.Unity;
using UnityEngine;
using _Project.Develop.Runtime.Data;

namespace _Project.Develop.Runtime.LifetimeScopes
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private ProjectConfig _projectConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            BindProjectConfig(builder);
        }

        private void BindProjectConfig(IContainerBuilder builder) => builder.RegisterInstance(_projectConfig);
    }

}