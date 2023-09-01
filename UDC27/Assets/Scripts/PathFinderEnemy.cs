using UnityEngine;
using UnityEngine.AI;

public class PathFinderEnemy : MonoBehaviour
{
    private Camera mainCamera;
    private NavMeshAgent agent;
    public GameObject player;

    void Start()
    {
        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        agent.SetDestination(player.transform.position);

    }
}