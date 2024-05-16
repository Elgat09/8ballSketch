using UnityEngine;

public class CueBallPath : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int pathResolution = 100; // Number of points in the path
    [SerializeField] private float pathHeight = 0.1f; // Height above the table

    private Vector3 cueBallStartPosition;
    private Vector3 cueBallFinalDestination;

    private void Start()
    {
        cueBallStartPosition = transform.position;
        

        Vector3[] pathPoints = CalculatePathPoints();

        lineRenderer.positionCount = pathPoints.Length;
        lineRenderer.SetPositions(pathPoints);
    }
    private void Update()
    {
        // Update the path whenever the camera or cue ball moves
        UpdatePath();
    }
    private void UpdatePath()
    {
        Vector3[] pathPoints = CalculatePathPoints();
        lineRenderer.positionCount = pathPoints.Length;
        lineRenderer.SetPositions(pathPoints);
    }

    private Vector3[] CalculatePathPoints()
    {
        Vector3[] points = new Vector3[pathResolution];
        for (int i = 0; i < pathResolution; i++)
        {
            float t = (float)i / (pathResolution - 1);
            Vector3 point = Vector3.Lerp(cueBallStartPosition, cueBallFinalDestination, t);
            point.y = pathHeight;
            points[i] = point;
        }
        return points;
    }

   
}
