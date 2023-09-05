using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        private List<NavMeshAgent> _agents;

        private void Start()
        {
            _agents = GetComponentsInChildren<NavMeshAgent>().ToList();
            
        }


        public NavMeshAgent GetAgentByName(string agentName)
        {
            foreach (var agent in _agents)
            {
                if (agent.name.Equals(agentName))
                {
                    return agent;
                }
            }

            return null;
        }

        public NavMeshAgent GetNearestEnemy(GameObject player)
        {
            var minDist = Mathf.Infinity;
            var nearest = _agents[0];
            foreach (var agent in _agents)
            {
                var curDist = Vector3.Distance(agent.transform.position, player.transform.position);
                if (curDist < minDist )
                {
                    minDist = curDist;
                    nearest = agent;
                }
            }
            return nearest;
        }
        
    }
}