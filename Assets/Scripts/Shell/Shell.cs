using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{

    public float m_MaxLifeTime = 2f;

    public float m_MaxDamage = 34f;

    public float m_ExplosionRadius = 5;

    public float m_ExplosionForce = 100f;

    public ParticleSystem m_ExplosionParticles;

    // Start is called before the first frame update
    void Start()
    {
        
        Destroy(gameObject, m_MaxLifeTime);

    }


    //On colliding with an object will cause the tank to take damage and set a radius around the area that will do less damage
    private void OnCollisionEnter(Collision other)
    {
        
        Rigidbody targetRidgidbody = other.gameObject.GetComponent<Rigidbody>();

        if (targetRidgidbody != null)
        {

            targetRidgidbody.AddExplosionForce(m_ExplosionForce,transform.position, m_ExplosionRadius);

            TankHealth targetHealth = targetRidgidbody.GetComponent<TankHealth>();

            if (targetHealth != null)
            {

                float damage = CalculateDamage(targetRidgidbody.position);

                targetHealth.TakeDamage(damage);

            }

        }

        m_ExplosionParticles.transform.parent = null;

        m_ExplosionParticles.Play();

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);

        Destroy(gameObject);

    }


    //Function that calculates the damage a shell will do to a tank depending on the distance the explosion of the shell is to the tank
    private float CalculateDamage(Vector3 targetPosition)
    {

        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;

        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

        float damage = relativeDistance * m_MaxDamage;

        damage = Mathf.Max(0f, damage);

        return damage;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
