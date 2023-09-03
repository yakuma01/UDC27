using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class FollowPlayer : MonoBehaviour
    {
        public GameObject target;

        private NavMeshAgent _agent;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _agent.SetDestination(target.transform.position);
        }
    }
}