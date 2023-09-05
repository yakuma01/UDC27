using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Utility;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        public GameObject tongue;
        public RayCaster rayCaster;
        
        private bool _takeInput;
        public float step = 1f;
        private NavMeshAgent _agent;
        private Quaternion _initialRotation;

        private float _inputBlockTime = .1f;

        // Start is called before the first frame update
        void Start()
        {
            _takeInput = true;
            _agent = GetComponent<NavMeshAgent>();
            _initialRotation = _agent.transform.rotation;
        }

        // Update is called once per frame
        void Update()
        {
            _agent.transform.rotation = _initialRotation;
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(ShootTongue());
            }
            if (!_takeInput)
            {
                return;
            }
            if (Input.GetKey(KeyCode.D))
            {
                var targetPosition = _agent.transform.position;
                targetPosition += Vector3.right * step;
                _agent.SetDestination(targetPosition);
                StartCoroutine(BlockInput());
            }
            if (Input.GetKey(KeyCode.A)){
                var targetPosition = _agent.transform.position;
                targetPosition += Vector3.left * step;
                _agent.SetDestination(targetPosition);
                StartCoroutine(BlockInput());
            }
            if (Input.GetKey(KeyCode.W)){
                var targetPosition = _agent.transform.position;
                targetPosition += Vector3.forward * step;
                _agent.SetDestination(targetPosition);
                StartCoroutine(BlockInput());
            }
            if (Input.GetKey(KeyCode.S)){
                var targetPosition = _agent.transform.position;
                targetPosition += Vector3.back * step;
                _agent.SetDestination(targetPosition);
                StartCoroutine(BlockInput());
            }
        }

        private IEnumerator ShootTongue()
        {
            tongue.SetActive(true);
            var nullCheck = new Vector3(420, 420, 420);
            var hitpos = rayCaster.GetRayCastPoint(nullCheck);
            if (hitpos != nullCheck)
            {
                hitpos.y = 0.01f;
                var tp = tongue.transform.position;
                Vector2 Point_1 = new Vector2(tp.x, tp.z);
                Vector2 Point_2 = new Vector2(hitpos.x, hitpos.z);
                float mappedAngle = Mathf.Atan2(Point_2.y - Point_1.y, Point_2.x - Point_1.x) * Mathf.Rad2Deg;
                var rotationVector = new Vector3(90, 0, mappedAngle);
                tongue.transform.rotation = Quaternion.Euler(rotationVector);
            }

            yield return new WaitForSeconds(_inputBlockTime);
            tongue.SetActive(false);
        }


        private IEnumerator BlockInput()
        {
            _takeInput = false;
            yield return new WaitForSecondsRealtime(_inputBlockTime);
            
            _takeInput = true;
        }
    }
}
