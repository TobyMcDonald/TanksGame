using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{

    public float m_StartingHealth = 100f;

    public GameObject m_ExplosionPrefab;

    private float m_CurrentHealth;
    private bool m_Dead;

    private ParticleSystem m_ExplosionParticles;

    public Image m_healthBar;


    //Initialises the explosion particle and makes it invisible
    private void Awake()
    {
        
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();

        m_ExplosionParticles.gameObject.SetActive(false);

    }


    //When the object spawns it sets the health of the tank to it's starting health and sets the tank to be alive, then calls the healthbar UI function
    private void OnEnable()
    {

        //Sending a message to the console to make sure the loine of code runs
        Debug.Log("Health of all tanks set to full");
        
        
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();

    }


    //Sets the fill of the health bar to what the tanks current health is
    private void SetHealthUI()
    {

        m_healthBar.fillAmount = m_CurrentHealth / m_StartingHealth;

    }


    //Takes away from the current health of the tank (amount taken away depends on vicinity to shell explosion as determined by the float value 'amount')
    //Updates the health bar UI and then checks if the player should be dead, if yes runs the 'OnDeath' function
    public void TakeDamage(float amount)
    {

        m_CurrentHealth -= amount;

        SetHealthUI();

        if(m_CurrentHealth <= 0f && !m_Dead)
        {

            OnDeath();

        }

    }


    //Sets the dead bool varibale to true and plays the explosion particle effect
    private void OnDeath()
    {

        m_Dead = true;

        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        m_ExplosionParticles.Play();

        gameObject.SetActive(false);

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
