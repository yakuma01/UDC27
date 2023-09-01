using UnityEngine;
using UnityEngine.AI;

public class PathFinder : MonoBehaviour
{
    private Camera mainCamera;
    private NavMeshAgent agent;
    private LineRenderer lineRenderer;
    private Vector3 targetPlayerPosition = Vector3.zero;

    void Start()
    {
        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetPlayerPosition = hit.point;
                //agent.obstacleAvoidanceType = 
            }
        }

        agent.SetDestination(targetPlayerPosition);
        DrawPath();
    }
    
    void DrawPath()
    {
        if (agent.hasPath)
        {
            lineRenderer.positionCount = agent.path.corners.Length;
            lineRenderer.SetPositions(agent.path.corners);
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }
}