using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 offset;
    [SerializeField] float downAngle;
    [SerializeField] float power;
    private float horizontalInput;
    Vector3 DragStartPosition;
    Vector3 _velocity;
    LineRenderer lr;

    Transform cueBall;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            if(ball.GetComponent<Ball>().IsCueBall() )
            {
                cueBall = ball.transform;
                break;
            }
        }
        ResetCamera();

        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cueBall != null )
        {
            horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

            transform.RotateAround(cueBall.position, Vector3.up, horizontalInput);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ResetCamera();
        }

        if (Input.GetMouseButtonDown(0))
        {
            DragStartPosition = Input.mousePosition;
            
            
        }
        if (Input.GetMouseButton(0))
        {
            
            Vector3[] trajectory = Plot(cueBall.gameObject.GetComponent<Rigidbody>(), (Vector3)transform.position, _velocity, 500);
            
            lr.positionCount = trajectory.Length;

            Vector3[] positions = new Vector3[trajectory.Length];
            for(int i = 0; i < trajectory.Length; i++)
            {
                positions[i] = trajectory[i];
            }
            lr.SetPositions(positions);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 hitdirection = transform.forward;
            hitdirection = new Vector3(hitdirection.x, 0, hitdirection.z).normalized;

            Vector3 DragEndPosition = Input.mousePosition;
            Vector3 _velocity = (DragEndPosition - DragStartPosition) * power;

            cueBall.gameObject.GetComponent<Rigidbody>().AddForce(hitdirection * _velocity.magnitude, ForceMode.Impulse);

            //cueBall.gameObject.GetComponent<Rigidbody>().velocity = _velocity;

            Debug.Log(_velocity);
            Debug.Log(DragStartPosition);
            Debug.Log(DragEndPosition);
        }
    }
    public void ResetCamera()
    {
        transform.position = cueBall.position + offset;
        transform.LookAt(cueBall.position);
        transform.localEulerAngles = new Vector3(downAngle, transform.localEulerAngles.y, 0);

    }
    public Vector3[] Plot(Rigidbody rigidbody,Vector3 pos, Vector3 velocity, int steps)
    {
        Vector3[] results = new Vector3[steps];
        
        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;

        float drag = 1f - timestep * rigidbody.drag;
        Vector3 movestep = velocity * timestep;

        for(int i = 0; i < steps; i++)
        {
            movestep *= drag;
            pos += movestep;
            results[i] = pos;

        }
        return results;
    }

}
