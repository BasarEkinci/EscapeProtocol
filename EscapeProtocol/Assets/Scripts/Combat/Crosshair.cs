using UnityEngine;

namespace Combat
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private float offset;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            Cursor.visible = false;
        }

        private void Update()
        {
            transform.position = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y - offset, 20));
        }
    }
}
