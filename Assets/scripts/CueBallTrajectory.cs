using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CueBallTrajectory : MonoBehaviour
{
    public Transform cueBall; // The cue ball transform
    public Transform cue; // The cue stick transform
    public float maxDistance = 10f; // Maximum length of the trajectory line
    public LayerMask ballLayerMask; // LayerMask to detect collisions with other balls

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // We only need two points for a straight line
    }

    void Update()
    {
        DrawTrajectory();
    }

    void DrawTrajectory()
    {
        Vector3 start = cueBall.position;
        Vector3 direction = (cue.position - cueBall.position).normalized;

        RaycastHit hit;
        Vector3 end;

        if (Physics.Raycast(start, direction, out hit, maxDistance, ballLayerMask))
        {
            end = hit.point;
        }
        else
        {
            end = start + direction * maxDistance;
        }

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}