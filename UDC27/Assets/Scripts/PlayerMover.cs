using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float moveSpeed = 2f;
    public LineRenderer lineRenderer;
    public LayerMask obstacleLayer; // LayerMask to specify obstacle layers

    private Camera mainCamera;
    private Vector3 targetPosition;
    private bool shouldMove = false;

    void Start()
    {
        mainCamera = Camera.main;
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;
                shouldMove = true;
                StartCoroutine(MoveToTarget());
            }
        }
    }

    IEnumerator MoveToTarget()
    {
        if (shouldMove)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, targetPosition);

            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                // Check for obstacles in the path
                RaycastHit obstacleHit;
                Vector3 direction = targetPosition - transform.position;
                if (Physics.Raycast(transform.position, direction, out obstacleHit, direction.magnitude, obstacleLayer))
                {
                    // Calculate a new target position that avoids the obstacle
                    targetPosition = obstacleHit.point - (obstacleHit.normal * 1f);
                    lineRenderer.SetPosition(1, targetPosition); // Update the LineRenderer with the new target position
                }

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            shouldMove = false;
            lineRenderer.positionCount = 0;
        }
    }
}
