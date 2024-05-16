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
            Vector3 hitdirection = transform.forward;
            hitdirection = new Vector3(hitdirection.x, 0, hitdirection.z).normalized;
            cueBall.gameObject.GetComponent<Rigidbody>().AddForce(hitdirection * power, ForceMode.Impulse);


        }
       
    }
    public void ResetCamera()
    {
        transform.position = cueBall.position + offset;
        transform.LookAt(cueBall.position);
        transform.localEulerAngles = new Vector3(downAngle, transform.localEulerAngles.y, 0);

    }
   

}
