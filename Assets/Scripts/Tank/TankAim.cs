using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAim : MonoBehaviour
{

    LayerMask m_layerMask;

    private void Awake()
    {

        m_layerMask = LayerMask.GetMask("Ground");

    }

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {

        //Find the position of the cursor and rotates the turret on the player tank to point to the mouse

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_layerMask))
        {
            transform.LookAt(hit.point);
        }

    }
}
