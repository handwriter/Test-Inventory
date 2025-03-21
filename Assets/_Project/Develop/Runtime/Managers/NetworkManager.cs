using _Project.Develop.Runtime.Data;
using UnityEngine;
using VContainer;
using _Project.Develop.Runtime.Utils;

namespace _Project.Develop.Runtime.Managers
{
    public class NetworkManager : MonoBehaviour
    {
        private ProjectConfig _projectConfig;

        [Inject]
        private void Construct(ProjectConfig projectConfig)
        {
            _projectConfig = projectConfig;
        }

        private void SendItemRequest(string action, string itemId)
        {
            string jsonData = $"{{\"action\": \"{action}\", \"item\": \"{itemId}\"}}";
            StartCoroutine(WebMethods.SendPostRequestWithAuth(_projectConfig.ApiURL, jsonData, _projectConfig.ApiAuthToken));
        }

        public void OnItemAttachedToBackpack(InventoryItemData item) => SendItemRequest("attachToBackpack", item.Id);

        public void OnItemDetachedFromBackpack(InventoryItemData item) => SendItemRequest("detachFromBackpack", item.Id);
    }

}