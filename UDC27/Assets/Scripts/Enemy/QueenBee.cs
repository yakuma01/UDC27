using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class QueenBee : MonoBehaviour
    {
        private NavMeshAgent _agent;

        private int _hitPoint = 6;
        // Start is called before the first frame update
        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        
            _agent.speed = 10;
            _agent.acceleration = 20;
        
        }

        public void HitBee()
        {
            _hitPoint--;
        }

        public void OneHitBee()
        {
            _hitPoint = 0;
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
