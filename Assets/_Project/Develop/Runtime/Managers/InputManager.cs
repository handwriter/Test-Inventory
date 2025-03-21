using UnityEngine;

namespace _Project.Develop.Runtime.Managers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private float _cursorDistance;
        private Vector3 _mousePosition;

        private void Update()
        {
            Camera cam = Camera.main;
            Vector2 mousePos = Input.mousePosition;
            
            _mousePosition = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, _cursorDistance));
        }

        public Vector3 GetMousePosition() => _mousePosition;
    }
}

