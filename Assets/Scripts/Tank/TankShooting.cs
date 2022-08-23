using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour
{

    public Rigidbody m_Shell;

    public Transform m_FireTransform;

    public float m_LaunchForce = 30f;

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {

        //When the left mouse button is pressed call the 'Fire' function
        if (Input.GetButtonUp("Fire1"))
        {

            Fire();

        }

    }


    //Sets the projection and spawns a shell
    private void Fire()
    {

        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;

    }

}
