using UnityEngine;

namespace Utility
{
    public class RayCaster : MonoBehaviour
    {
        void FixedUpdate()
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                Debug.Log(hit.transform.name);
            }
            
        }
    }
}