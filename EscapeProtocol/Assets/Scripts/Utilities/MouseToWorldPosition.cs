using UnityEngine;

namespace Utilities
{
    public class MouseToWorldPosition : MonoSingleton<MouseToWorldPosition>
    {
        private Camera _camera;
        private Vector3 _lookPoint;

        protected override void Awake()
        {
            base.Awake();
            _camera = Camera.main;
        }

        private void Start()
        {
            Cursor.visible = false;
        }

        private void Update()
        {
            GetCursorWorldPoint();
        }
        internal Vector3 GetCursorWorldPoint(float zPosition = 0)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            {
                if(hit.collider.CompareTag("Pointer"))
                {
                    _lookPoint = hit.point;
                    _lookPoint.z = zPosition;
                }
            }
            return _lookPoint;
        }
    }
}