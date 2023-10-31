
using UnityEngine;

namespace Joymg
{
    public class CameraMoveToPlayer : MonoBehaviour
    {
        [SerializeField]
        private Transform cameraPosition;

        private void Update()
        {
            transform.position = cameraPosition.position;
        }
    }
}
