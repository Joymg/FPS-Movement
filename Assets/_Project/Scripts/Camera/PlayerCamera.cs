using UnityEngine;

namespace Joymg
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField]
        private float sensibilityX = 3f;
        [SerializeField]
        private float sensibilityY = 3f;

        [SerializeField]
        private Transform orientation;

        [SerializeField]
        private float xRotation;
        [SerializeField]
        private float yRotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            float mouseX= Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensibilityX;
            float mouseY= Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensibilityY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
