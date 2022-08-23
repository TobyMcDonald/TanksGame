using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{

    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;

    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;


    //Sets up the tanks rigidbody
    private void Awake()
    {

        m_Rigidbody = GetComponent<Rigidbody>();

    }

    private void OnEnable()
    {

        m_Rigidbody.isKinematic = false;

        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;

    }

    private void OnDisable()
    {
        
        m_Rigidbody.isKinematic = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Detects the pressing of the "WASD" keys or the arrow keys

        m_MovementInputValue = Input.GetAxis("Vertical");
        m_TurnInputValue = Input.GetAxis("Horizontal");

    }

    private void FixedUpdate()
    {

        //Moves the tank forward or backwards and allows the tank to turn by calling on the functions 'Move' and 'Turn'

        Move();
        Turn();

    }


    //Function that moves the tank forwards or backwards
    private void Move()
    {
        
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

    }


    //Function that rotates the tank
    private void Turn()
    {

        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);

    }

}
