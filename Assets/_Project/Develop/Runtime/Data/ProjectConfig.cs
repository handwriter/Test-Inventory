using UnityEngine;

namespace _Project.Develop.Runtime.Data
{
    [CreateAssetMenu(fileName = "ProjectConfig", menuName = "Configs/Project Config")]
    public class ProjectConfig : ScriptableObject
    {
        public string ApiURL;
        public string ApiAuthToken;
    }
}