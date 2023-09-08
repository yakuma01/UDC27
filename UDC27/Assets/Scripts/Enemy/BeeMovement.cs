using System;
using System.Collections;
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

        private float _followRange = 10f;
        private float _attackRange = 3f;
        
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
            StartCoroutine(IdleBeeMovement());


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
            //_agent.SetDestination(_initialPosition);
        }
        
        private void FollowBee()
        {
            _agent.speed = 3f;
            _agent.acceleration = 8f;
            _agent.transform.rotation = _initialRotation;
            _agent.SetDestination(target.transform.position);
        }
        
        private void AttackBee()
        {
            _agent.speed = 3.5f;
            _agent.acceleration = 8f;
            _agent.transform.rotation = _initialRotation;

            var pos = target.transform.position;
            pos = pos + target.transform.forward*5;
            _agent.SetDestination(pos);
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
            var dist = Vector3.Distance(target.transform.position, transform.position);
            // 
            if (dist < _followRange)
            {
                if (dist < _attackRange)
                {
                    switch (_currentMotion)
                    {
                        case BeeMotion.Idle:
                        case BeeMotion.Protect:
                        case BeeMotion.Follow:
                            StartCoroutine(HandleCriticalTime(1));
                            _currentMotion = BeeMotion.Attack;
                            break;
                        case BeeMotion.Attack:
                            if (dist < .5)
                            {
                                //_agent.
                            }
                            break;
                    }
                }
                else
                {
                    switch (_currentMotion)
                    {
                        case BeeMotion.Idle:
                        case BeeMotion.Protect:
                        case BeeMotion.Attack:
                            _currentMotion = BeeMotion.Follow;
                            break;
                        case BeeMotion.Follow:
                            break;
                        
                    }
                }
                
            }
            else
            {
                if (_currentMotion != BeeMotion.Idle)
                {
                    _currentMotion = BeeMotion.Idle;
                    StartCoroutine(IdleBeeMovement());

                }
                
            }
        }

        private IEnumerator IdleBeeMovement()
        {
            Vector3 below = transform.position + transform.up;
            Vector3 above = transform.position - transform.up;
            Vector3 right = transform.position - transform.right;
            Vector3 left = transform.position + transform.right;

            _agent.SetDestination(above);

            var time = 1f;
            yield return new WaitForSeconds(time);
            
            _agent.SetDestination(right);

            yield return new WaitForSeconds(time);
            
            _agent.SetDestination(below);

            yield return new WaitForSeconds(time);
            
            _agent.SetDestination(left);

            yield return new WaitForSeconds(time);

            StartCoroutine(IdleBeeMovement());
        }

        private IEnumerator HandleCriticalTime2(float time)
        {
            var halftime = time / 2f;

            var criticalTime = 1f;
            var criticalTimeStart = halftime - criticalTime/2;
            yield return new WaitForSeconds(criticalTimeStart);
            
            var common = _agent.GetComponent<CommonBee>();
            var queen = _agent.GetComponent<QueenBee>();
            
            if (common != null)
            {
                common.EnableCriticalStage();
            }
            else if (queen != null)
            {
                queen.EnableCriticalStage();
            }
            
            yield return new WaitForSeconds(criticalTime);
            
            if (common != null)
            {
                common.DisableCriticalStage();
            }
            else if (queen != null)
            {
                queen.DisableCriticalStage();
            }

            var remainingTime = time - criticalTimeStart - criticalTime;
            
            yield return new WaitForSeconds(remainingTime);

            Debug.Log("completed");


        }
        
        private IEnumerator HandleCriticalTime(float time)
        {
            
            //yield return new WaitForSeconds(1);
            
            var common = _agent.GetComponent<CommonBee>();
            var queen = _agent.GetComponent<QueenBee>();
            
            if (common != null)
            {
                common.EnableCriticalStage();
            }
            else if (queen != null)
            {
                queen.EnableCriticalStage();
            }

            transform.localScale *= 2;
            
            yield return new WaitForSeconds(time);
            
            transform.localScale /= 2;
            
            if (common != null)
            {
                common.DisableCriticalStage();
            }
            else if (queen != null)
            {
                queen.DisableCriticalStage();
            }

        }
    }
}