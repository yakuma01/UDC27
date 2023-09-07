using UnityEngine;

namespace Utility
{
    public class RayCaster : MonoBehaviour
    {
        [SerializeField] private LayerMask UIlayerMask;
        [SerializeField] private Camera _cam;
        void FixedUpdate()
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                //Debug.Log(hit.transform.name);
            }
            
        }

        public Vector3 GetRayCastPoint(Vector3 nullCheck)
        {
            Vector3 mousePosition = Input.mousePosition;

            // Create a ray from the camera to the mouse click position
            Ray ray = _cam.ScreenPointToRay(mousePosition);

            // Perform a raycast on the plane
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var hitpos = hit.point;
                return hitpos;
            }

            return nullCheck;
        }
    }
}