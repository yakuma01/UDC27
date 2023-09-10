using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class QueenBee : MonoBehaviour
    {
        private NavMeshAgent _agent;

        public int _hitPoint = 6;

        private bool _inCriticalStage = false;
        // Start is called before the first frame update
        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        
            _agent.speed = 10;
            _agent.acceleration = 20;
        
        }

        public void HitBee()
        {
            if (IsCritical())
            {
                OneHitBee();
            }
            else
            {
                _hitPoint--;
            }
        }

        private void OneHitBee()
        {
            _hitPoint = 0;
        }

        public void EnableCriticalStage()
        {
            _inCriticalStage = true;
        }
        public void DisableCriticalStage()
        {
            _inCriticalStage = false;
        }
        
        public bool IsCritical()
        {
            return _inCriticalStage;
        }

        // Update is called once per frame
        void Update()
        {
            if (_hitPoint == 0)
            {
                DestroyBee();
            }
        }

        private void DestroyBee()
        {
            gameObject.SetActive(false);
        }
    }
}
