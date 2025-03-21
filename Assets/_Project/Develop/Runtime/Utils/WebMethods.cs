using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

namespace _Project.Develop.Runtime.Utils
{
    public static class WebMethods
    {
        public static IEnumerator SendPostRequestWithAuth(string url, string json, string token)
        {
            var request = GetPostRequestWithAuth(url, json, token);

            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log("Error While Sending: " + request.error);
            }
            else
            {
                Debug.Log("Received: " + request.downloadHandler.text);
            }
        }

        public static UnityWebRequest GetPostRequestWithAuth(string url, string json, string token)
        {
            var request = GetPostRequest(url, json);
            request.SetRequestHeader("Authorization", $"Bearer {token}");
            return request;
        }

        public static UnityWebRequest GetPostRequest(string url, string json)
        {
            var request = new UnityWebRequest(url, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            return request;
        }
    }
}
