using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class BeeMovement : MonoBehaviour
    {
        public GameObject target;

        public GameObject queenBee;
        
        private NavMeshAgent _agent;

        private Quaternion _initialRotation;

        [SerializeField]private BeeMotion _currentMotion;

        private Vector3 _initialPosition;

        private float _attackRange = 10f;
        
        private enum BeeMotion
        {
            Idle,
            Follow,
            Protect,
            Attack
        }

        private void Start()
        {
            

            _agent = GetComponent<NavMeshAgent>();
            _initialRotation = _agent.transform.rotation;
            _initialPosition = _agent.transform.position;
            
            _currentMotion = BeeMotion.Idle;
            

        }

        private void Update()
        {
            CheckForPlayer();

            switch (_currentMotion)
            {
                case BeeMotion.Idle:
                    IdleBee();
                    break;
                case BeeMotion.Follow:
                    FollowBee();
                    break;
                case BeeMotion.Protect:
                    ProtectBee();
                    break;
                case BeeMotion.Attack:
                    AttackBee();
                    break;
            }
            _agent.transform.rotation = _initialRotation;
            
        }

        private void IdleBee()
        {
            _agent.speed = 3.5f;
            _agent.acceleration = 8f;
            _agent.transform.rotation = _initialRotation;
            _agent.SetDestination(_initialPosition);
        }
        
        private void FollowBee()
        {
            _agent.speed = 3.5f;
            _agent.acceleration = 8f;
            _agent.transform.rotation = _initialRotation;
            _agent.SetDestination(target.transform.position);
        }
        
        private void AttackBee()
        {
            _agent.speed = 10f;
            _agent.acceleration = 20f;
            _agent.transform.rotation = _initialRotation;
            _agent.SetDestination(target.transform.position);
        }
        
        private void ProtectBee()
        {
            _agent.speed = 3.2f;
            _agent.acceleration = 8f;
            _agent.transform.rotation = _initialRotation;
            _agent.SetDestination(queenBee.transform.position); 
        }
        
        private void CheckForPlayer()
        {
            if (Vector3.Distance(target.transform.position, transform.position) < _attackRange)
            {
                _currentMotion = BeeMotion.Attack;
            }
            else
            {
                _currentMotion = BeeMotion.Idle;
            }
        }
    }
}