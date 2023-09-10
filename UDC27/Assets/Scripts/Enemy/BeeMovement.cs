using System;
using System.Collections;
using Player;
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

        private bool calmDownAttack = false;

        [SerializeField]private BeeMotion _currentMotion;

        private Vector3 _initialPosition;

        private float _followRange = 10f;
        private float _attackRange = 4f;
        
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
            //StartCoroutine(IdleBeeMovement());


        }

        private void Update()
        {
            CheckForPlayer();

            switch (_currentMotion)
            {
                case BeeMotion.Idle:
                    //IdleBee();
                    break;
                case BeeMotion.Follow:
                    FollowBeeSetup();
                    break;
                case BeeMotion.Protect:
                    ProtectBeeSetup();
                    break;
                case BeeMotion.Attack:
                    AttackBee();
                    break;
            }
            _agent.transform.rotation = _initialRotation;
            
        }

        private void IdleBeeSetup()
        {
            _agent.speed = 3.5f;
            _agent.acceleration = 8f;
            _agent.transform.rotation = _initialRotation;
            //_agent.SetDestination(_initialPosition);
        }
        
        private void FollowBeeSetup()
        {
            //_agent.speed = 3f;
            _agent.acceleration = 8f;
            _agent.transform.rotation = _initialRotation;
            //_agent.SetDestination(target.transform.position);
        }
        
        private void AttackBeeSetup()
        {
            //_agent.speed = 3.5f;
            _agent.acceleration = 8f;
            _agent.transform.rotation = _initialRotation;
        }

        private void AttackBee()
        {
            var pos = target.transform.position;
            var offset = 2;
            if (target.GetComponent<CustomMovement>().facingRight)
            {
                pos += target.transform.right * offset;
            }
            else
            {
                pos -= target.transform.right * offset;
            }
            
            /*if (calmDownAttack)
            {
                pos = _initialPosition;
            }*/
            //_agent.SetDestination(pos);
        }

        private void ProtectBeeSetup()
        {
            //_agent.speed = 3.2f;
            _agent.acceleration = 8f;
            _agent.transform.rotation = _initialRotation;
            //_agent.SetDestination(queenBee.transform.position); 
        }
        
        private void CheckForPlayer()
        {
            var dist = Vector3.Distance(target.transform.position, transform.position);
            // 
            if (dist < _followRange)
            {
                if (dist < _attackRange)
                {
                    Debug.Log("attacking");
                    //calmDownAttack = false;
                    switch (_currentMotion)
                    {
                        case BeeMotion.Idle:
                        case BeeMotion.Protect:
                        case BeeMotion.Follow:
                            StartCoroutine(HandleCriticalTime(1));
                            _currentMotion = BeeMotion.Attack;
                            break;
                        case BeeMotion.Attack:
                            if (dist < 1.5 || calmDownAttack)
                            {
                                Debug.Log("its now");
                                _agent.SetDestination(GetOffsetPosition(5));
                                calmDownAttack = true;
                            }
                            else
                            {
                               // _agent.SetDestination(target.transform.position);
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
                        case BeeMotion.Follow:
                            _currentMotion = BeeMotion.Follow;
                            
                            _agent.SetDestination(GetOffsetPosition());
                            break;
                        case BeeMotion.Attack:
                            if (dist > 5)
                            {
                                _currentMotion = BeeMotion.Follow;
                                _agent.SetDestination(target.transform.position);
                                calmDownAttack = false;
                            }
                            _agent.SetDestination(_initialPosition);
                            break;
                        
                    }
                }
                
            }
            else
            {
                if (_currentMotion != BeeMotion.Idle)
                {
                    _currentMotion = BeeMotion.Idle;
                    //StartCoroutine(IdleBeeMovement());

                }
                
            }
        }

        private Vector3 GetOffsetPosition(int offset = 2)
        {
            var pos = target.transform.position;
            if (target.GetComponent<CustomMovement>().facingRight)
            {
                pos -= target.transform.right * offset;
            }
            else
            {
                pos += target.transform.right * offset;
            }

            return pos;

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
            _agent.ResetPath(); 
            
            Debug.Log("stopforattack");
            yield return new WaitForSeconds(1);
            Debug.Log("startforattack");

            _agent.SetDestination(target.transform.position);

            
            //_agent.speed = 3.5f;
            
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

            transform.localScale *= 1.4f;
            
            yield return new WaitForSeconds(time);
            
            transform.localScale /= 1.4f;
            
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