using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTankMovement : MonoBehaviour
{

    public float m_CloseDistance = 8f;

    public Transform m_Turret;

    private GameObject m_Player;

    private NavMeshAgent m_NavAgent;

    private Rigidbody m_Rigidbody;

    private bool m_Follow;

    public List<Transform> _waypoints = new List<Transform> ();

    private int currentWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    //Set the AI of the tank to look for a GameObject tagged as a 'Player', but sets it to not follow the tagged object. Also sets up te navmesh and rigidbody
    private void Awake()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Follow = false;
    }

    private void OnEnable()
    {

        m_Rigidbody.isKinematic = false;

    }

    private void OnDisable()
    {

        m_Rigidbody.isKinematic = true;

    }


    //When an Object tagged as 'Player' enters the range it will set the follow bool to be true
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            m_Follow = true;

        }       

    }


    //When an Object tagged as 'Player' exits the range it will set the follow bool to be false
    private void OnTriggerExit(Collider other)
    {

        if(other.tag == "Player")
        {

            m_Follow = false;

        }

    }

    // Update is called once per frame
    void Update()
    {

        if (m_Follow == false)
        {

            if (_waypoints.Count <= 0)
            {

                return;

            }

            if (currentWaypoint < _waypoints.Count)
            {

                if (Vector3.Distance(transform.position, _waypoints[currentWaypoint].position) > 2)
                {

                    m_NavAgent.SetDestination(_waypoints[currentWaypoint].position);

                }
                else
                {

                    currentWaypoint++;

                }

            }
            else
            {

                currentWaypoint = 0;

            }

        }
        else
        {

            float distance = (m_Player.transform.position - transform.position).magnitude;

            if (distance > m_CloseDistance)
            {

                m_NavAgent.SetDestination(m_Player.transform.position);
                m_NavAgent.isStopped = false;

            }
            else
            {

                m_NavAgent.isStopped = true;

            }

            if (m_Turret != null)
            {

                m_Turret.LookAt(m_Player.transform);

            }

        }
        


    }

}
