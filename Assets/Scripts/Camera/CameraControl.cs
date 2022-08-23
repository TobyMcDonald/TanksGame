using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public float m_DampTime = 0.2f;

    public Transform m_target;

    private Vector3 m_MoveVelocity;
    private Vector3 m_DesiredPosition;


    //When the scene loads it will find an object with the 'Player' tag and set the position of the camera to be above the object
    private void Awake()
    {

        m_target = GameObject.FindGameObjectWithTag("Player").transform;
        
    }


    //Calls the 'Move' function
    private void FixedUpdate()
    {

        Move();

    }

    //Allows the camera to track the movement of the player and follow
    private void Move()
    {
        m_DesiredPosition = m_target.position;
        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
